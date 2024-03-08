using Npgsql;
using ToDoIt.Server.Database.Api;

namespace ToDoIt.Server.Database;

public class PostgresDatabaseCommandExecutor : IDatabaseCommandExecutor
{
    private const string c_ConnectionUrl = "Host=localhost;Username=bx_admin;Password=nojXGZDdTeTuj3ZrcYYE;Database=todoit;"; // TODO (BC) replace.

    private readonly NpgsqlDataSource m_NpgsqlDataSource = NpgsqlDataSource.Create(c_ConnectionUrl);

    public async Task Execute(Func<IDatabaseCommand, Task> command)
    {
        await using var connection = m_NpgsqlDataSource.CreateConnection();
        await connection.OpenAsync();
        await using var cmd = connection.CreateCommand();
        var databaseCommand = new PostgresDatabaseCommand(cmd);
        
        await command(databaseCommand);
    }

    public async Task<T> Execute<T>(Func<IDatabaseCommand, Task<T>> command)
    {
        await using var connection = m_NpgsqlDataSource.CreateConnection();
        await connection.OpenAsync();
        await using var cmd = connection.CreateCommand();
        var databaseCommand = new PostgresDatabaseCommand(cmd);

        return await command(databaseCommand);
    }

    public void Dispose()
    {
        m_NpgsqlDataSource.Dispose();
    }
}