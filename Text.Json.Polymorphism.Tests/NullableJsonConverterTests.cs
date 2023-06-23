using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Text.Json.Polymorphism.Tests.Models;
using Xunit;

namespace Text.Json.Polymorphism.Tests
{
    public class NullableJsonConverterTests
    {
        private readonly JsonSerializerOptions _options;
        private readonly JsonSerializerOptions _defaultOptions;
        public NullableJsonConverterTests()
        {
            InitializeOptions(out _options);
            InitializeOptions(out _defaultOptions);

            _options.Converters.Add(new NullableJsonConverter(_defaultOptions));
        }

        [Fact]
        public void GivenJsonWithMissingProperty_WhenDeserializedAsSimple_ThenThrow()
        {
            string json = "{}";

            Assert.Throws<InvalidOperationException>(() => JsonSerializer.Deserialize<Simple>(json, _options));
        }

        [Fact]
        public void GivenJsonWithPropertyIncluded_WhenDeserializedAsSimple_ThenSucceed()
        {
            string json = """
                {
                    "value": "Hello"
                }
                """;

            Simple? result = JsonSerializer.Deserialize<Simple>(json, _options);

            Assert.Equal("Hello", result!.Value);
        }

        private void InitializeOptions([NotNull] out JsonSerializerOptions? options)
        {
            options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
    }
}
