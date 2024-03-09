using Microsoft.AspNetCore.Mvc;
using ToDoIt.Server.Models;
using ToDoIt.Server.Serialization;
using ToDoIt.Server.Services.Api;
using ToDoIt.Server.Stores.Api;

namespace ToDoIt.Server.Controllers;

[ApiController]
public class ToDoController : ControllerBase
{
    private readonly IToDoService m_ToDoService;

    public ToDoController(IToDoService toDoService)
    {
        m_ToDoService = toDoService;
    }

    [HttpGet, Route("api/tasks")]
    public async Task<IActionResult> GetToDos()
    {
        var toDos = await m_ToDoService.GetAllToDos();
        return Ok(JsonHelper.Serialize(toDos));
    }

    [HttpPost, Route("api/tasks")]
    public async Task<IActionResult> NewToDo([FromBody] CreateToDoRequest request)
    {
        var newToDo = new ToDo(Guid.NewGuid(), request.Description, request.Priority, false);
        await m_ToDoService.SaveToDo(newToDo);
        return Ok(JsonHelper.Serialize(newToDo));
    }

    [HttpPut, Route("api/tasks/{toDoIdString}/done")]
    public async Task<IActionResult> MarkToDoAsDone(string toDoIdString)
    {
        if (!Guid.TryParse(toDoIdString, out var toDoId))
        {
            return BadRequest();
        }

        var updatedToDo = await m_ToDoService.MarkToDoAsDone(toDoId);

        return Ok(JsonHelper.Serialize(updatedToDo));
    }
}