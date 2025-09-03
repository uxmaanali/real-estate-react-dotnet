namespace RealEstate.Controllers;
using Microsoft.AspNetCore.Mvc;

using RealEstate.Services.Auth;
using RealEstate.Shared.Models.Login;
using RealEstate.Shared.Models.Register;
using RealEstate.Shared.Services.AuthorizedContext;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ApiBaseController
{
    private readonly IAuthService _authService;
    private readonly IAuthorizedContextService _authorizedContextService;

    public AuthController(IAuthService authService, IAuthorizedContextService authorizedContextService)
    {
        _authService = authService;
        _authorizedContextService = authorizedContextService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
    {
        var response = await _authService.Login(request);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestModel request)
    {
        var response = await _authService.Register(request);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }
}
