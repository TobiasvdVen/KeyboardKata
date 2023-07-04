using KeyboardKata.Domain.Extensions.Json;
using KeyboardKata.Domain.Tests.Extensions.Json.Models;
using System.Text.Json;
using Xunit;

namespace KeyboardKata.Domain.Tests.Extensions.Json
{
    public class TypeDiscriminatorJsonConverterTests
    {
        [Fact]
        public void GivenBlueRegistered_WhenJsonIsBlue_ThenDeserializeBlue()
        {
            string json = """
                {
                    "someInterface":
                    {
                        "type": "BlueImplementation"
                    }
                }
                """;

            TypeDiscriminatorBuilder<ISomeInterface> builder = new();
            builder.Register(typeof(BlueImplementation));

            HasSomeInterface result = Deserialize<HasSomeInterface, ISomeInterface>(json, builder.BuildConverter());

            Assert.IsType<BlueImplementation>(result!.SomeInterface);
        }

        [Fact]
        public void GivenOnlyBlueRegistered_WhenJsonIsOrange_ThenThrow()
        {
            string json = """
                {
                    "someInterface":
                    {
                        "type": "OrangeImplementation"
                    }
                }
                """;

            TypeDiscriminatorBuilder<ISomeInterface> builder = new();
            builder.Register(typeof(BlueImplementation));

            Assert.Throws<JsonException>(() => Deserialize<HasSomeInterface, ISomeInterface>(json, builder.BuildConverter()));
        }

        [Fact]
        public void GivenOrangeAndBlueRegistered_WhenJsonIsOrange_ThenDeserializeOrange()
        {
            string json = """
                {
                    "someInterface":
                    {
                        "type": "OrangeImplementation"
                    }
                }
                """;

            TypeDiscriminatorBuilder<ISomeInterface> builder = new();
            builder.Register(typeof(BlueImplementation));
            builder.Register(typeof(OrangeImplementation));

            HasSomeInterface result = Deserialize<HasSomeInterface, ISomeInterface>(json, builder.BuildConverter());

            Assert.IsType<OrangeImplementation>(result!.SomeInterface);
        }

        private TDeserialize Deserialize<TDeserialize, TDiscrimated>(string json, TypeDiscriminatorJsonConverter<TDiscrimated> converter) where TDiscrimated : class
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
