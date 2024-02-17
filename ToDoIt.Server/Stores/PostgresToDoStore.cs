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
            cmd.CommandText = """
                SELECT * FROM to_dos;
                """;

            var reader = await cmd.ExecuteReaderAsync();

            var toDos = new List<ToDo>();
            while (await reader.ReadAsync())
            {
                toDos.Add(new ToDo("1", "lol", Priority.Low, false));
            }

            return toDos;
        });
    }
}