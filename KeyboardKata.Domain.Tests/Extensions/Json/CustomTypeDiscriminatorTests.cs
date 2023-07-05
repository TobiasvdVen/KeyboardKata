using KeyboardKata.Domain.Extensions.Json;
using KeyboardKata.Domain.Tests.Extensions.Json.Models;
using System.Text.Json;
using Xunit;

namespace KeyboardKata.Domain.Tests.Extensions.Json
{
    public class CustomTypeDiscriminatorTests
    {
        private readonly TypeDiscriminatorBuilder<IComponent> _builder;

        public CustomTypeDiscriminatorTests()
        {
            _builder = new TypeDiscriminatorBuilder<IComponent>();

            _builder.Register(typeof(TextComponent));
            _builder.Register(typeof(NumberComponent));
        }

        [Fact]
        public void GivenCustomDescriminator_WhenJsonText_ThenDeserializeText()
        {
            string json = """
                {
                    "someComponent":
                    {
                        "component": "TextComponent",
                        "text": "Hello world!"
                    }
                }
                """;

            _builder.TypeDiscriminatorIdentifier = "component";

            HasComponent result = JsonAssert.Deserialize<HasComponent, IComponent>(json, _builder.BuildConverter());

            Assert.IsType<TextComponent>(result.SomeComponent);
        }

        [Fact]
        public void GivenDefaultDiscriminator_WhenJsonMissingDiscriminator_ThenThrow()
        {
            string json = """
                {
                    "someComponent":
                    {
                        "text": "Hello world!"
                    }
                }
                """;

            Assert.Throws<JsonException>(() => JsonAssert.Deserialize<HasComponent, IComponent>(json, _builder.BuildConverter()));
        }

        [Fact]
        public void GivenCustomDiscriminator_WhenJsonMissingDiscriminator_ThenThrow()
        {
            string json = """
                {
                    "someComponent":
                    {
                        "type": "TextComponent",
                        "text": "Hello world!"
                    }
                }
                """;

            _builder.TypeDiscriminatorIdentifier = "custom";

            JsonException ex = Assert.Throws<JsonException>(() => JsonAssert.Deserialize<HasComponent, IComponent>(json, _builder.BuildConverter()));
            Assert.Contains(_builder.TypeDiscriminatorIdentifier, ex.Message);
        }

        [Fact]
        public void GivenDefaultDiscriminator_WhenDiscriminatorIsNotFirstInJson_ThenDeserialize()
        {
            string json = """
                {
                    "someComponent":
                    {
                        "text": "Hello world!",
                        "component": "NumberComponent"
                    }
                }
                """;

            _builder.TypeDiscriminatorIdentifier = "component";

            HasComponent result = JsonAssert.Deserialize<HasComponent, IComponent>(json, _builder.BuildConverter());

            Assert.IsType<NumberComponent>(result.SomeComponent);
        }
    }
}
