namespace RealEstate.Controllers;
using Microsoft.AspNetCore.Mvc;

using RealEstate.Actions;

[ServiceFilter(typeof(PopulateBaseDtoActionFilter))]
public class ApiBaseController : ControllerBase
{

}
