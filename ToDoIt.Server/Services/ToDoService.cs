using ToDoIt.Server.Models;
using ToDoIt.Server.Services.Api;
using ToDoIt.Server.Stores.Api;

namespace ToDoIt.Server.Services;

public class ToDoService : IToDoService
{
    private readonly IToDoStore m_ToDoStore;

    public ToDoService(IToDoStore toDoStore)
    {
        m_ToDoStore = toDoStore;
    }

    public Task<IEnumerable<ToDo>> GetAllToDos()
    {
        return m_ToDoStore.GetToDos();
    }

    public async Task SaveToDo(ToDo newToDo)
    {
        await m_ToDoStore.SaveToDo(newToDo);
    }

    public async Task<ToDo> MarkToDoAsDone(Guid toDoId)
    {
        var existingToDo = await m_ToDoStore.GetToDo(toDoId);
        var completedToDo = existingToDo with { Done = true };
        return await m_ToDoStore.UpdateToDo(completedToDo);
    }
}