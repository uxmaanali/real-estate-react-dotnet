using Microsoft.EntityFrameworkCore;

using RealEstate.Database.Entities;
using RealEstate.Database.Entities.Context;
using RealEstate.Services.Abstraction;
using RealEstate.Shared.Models;
using RealEstate.Shared.Models.Login;
using RealEstate.Shared.Models.Register;
using RealEstate.Shared.Utils;

namespace RealEstate.Services
{
    public class AuthService : IScopedDependency
    {
        private readonly RealEstateContext _dbContext;
        private readonly JwtService _jwtService;

        public AuthService(RealEstateContext dbContext, JwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        }

        public async Task<ApiResponse<LoginResponseDto>> Login(LoginRequestDto request)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == request.Email);
            if (user == null)
            {
                return ApiResponse<LoginResponseDto>.FailureResponse("Email or password is incorrect.");
            }

            var verified = PasswordHasher.VerifyPassword(request.Password, user.PasswordHash, user.Salt);
            if (!verified)
            {
                return ApiResponse<LoginResponseDto>.FailureResponse("Email or password is incorrect.");
            }

            var token = _jwtService.GenerateJWTToken(user);

            var response = new LoginResponseDto
            {
                UserId = user.Id,
                Token = token,
            };

            return ApiResponse<LoginResponseDto>.SuccessResponse(response);
        }

        public async Task<ApiResponse<int>> Register(RegisterRequestDto request)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == request.Email);
            if (user is not null)
            {
                return ApiResponse<int>.FailureResponse("User already exists.");
            }

            var (isValid, message) = PasswordValidator.ValidatePassword(request.Password);
            if (!isValid)
            {
                return ApiResponse<int>.FailureResponse(message);
            }

            var salt = PasswordHasher.GenerateSaltFromGuid();
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

            return ApiResponse<int>.SuccessResponse(newUser.Id);
        }
    }
}
