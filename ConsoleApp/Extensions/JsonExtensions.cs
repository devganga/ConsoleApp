using System.Text.Json;

namespace ConsoleApp.Extensions
{
    public static class JsonExtensions
    {
        //static JsonSerializerOptions jsonSerializerOptions = JsonSerializerOptions.Default;
        public static string JsonSerialize(this object obj)
        {
            var jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //jsonSerializerOptions.PropertyNameCaseInsensitive = propertyNameCaseInsensitive;
            return obj == null
                ? throw new ArgumentNullException($"Serialize object {nameof(obj)} shouldn't be null")
                : JsonSerializer.Serialize(obj, jsonSerializerOptions);
        }
        public static T? JsonDeserialize<T>(this string serializeText, bool propertyNameCaseInsensitive = true)
        {
            var jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.PropertyNameCaseInsensitive = propertyNameCaseInsensitive;
            //jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //gettypeinf
            return serializeText == null
                 ? throw new ArgumentNullException($"Serialize text {nameof(serializeText)} shouldn't be null")
                : JsonSerializer.Deserialize<T>(serializeText, jsonSerializerOptions);
        }
        public static T? JsonDeserialize<T>(this string serializeText, JsonSerializerOptions? jsonSerializerOptions)
        {
            //var jsonSerializerOptions = JsonSerializerOptions.Default;
            //jsonSerializerOptions.PropertyNameCaseInsensitive = propertyNameCaseInsensitive;
            //jsonSerializerOptions.PropertyNameCaseInsensitive

            return serializeText == null
                 ? throw new ArgumentNullException($"Serialize text {nameof(serializeText)} shouldn't be null")
                : JsonSerializer.Deserialize<T>(serializeText, jsonSerializerOptions);
        }
    }
}
