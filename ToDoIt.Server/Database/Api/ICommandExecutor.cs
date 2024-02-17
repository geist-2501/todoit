using Npgsql;

namespace ToDoIt.Server.Database.Api;

public interface ICommandExecutor : IDisposable
{
    Task Execute(Func<NpgsqlCommand, Task> command);
    Task<T> Execute<T>(Func<NpgsqlCommand, Task<T>> command);
}