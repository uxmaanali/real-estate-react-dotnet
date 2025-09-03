namespace RealEstate.Actions;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Filters;

using RealEstate.Shared.Abstraction;
using RealEstate.Shared.Services.AuthorizedContext;

public class PopulateBaseDtoActionFilter : IAsyncActionFilter, IScopedDependency
{
    private readonly IAuthorizedContextService _authorizedContextService;

    public PopulateBaseDtoActionFilter(IAuthorizedContextService authorizedContextService)
    {
        _authorizedContextService = authorizedContextService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;

        var cancellationToken = httpContext.RequestAborted;
        var userId = _authorizedContextService.GetUserId();

        var baseDto = context.ActionArguments.Values.OfType<BaseModel>().FirstOrDefault();

        if (baseDto is not null)
        {
            baseDto.CancellationToken = httpContext.RequestAborted;
            baseDto.UserId = userId;
        }

        await next();
    }
}
