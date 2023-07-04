using KeyboardKata.Domain.Extensions.Json;
using KeyboardKata.Domain.Tests.Extensions.Json.Models;
using System.Text.Json;
using Xunit;

namespace KeyboardKata.Domain.Tests.Extensions.Json
{
    public class TypeDiscriminatorRegistrationTests
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

            TypeDiscriminatorBuilder<IColor> builder = new();
            builder.Register(typeof(BlueImplementation));

            HasColor result = Deserialize<HasColor, IColor>(json, builder.BuildConverter());

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

            TypeDiscriminatorBuilder<IColor> builder = new();
            builder.Register(typeof(BlueImplementation));

            Assert.Throws<JsonException>(() => Deserialize<HasColor, IColor>(json, builder.BuildConverter()));
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

            TypeDiscriminatorBuilder<IColor> builder = new();
            builder.Register(typeof(BlueImplementation));
            builder.Register(typeof(OrangeImplementation));

            HasColor result = Deserialize<HasColor, IColor>(json, builder.BuildConverter());

            Assert.IsType<OrangeImplementation>(result!.SomeInterface);
        }

        [Fact]
        public void GivenRegisteredWithMutation_WhenJsonIsBlue_ThenDeserializeBlue()
        {
            string json = """
                {
                    "someInterface":
                    {
                        "type": "Blue"
                    }
                }
                """;

            TypeDiscriminatorBuilder<IColor> builder = new();

            static string mutator(string discriminator) => discriminator.Replace("Implementation", string.Empty);

            builder.Register(typeof(BlueImplementation), mutator);

            HasColor result = Deserialize<HasColor, IColor>(json, builder.BuildConverter());

            Assert.IsType<BlueImplementation>(result!.SomeInterface);
        }

        [Fact]
        public void GivenDefaultMutator_WhenJsonIsOrange_ThenDeserializeOrange()
        {
            string json = """
                {
                    "someInterface":
                    {
                        "type": "Orange"
                    }
                }
                """;

            TypeDiscriminatorBuilder<IColor> builder = new()
            {
                DefaultMutator = (string discriminator) => discriminator.Replace("Implementation", string.Empty)
            };

            builder.Register(typeof(BlueImplementation));
            builder.Register(typeof(OrangeImplementation));

            HasColor result = Deserialize<HasColor, IColor>(json, builder.BuildConverter());

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
