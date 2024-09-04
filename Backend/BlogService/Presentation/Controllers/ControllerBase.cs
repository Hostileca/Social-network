using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    public string UserId => "123";
}