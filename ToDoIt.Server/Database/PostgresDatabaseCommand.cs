using System.Data.Common;
using Npgsql;
using ToDoIt.Server.Database.Api;

namespace ToDoIt.Server.Database;

public class PostgresDatabaseCommand : IDatabaseCommand
{
    private readonly NpgsqlCommand m_Command;

    public PostgresDatabaseCommand(NpgsqlCommand command)
    {
        m_Command = command;
    }

    public string CommandText
    {
        get => m_Command.CommandText;
        set => m_Command.CommandText = value;
    }

    public void AddParam<T>(string paramName, T? paramValue)
    {
        object? paramObject = paramValue;
        if (paramValue is Enum)
        {
            throw new Exception("Use AddEnumParam instead for enum parameters");
        }
        
        m_Command.Parameters.Add(new NpgsqlParameter(paramName, paramObject ?? DBNull.Value));
    }

    public void AddEnumParam<T>(string paramName, T enumValue, string pgEnumName) where T : struct
    {
        m_Command.Parameters.Add(new NpgsqlParameter
        {
            ParameterName = paramName,
            DataTypeName = pgEnumName,
            Value = enumValue.ToString()
        });
    }

    public Task<int> ExecuteNonQuery()
    {
        return m_Command.ExecuteNonQueryAsync();
    }

    public async Task<T> ExecuteSingleQuery<T>(Func<IDatabaseReader, T> extractor)
    {
        var reader = await ExecuteQuery();
        if (!await reader.Read())
        {
            throw new DatabaseCommandException("Expected exactly one result, received none");
        }

        var result = extractor(reader);
        
        if (await reader.Read())
        {
            throw new DatabaseCommandException("Expected exactly one result, received multiple");
        }

        return result;
    }

    public async Task<IDatabaseReader> ExecuteQuery()
    {
        var reader = await m_Command.ExecuteReaderAsync();
        return new PostgresDatabaseReader(reader);
    }
}