using KeyboardKata.Domain.Extensions.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace KeyboardKata.Domain.Tests.Extensions.Json
{
    public static class JsonAssert
    {
        public static TDeserialize Deserialize<TDeserialize, TDiscrimated>(string json, TypeDiscriminatorJsonConverter<TDiscrimated> converter) where TDiscrimated : class
        {
            return Deserialize<TDeserialize>(json, converter);
        }

        public static TDeserialize Deserialize<TDeserialize>(string json, params JsonConverter[] converters)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            foreach (JsonConverter converter in converters)
            {
                options.Converters.Add(converter);
            }

            TDeserialize? result = JsonSerializer.Deserialize<TDeserialize>(json, options);

            Assert.NotNull(result);

            return result;
        }
    }
}
