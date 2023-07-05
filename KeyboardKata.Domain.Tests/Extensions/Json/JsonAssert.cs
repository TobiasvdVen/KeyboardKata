using KeyboardKata.Domain.Extensions.Json;
using System.Text.Json;
using Xunit;

namespace KeyboardKata.Domain.Tests.Extensions.Json
{
    public static class JsonAssert
    {
        public static TDeserialize Deserialize<TDeserialize, TDiscrimated>(string json, TypeDiscriminatorJsonConverter<TDiscrimated> converter) where TDiscrimated : class
        {
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            options.Converters.Add(converter);

            TDeserialize? result = JsonSerializer.Deserialize<TDeserialize>(json, options);

            Assert.NotNull(result);

            return result;
        }
    }
}
