namespace ToDoIt.Server.Database.Api;

/// <summary>
/// Executes commands on the database.
/// </summary>
public interface IDatabaseCommandExecutor : IDisposable
{
    Task Execute(Func<IDatabaseCommand, Task> command);
    Task<T> Execute<T>(Func<IDatabaseCommand, Task<T>> command);
}