namespace ToDoIt.Server.Database.Api;

public class DatabaseCommandException : Exception
{
    public DatabaseCommandException(string? message) : base(message)
    {
    }
}