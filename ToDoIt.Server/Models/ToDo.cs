namespace ToDoIt.Server.Models;

public record ToDo(
    string Id,
    string Description,
    Priority Priority,
    bool Done
);