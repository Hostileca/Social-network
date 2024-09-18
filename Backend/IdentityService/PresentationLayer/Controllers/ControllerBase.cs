using System.Security.Claims;

namespace PresentationLayer.Controllers;

public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected string UserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}