using Npgsql;
using ToDoIt.Server.Database.Api;
using ToDoIt.Server.Models;
using ToDoIt.Server.Stores.Api;

namespace ToDoIt.Server.Stores;

public class PostgresToDoStore : IToDoStore
{
    private readonly ICommandExecutor m_Executor;

    public PostgresToDoStore(ICommandExecutor executor)
    {
        m_Executor = executor;
    }

    public async Task<IEnumerable<ToDo>> GetToDos()
    {
        return await m_Executor.Execute(async cmd =>
        {
            cmd.CommandText = "SELECT id, description, priority, done FROM todos;";

            var reader = await cmd.ExecuteReaderAsync();

            var toDos = new List<ToDo>();
            while (await reader.ReadAsync())
            {
                var id = reader.GetGuid(0);
                var description = reader.GetString(1);
                var priority = Enum.Parse<Priority>(reader.GetString(2));
                var done = reader.GetBoolean(3);
                toDos.Add(new ToDo(id, description, priority, done));
            }

            return toDos;
        });
    }

    public async Task SaveToDo(ToDo toDo)
    {
        await m_Executor.Execute(async cmd =>
        {
            cmd.CommandText = """
                              INSERT INTO todos(id, description, priority, done) 
                              VALUES (@id_param, @description_param, @priority_param, @done_param)
                              """;

            cmd.Parameters.Add(new NpgsqlParameter("id_param", toDo.Id));
            cmd.Parameters.Add(new NpgsqlParameter("description_param", toDo.Description));
            cmd.Parameters.Add(new NpgsqlParameter("priority_param", toDo.Priority));
            cmd.Parameters.Add(new NpgsqlParameter("done_param", toDo.Done));

            await cmd.ExecuteNonQueryAsync();
        });
    }
}