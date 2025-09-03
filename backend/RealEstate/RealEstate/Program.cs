using RealEstate.Actions;
using RealEstate.Database.Utils;
using RealEstate.Services.Utils;
using RealEstate.Shared.OptionsConfig;
using RealEstate.Shared.OptionsConfig.Jwt;
using RealEstate.Utils;

var builder = WebApplication.CreateBuilder(args);

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

// Register encryption
builder.Services.AddEncryption(builder.Configuration);

// Register cache
builder.Services.AddHybridCache(builder.Configuration);

// Adding Authention & Authorization in application
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

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

// Add action filters
builder.Services.AddScoped<PopulateBaseDtoActionFilter>();

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<PopulateBaseDtoActionFilter>();
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
