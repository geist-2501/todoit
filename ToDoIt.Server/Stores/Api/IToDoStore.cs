﻿using ToDoIt.Server.Models;

namespace ToDoIt.Server.Stores.Api;

public interface IToDoStore
{
    public Task<IEnumerable<ToDo>> GetToDos();
    public Task SaveToDo(ToDo toDo);
    public Task<ToDo> UpdateToDo(ToDo toDo);
    Task<ToDo> GetToDo(Guid toDoId);
}