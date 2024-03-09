using ToDoIt.Server.Database.Api;
using ToDoIt.Server.Logging;
using ToDoIt.Server.Models;
using ToDoIt.Server.Stores.Api;
using ILogger = Serilog.ILogger;

namespace ToDoIt.Server.Stores;

public class PostgresToDoStore : IToDoStore
{
    private readonly ILogger m_Logger = LogManager.GetLogger(nameof(PostgresToDoStore));
    
    private readonly IDatabaseCommandExecutor m_Executor;

    public PostgresToDoStore(IDatabaseCommandExecutor executor)
    {
        m_Executor = executor;
    }

    public async Task<IEnumerable<ToDo>> GetToDos()
    {
        m_Logger.Information("Foo");
        return await m_Executor.Execute(async cmd =>
        {
            cmd.CommandText = "SELECT id, description, priority, done FROM todos;";

            var reader = await cmd.ExecuteQuery();

            var toDos = new List<ToDo>();
            while (await reader.Read())
            {
                var id = reader.GetGuid("id");
                var description = reader.Get<string>("description");
                var priority = reader.GetEnum<Priority>("priority");
                var done = reader.Get<bool>("done");
                
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

            cmd.AddParam("id_param", toDo.Id);
            cmd.AddParam("description_param", toDo.Description);
            cmd.AddEnumParam("priority_param", toDo.Priority, "priority_enum");
            cmd.AddParam("done_param", toDo.Done);

            await cmd.ExecuteNonQuery();
        });
    }
}