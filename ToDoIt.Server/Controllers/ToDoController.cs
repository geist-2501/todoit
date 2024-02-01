using Microsoft.AspNetCore.Mvc;
using ToDoIt.Server.Models;
using ToDoIt.Server.Serialization;

namespace ToDoIt.Server.Controllers;

[ApiController]
public class ToDoController : ControllerBase
{
    [HttpGet, Route("api/tasks")]
    public IActionResult GetToDos()
    {
        var dummyToDos = new ToDo[]
        {
            new("1", "Take dog out", Priority.Medium, false),
            new("2", "Get shopping", Priority.Low, false),
            new("3", "Clean bathroom and shower unit", Priority.Medium, true),
        };
        return Ok(JsonHelper.Serialize(dummyToDos));
    }
}