using KeyboardKata.Domain.Actions.Pools;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KeyboardKata.Domain.Sessions.Configuration.Json
{
    public class KeyboardActionPoolJsonConverter : JsonConverter<KeyboardActionPool>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(KeyboardActionPool);
        }

        public override KeyboardActionPool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader reset = reader;

            reader.Read();
            string? property = reader.GetString();

            if (property == "prompt")
            {
                reader = reset;

                return JsonSerializer.Deserialize<SingleActionPool>(ref reader, options);
            }
            else if (property == "linear")
            {
                reader = reset;

                return JsonSerializer.Deserialize<LinearActionPool>(ref reader, options);
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, KeyboardActionPool value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
