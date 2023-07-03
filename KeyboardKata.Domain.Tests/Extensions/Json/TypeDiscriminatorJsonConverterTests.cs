using KeyboardKata.Domain.Extensions.Json;
using KeyboardKata.Domain.Tests.Extensions.Json.Models;
using System.Text.Json;
using Xunit;

namespace KeyboardKata.Domain.Tests.Extensions.Json
{
    public class TypeDiscriminatorJsonConverterTests
    {
        private readonly JsonSerializerOptions _options;

        public TypeDiscriminatorJsonConverterTests()
        {
            _options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            TypeDiscriminatorBuilder<ISomeInterface> builder = new();

            builder.Register(typeof(BlueImplementation), "BlueImplementation");

            TypeDiscriminatorJsonConverter<ISomeInterface> converter = builder.BuildConverter();

            _options.Converters.Add(converter);
        }

        [Fact]
        public void GivenBlue_ThenDeserializeAsBlue()
        {
            string json = """
                {
                    "someInterface":
                    {
                        "type": "BlueImplementation"
                    }
                }
                """;

            HasSomeInterface? result = JsonSerializer.Deserialize<HasSomeInterface>(json, _options);

            BlueImplementation blue = Assert.IsType<BlueImplementation>(result!.SomeInterface);
        }
    }
}
