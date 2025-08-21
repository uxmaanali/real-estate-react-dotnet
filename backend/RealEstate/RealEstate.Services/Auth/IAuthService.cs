using RealEstate.Shared.Models;
using RealEstate.Shared.Models.Login;
using RealEstate.Shared.Models.Register;

namespace RealEstate.Services.Auth;
public interface IAuthService
{
    Task<ApiResponse<LoginResponseDto>> Login(LoginRequestDto request);

    Task<ApiResponse<int>> Register(RegisterRequestDto request);
}
