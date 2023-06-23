using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Text.Json.Polymorphism
{
    public class NullableJsonConverter : JsonConverter<object>
    {
        private readonly JsonSerializerOptions _defaultOptions;

        public NullableJsonConverter(JsonSerializerOptions defaultOptions)
        {
            _defaultOptions = defaultOptions;
        }

        public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            object? value = JsonSerializer.Deserialize(ref reader, typeToConvert, _defaultOptions);

            PropertyInfo[] properties = typeToConvert.GetProperties();

            if (properties.Any(p => p.GetValue(value) is null && !p.IsNullable()))
            {
                throw new InvalidOperationException($"Property was null.");
            }

            return value;
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }
    }
}
