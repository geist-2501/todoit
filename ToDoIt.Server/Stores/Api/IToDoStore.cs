using ToDoIt.Server.Models;

namespace ToDoIt.Server.Stores.Api;

public interface IToDoStore
{
    public Task<IEnumerable<ToDo>> GetToDos();
}