using Microsoft.AspNetCore.Mvc;

using RealEstate.Services;
using RealEstate.Shared.Models.Login;
using RealEstate.Shared.Models.Register;

namespace RealEstate.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var response = await _authService.Login(request);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var response = await _authService.Register(request);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }
}
