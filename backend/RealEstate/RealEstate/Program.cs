using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using RealEstate.Database;
using RealEstate.Shared.OptionsConfig;
using RealEstate.Shared.OptionsConfig.Jwt;
using RealEstate.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAppSettings(builder.Configuration, builder.Environment);

// Register Database Context.
builder.Services.RegisterContext(builder.Configuration);

// Register dependencies for services
builder.Services.AddServicsDependency();

// Register dependencies for options
builder.Services.AddOptions<JwtOptions>()
            .Bind(builder.Configuration.GetSection(OptionConstants.JwtSectionName))
            .ValidateDataAnnotations();

// Adding Authention in application
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer((options) =>
{
    var jwtOptions = builder.Configuration.GetSection(OptionConstants.JwtSectionName).Get<JwtOptions>();
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions!.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
    };
});

// Or you can also register as follows
builder.Services.AddHttpContextAccessor();

// Adding CORS to application
builder.Services.AddCors(options =>
{
    // For development only
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use cors
app.UseCors("AllowAll");

// Apply migrations and seed data
await app.Services.ApplyMigrationsAsync();
await app.Services.SeedDatabaseAsync();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
