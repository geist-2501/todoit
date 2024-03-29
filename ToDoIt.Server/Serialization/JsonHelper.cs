using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToDoIt.Server.Serialization;

public static class JsonHelper
{
    private static readonly JsonSerializerOptions s_Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = {         
            new JsonStringEnumConverter()
        }
    };
    
    public static string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, s_Options);
    }
}