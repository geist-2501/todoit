using System.Text.Json;

namespace ToDoIt.Server.Serialization;

public static class JsonHelper
{
    private static readonly JsonSerializerOptions s_Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    
    public static string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, s_Options);
    }
}