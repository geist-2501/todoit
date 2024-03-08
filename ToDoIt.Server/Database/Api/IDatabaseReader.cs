namespace ToDoIt.Server.Database.Api;

/// <summary>
/// Reads the result of an <see cref="IDatabaseCommand"/>.
/// </summary>
public interface IDatabaseReader : IDisposable
{
    T Get<T>(string name) where T : notnull;
    T? GetOrDefault<T>(string name);
    T GetEnum<T>(string name) where T : struct;
    Guid GetGuid(string name);
    Task<bool> Read();
}