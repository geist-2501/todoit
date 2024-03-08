using Microsoft.AspNetCore.Mvc;
using ToDoIt.Server.Models;
using ToDoIt.Server.Serialization;
using ToDoIt.Server.Stores.Api;

namespace ToDoIt.Server.Controllers;

[ApiController]
public class ToDoController : ControllerBase
{
    private readonly IToDoStore m_ToDoStore;

    public ToDoController(IToDoStore toDoStore)
    {
        m_ToDoStore = toDoStore;
    }

    [HttpGet, Route("api/tasks")]
    public async Task<IActionResult> GetToDos()
    {
        var toDos = await m_ToDoStore.GetToDos();
        return Ok(JsonHelper.Serialize(toDos));
    }

    [HttpPost, Route("api/tasks")]
    public async Task<IActionResult> NewToDo([FromBody] CreateToDoRequest request)
    {
        var newToDo = new ToDo(Guid.NewGuid(), request.Description, request.Priority, false);
        await m_ToDoStore.SaveToDo(newToDo);
        return Ok(JsonHelper.Serialize(newToDo));
    }
}