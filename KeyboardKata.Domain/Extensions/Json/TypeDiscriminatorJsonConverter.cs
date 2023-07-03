using System.Text.Json;
using System.Text.Json.Serialization;

namespace KeyboardKata.Domain.Extensions.Json
{
    public class TypeDiscriminatorJsonConverter<T> : JsonConverter<T> where T : class
    {
        private readonly ITypeDiscriminatorMap _discriminatorMap;

        public TypeDiscriminatorJsonConverter(ITypeDiscriminatorMap discriminatorMap)
        {
            _discriminatorMap = discriminatorMap;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(T);
        }

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader copy = reader;

            copy.Read();
            copy.Read();
            string? discriminator = copy.GetString();

            if (discriminator is null)
            {
                return null;
            }

            Type type = _discriminatorMap.ResolveType(discriminator);

            object? result = JsonSerializer.Deserialize(ref reader, type, options);

            return result as T;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
