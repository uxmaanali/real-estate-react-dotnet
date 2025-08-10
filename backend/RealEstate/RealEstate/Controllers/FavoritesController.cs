using Microsoft.AspNetCore.Mvc;

using RealEstate.Services;
using RealEstate.Shared.Models;
using RealEstate.Shared.Models.Properties;

namespace RealEstate.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FavoritesController : ControllerBase
{
    private readonly AuthorizedContext _authorizedContext;
    private readonly FavoritesService _favoritesService;

    public FavoritesController(AuthorizedContext authorizedContext, FavoritesService favoritesService)
    {
        _authorizedContext = authorizedContext;
        _favoritesService = favoritesService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Favorites()
    {
        try
        {
            var userId = _authorizedContext.GetUserId();
            if (userId == null)
            {
                return BadRequest(ApiResponse<List<PropertyDto>>.FailureResponse("You are not authorized.", new List<PropertyDto>()));
            }

            var properties = await _favoritesService.GetFavorites(userId.Value);
            return Ok(ApiResponse<List<PropertyDto>>.SuccessResponse(properties));
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                ApiResponse<List<PropertyDto>>.FailureResponse("An error occurred while processing your request")
            );
        }
    }

    [HttpPost]
    [Route("{propertyId}")]
    public async Task<IActionResult> AddRemoveFavorite([FromRoute] int propertyId)
    {
        try
        {
            var userId = _authorizedContext.GetUserId();
            if (userId == null)
            {
                return BadRequest(ApiResponse<bool>.FailureResponse("You are not authorized."));
            }

            var (success, message) = await _favoritesService.AddRemoveFavorite(userId.Value, propertyId);

            if (success)
            {
                return Ok(ApiResponse<bool>.SuccessResponse(success, message));
            }

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                ApiResponse<bool>.FailureResponse(message)
            );
        }
        catch (Exception)
        {
            return StatusCode(
               StatusCodes.Status500InternalServerError,
               ApiResponse<bool>.FailureResponse("An error occurred while processing your request")
           );
        }
    }
}
