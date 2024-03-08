using Npgsql;
using ToDoIt.Server.Database.Api;

namespace ToDoIt.Server.Database;

public class PostgresDatabaseReader : IDatabaseReader
{
    private readonly NpgsqlDataReader m_Reader;

    public PostgresDatabaseReader(NpgsqlDataReader reader)
    {
        m_Reader = reader;
    }

    public T Get<T>(string name) where T : notnull
    {
        var value = m_Reader[name];

        return (T)value;
    }

    public T? GetOrDefault<T>(string name)
    {
        var value = m_Reader[name];

        if (value is DBNull)
        {
            return default;
        }

        return (T)value;
    }

    public T GetEnum<T>(string name) where T : struct
    {
        var value = (string)m_Reader[name];

        return Enum.Parse<T>(value);
    }

    public Guid GetGuid(string name)
    {
        var value = (Guid)m_Reader[name];

        return value;
    }

    public Task<bool> Read()
    {
        return m_Reader.ReadAsync();
    }
    
    public void Dispose()
    {
        m_Reader.Dispose();
    }
}