using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoIt.Server.Serialization;

namespace ToDoIt.Server.Controllers;

public class ProtectedController : ControllerBase
{
    [Authorize]
    [HttpGet("foo")]
    public IActionResult Foo()
    {
        var test = User.Identity;
        return Ok(JsonHelper.Serialize(new
        {
            text = "hello word",
            identity = test
        }));
    }
}