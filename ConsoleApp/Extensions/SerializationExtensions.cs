using System.Text.Json;

namespace ConsoleApp.Extensions
{
    public static class SerializationExtensions
    {
        public static string Serialize(this object obj)
        {
            return obj == null
                ? throw new Exception($"Serialize object shouldn't be null")
                : JsonSerializer.Serialize(obj);
        }
        public static T? Deserialize<T>(this string serializeText)
        {
            return serializeText == null
                ? throw new Exception($"Serialize text shouldn't be null")
                : JsonSerializer.Deserialize<T>(serializeText);
        }
    }
}
