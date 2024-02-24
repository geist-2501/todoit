using Npgsql;
using ToDoIt.Server.Database.Api;

namespace ToDoIt.Server.Database;

public class PostgresCommandExecutor : ICommandExecutor
{
    private const string c_ConnectionUrl = "Host=localhost;Username=bx_admin;Password=nojXGZDdTeTuj3ZrcYYE"; // TODO (BC) replace.

    private readonly NpgsqlDataSource m_NpgsqlDataSource = NpgsqlDataSource.Create(c_ConnectionUrl);

    public async Task Execute(Func<NpgsqlCommand, Task> command)
    {
        await using var connection = m_NpgsqlDataSource.CreateConnection();
        await using var cmd = connection.CreateCommand();
        
        await command(cmd);
    }

    public async Task<T> Execute<T>(Func<NpgsqlCommand, Task<T>> command)
    {
        await using var connection = m_NpgsqlDataSource.CreateConnection();
        await using var cmd = connection.CreateCommand();
        
        return await command(cmd);
    }

    public void Dispose()
    {
        m_NpgsqlDataSource.Dispose();
    }
}