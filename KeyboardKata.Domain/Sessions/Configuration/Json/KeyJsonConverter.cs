using KeyboardKata.Domain.InputProcessing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KeyboardKata.Domain.Sessions.Configuration.Json
{
    internal class KeyJsonConverter : JsonConverter<Key>
    {
        private readonly IKeyCodeMapper _mapper;

        public KeyJsonConverter(IKeyCodeMapper mapper)
        {
            _mapper = mapper;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(Key);
        }

        public override Key? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.Read();
            reader.Read();

            string? keyDescriptor = reader.GetString();

            Key key = _mapper.Key(keyDescriptor!);

            reader.Read();

            return key;
        }

        public override void Write(Utf8JsonWriter writer, Key value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
