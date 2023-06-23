using KeyboardKata.Domain.InputMatching;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KeyboardKata.Domain.Sessions.Configuration.Json
{
    public class PatternJsonConverter : JsonConverter<IPattern>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(IPattern);
        }

        public override IPattern? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader reset = reader;

            reader.Read();
            string? property = reader.GetString();

            if (property == "inputs")
            {
                reader = reset;

                return JsonSerializer.Deserialize<ExactMatchPattern>(ref reader, options);
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, IPattern value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
