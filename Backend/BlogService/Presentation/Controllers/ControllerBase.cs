using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected string UserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}