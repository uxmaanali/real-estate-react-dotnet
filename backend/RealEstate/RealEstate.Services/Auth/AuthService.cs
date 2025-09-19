namespace RealEstate.Services.Auth;

using Microsoft.EntityFrameworkCore;

using RealEstate.Database.Entities;
using RealEstate.Database.Entities.Context;
using RealEstate.MessageBroker.Events;
using RealEstate.MessageBroker.Publisher;
using RealEstate.Services.Jwt;
using RealEstate.Shared.Abstraction;
using RealEstate.Shared.Models;
using RealEstate.Shared.Models.Login;
using RealEstate.Shared.Models.Register;
using RealEstate.Shared.Utils;

public class AuthService : IAuthService, IScopedDependency, IService
{
    private readonly RealEstateContext _dbContext;
    private readonly IJwtService _jwtService;
    private readonly IMessagePublisher _messagePublisher;

    public AuthService(RealEstateContext dbContext, IJwtService jwtService, IMessagePublisher messagePublisher)
    {
        _dbContext = dbContext;
        _jwtService = jwtService;
        this._messagePublisher = messagePublisher;
    }

    public async Task<ApiResponse<LoginResponseModel>> Login(LoginRequestModel request)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == request.Email, request.CancellationToken);
        if (user == null)
        {
            return ApiResponse<LoginResponseModel>.FailureResponse("Email or password is incorrect.");
        }

        var verified = PasswordHasher.VerifyPassword(request.Password, user.PasswordHash, user.Salt);
        if (!verified)
        {
            return ApiResponse<LoginResponseModel>.FailureResponse("Email or password is incorrect.");
        }

        var token = _jwtService.GenerateJWTToken(user);

        var response = new LoginResponseModel
        {
            UserId = user.Id,
            Token = token,
        };

        return ApiResponse<LoginResponseModel>.SuccessResponse(response);
    }

    public async Task<ApiResponse<int>> Register(RegisterRequestModel request)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == request.Email, request.CancellationToken);
        if (user is not null)
        {
            return ApiResponse<int>.FailureResponse("User already exists.");
        }

        var (isValid, message) = PasswordValidator.ValidatePassword(request.Password);
        if (!isValid)
        {
            return ApiResponse<int>.FailureResponse(message);
        }

        var salt = PasswordHasher.GeneratePasswordSalt();
        var passwordHash = PasswordHasher.HashPassword(request.Password, salt);

        var newUser = new User
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = request.Role,
            Salt = salt,
        };
        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();

        await _messagePublisher.Publish(new SendWelcomeMessageEvent(newUser.Id, request.Email, "+92123456789"));

        return ApiResponse<int>.SuccessResponse(newUser.Id);
    }
}
