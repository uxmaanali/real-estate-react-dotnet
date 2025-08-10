using Microsoft.AspNetCore.Mvc;

using RealEstate.Services;
using RealEstate.Shared.Models;
using RealEstate.Shared.Models.Properties;

namespace RealEstate.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertiesController : ControllerBase
{
    private readonly AuthorizedContext _authorizedContext;
    private readonly PropertiesService _propertiesService;

    public PropertiesController(PropertiesService propertiesService, AuthorizedContext authorizedContext)
    {
        _authorizedContext = authorizedContext;
        _propertiesService = propertiesService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Properties([FromQuery] PropertyFilters filters)
    {
        try
        {
            var userId = _authorizedContext.GetUserId();
            var properties = await _propertiesService.GetPropertiesAsync(filters, userId);
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

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Property([FromRoute] int id)
    {
        try
        {
            var userId = _authorizedContext.GetUserId();
            var property = await _propertiesService.GetProperty(id, userId);

            if (property == null)
            {
                return NotFound(ApiResponse<PropertyDto>.FailureResponse("Property not found."));
            }

            return Ok(ApiResponse<PropertyDto>.SuccessResponse(property));
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                ApiResponse<ApiResponse<PropertyDto>>.FailureResponse("An error occurred while processing your request")
            );
        }
    }
}
