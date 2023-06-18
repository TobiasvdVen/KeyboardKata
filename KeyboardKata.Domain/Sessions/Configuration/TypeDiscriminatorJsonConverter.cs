using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KeyboardKata.Domain.Sessions.Configuration
{
    internal class TypeDiscriminatorJsonConverter<T> : JsonConverter<T>
    {
        private readonly IKeyCodeMapper _mapper;

        public TypeDiscriminatorJsonConverter(IKeyCodeMapper mapper)
        {
            _mapper = mapper;
        }

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader cachedReader = reader;

            reader.Read();
            string? propertyName = reader.GetString();

            if (propertyName != "_type")
            {
                throw new JsonException("First property was not a type discriminator!");
            }

            reader.Read();

            string? discriminator = reader.GetString();

            if (discriminator == "ExactMatch")
            {

            }

            JsonSerializerOptions nonDiscriminatorOptions = new()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            nonDiscriminatorOptions.Converters.Add(new KeyJsonConverter(_mapper));
            nonDiscriminatorOptions.Converters.Add(new JsonStringEnumConverter());

            IPattern? pattern = JsonSerializer.Deserialize<ExactMatchPattern>(ref cachedReader, nonDiscriminatorOptions);

            reader = cachedReader;
            return (T)pattern;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(IPattern);
        }
    }
}
