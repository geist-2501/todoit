namespace ToDoIt.Server.Database.Api;

/// <summary>
/// Represents a command to enact on the database. Gets executed by the <see cref="IDatabaseCommandExecutor"/>.
/// </summary>
public interface IDatabaseCommand
{
    public string CommandText { get; set; }

    public void AddParam<T>(string paramName, T? paramValue);
    public void AddEnumParam<T>(string paramName, T enumValue, string pgEnumName) where T : struct;
    public Task<int> ExecuteNonQuery();
    public Task<T> ExecuteSingleQuery<T>(Func<IDatabaseReader, T> extractor);
    public Task<IDatabaseReader> ExecuteQuery();
}