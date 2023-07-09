using System.Text.Json;
using System.Text.Json.Serialization;

namespace KeyboardKata.Domain.Extensions.Json
{
    public class TypeDiscriminatorJsonConverter<T> : JsonConverter<T> where T : class
    {
        private readonly string _typeDiscriminatorIdentifier;
        private readonly ITypeDiscriminatorMap _discriminatorMap;

        public TypeDiscriminatorJsonConverter(string typeDiscriminatorIdentifier, ITypeDiscriminatorMap discriminatorMap)
        {
            _typeDiscriminatorIdentifier = typeDiscriminatorIdentifier;
            _discriminatorMap = discriminatorMap;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            bool canConvert = typeToConvert == typeof(T);

            return canConvert;
        }

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader cached = reader;

            string? discriminator = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    if (reader.ValueTextEquals(_typeDiscriminatorIdentifier))
                    {
                        reader.Read();
                        discriminator = reader.GetString();
                        break;
                    }
                }

                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }
            }

            if (discriminator is null)
            {
                throw new JsonException($"Unable to find type discriminator identified by: {_typeDiscriminatorIdentifier}.");
            }

            if (!_discriminatorMap.CanResolve(discriminator))
            {
                throw new JsonException($"Unable to resolve discriminator \"{discriminator}\" when deserializing type: {typeToConvert.Name}.");
            }

            Type type = _discriminatorMap.ResolveType(discriminator);

            reader = cached;
            object? result = JsonSerializer.Deserialize(ref reader, type, options);

            return result as T;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
