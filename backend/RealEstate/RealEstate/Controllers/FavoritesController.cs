namespace RealEstate.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealEstate.Services.Favorites;
using RealEstate.Shared.Abstraction;
using RealEstate.Shared.Models;
using RealEstate.Shared.Models.Properties;
using RealEstate.Shared.Services.AuthorizedContext;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FavoritesController : ApiBaseController
{
    private readonly IAuthorizedContextService _authorizedContextService;
    private readonly IFavoritesService _favoritesService;

    public FavoritesController(IAuthorizedContextService authorizedContextService, IFavoritesService favoritesService)
    {
        _authorizedContextService = authorizedContextService;
        _favoritesService = favoritesService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Favorites([FromQuery] BaseModel model)
    {
        try
        {
            if (model.UserId == null)
            {
                return BadRequest(ApiResponse<List<PropertyModel>>.FailureResponse("You are not authorized.", new List<PropertyModel>()));
            }

            var properties = await _favoritesService.GetFavorites(model);
            return Ok(ApiResponse<List<PropertyModel>>.SuccessResponse(properties));
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                ApiResponse<List<PropertyModel>>.FailureResponse("An error occurred while processing your request")
            );
        }
    }

    [HttpPost]
    [Route("{propertyId}")]
    public async Task<IActionResult> AddRemoveFavorite([FromRoute] int propertyId, [FromQuery] BaseModel model)
    {
        try
        {
            if (model.UserId == null)
            {
                return BadRequest(ApiResponse<bool>.FailureResponse("You are not authorized."));
            }

            model.Id = propertyId;

            var (success, message) = await _favoritesService.AddRemoveFavorite(model);

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
