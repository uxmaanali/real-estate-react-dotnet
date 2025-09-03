namespace RealEstate.Controllers;
using Microsoft.AspNetCore.Mvc;

using RealEstate.Services.Properties;
using RealEstate.Shared.Abstraction;
using RealEstate.Shared.Models;
using RealEstate.Shared.Models.Properties;
using RealEstate.Shared.Services.AuthorizedContext;

[Route("api/[controller]")]
[ApiController]
public class PropertiesController : ApiBaseController
{
    private readonly IAuthorizedContextService _authorizedContextService;
    private readonly IPropertiesService _propertiesService;

    public PropertiesController(IPropertiesService propertiesService, IAuthorizedContextService authorizedContextService)
    {
        _authorizedContextService = authorizedContextService;
        _propertiesService = propertiesService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Properties([FromQuery] PropertyFiltersRequestModel filters)
    {
        try
        {
            var properties = await _propertiesService.GetPropertiesAsync(filters);
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

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Property([FromRoute] int id, [FromQuery] BaseModel model)
    {
        try
        {
            model.Id = id;
            var property = await _propertiesService.GetProperty(model);

            if (property == null)
            {
                return NotFound(ApiResponse<PropertyModel>.FailureResponse("Property not found."));
            }

            return Ok(ApiResponse<PropertyModel>.SuccessResponse(property));
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                ApiResponse<ApiResponse<PropertyModel>>.FailureResponse("An error occurred while processing your request")
            );
        }
    }
}
