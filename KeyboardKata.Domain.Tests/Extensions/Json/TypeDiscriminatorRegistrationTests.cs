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
                    "color":
                    {
                        "type": "BlueImplementation"
                    }
                }
                """;

            TypeDiscriminatorBuilder<IColor> builder = new();
            builder.Register(typeof(BlueImplementation));

            HasColor result = JsonAssert.Deserialize<HasColor, IColor>(json, builder.BuildConverter());

            Assert.IsType<BlueImplementation>(result!.Color);
        }

        [Fact]
        public void GivenOnlyBlueRegistered_WhenJsonIsOrange_ThenThrow()
        {
            string json = """
                {
                    "color":
                    {
                        "type": "OrangeImplementation"
                    }
                }
                """;

            TypeDiscriminatorBuilder<IColor> builder = new();
            builder.Register(typeof(BlueImplementation));

            Assert.Throws<JsonException>(() => JsonAssert.Deserialize<HasColor, IColor>(json, builder.BuildConverter()));
        }

        [Fact]
        public void GivenOrangeAndBlueRegistered_WhenJsonIsOrange_ThenDeserializeOrange()
        {
            string json = """
                {
                    "color":
                    {
                        "type": "OrangeImplementation"
                    }
                }
                """;

            TypeDiscriminatorBuilder<IColor> builder = new();
            builder.Register(typeof(BlueImplementation));
            builder.Register(typeof(OrangeImplementation));

            HasColor result = JsonAssert.Deserialize<HasColor, IColor>(json, builder.BuildConverter());

            Assert.IsType<OrangeImplementation>(result!.Color);
        }

        [Fact]
        public void GivenRegisteredWithMutation_WhenJsonIsBlue_ThenDeserializeBlue()
        {
            string json = """
                {
                    "color":
                    {
                        "type": "Blue"
                    }
                }
                """;

            TypeDiscriminatorBuilder<IColor> builder = new();

            static string mutator(string discriminator) => discriminator.Replace("Implementation", string.Empty);

            builder.Register(typeof(BlueImplementation), mutator);

            HasColor result = JsonAssert.Deserialize<HasColor, IColor>(json, builder.BuildConverter());

            Assert.IsType<BlueImplementation>(result!.Color);
        }

        [Fact]
        public void GivenDefaultMutator_WhenJsonIsOrange_ThenDeserializeOrange()
        {
            string json = """
                {
                    "color":
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

            HasColor result = JsonAssert.Deserialize<HasColor, IColor>(json, builder.BuildConverter());

            Assert.IsType<OrangeImplementation>(result!.Color);
        }
    }
}
