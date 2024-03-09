using ToDoIt.Server.Models;

namespace ToDoIt.Server.Services.Api;

public interface IToDoService
{
    Task<IEnumerable<ToDo>> GetAllToDos();
    Task SaveToDo(ToDo newToDo);
    Task<ToDo> MarkToDoAsDone(Guid toDoId);
}