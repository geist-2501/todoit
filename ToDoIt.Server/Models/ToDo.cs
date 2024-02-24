namespace ToDoIt.Server.Models;

public record ToDo(
    Guid Id,
    string Description,
    Priority Priority,
    bool Done
);