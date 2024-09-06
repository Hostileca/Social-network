using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected string UserId => "123";
}