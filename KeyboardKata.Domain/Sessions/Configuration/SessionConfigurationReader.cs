using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KeyboardKata.Domain.Sessions.Configuration
{
    public class SessionConfigurationReader
    {
        private readonly IKeyCodeMapper _keyCodeMapper;

        public SessionConfigurationReader(IKeyCodeMapper keyCodeMapper)
        {
            _keyCodeMapper = keyCodeMapper;
        }

        public SessionConfiguration Read(Stream stream)
        {
            try
            {
                JsonSerializerOptions options = new()
                {
                    AllowTrailingCommas = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

                };

                //options.Converters.Add(new TypeDiscriminatorJsonConverter<IPattern>(_keyCodeMapper));
                options.Converters.Add(new JsonDiscriminatorConverter<IPattern>("_type"));
                options.Converters.Add(new JsonStringEnumConverter());
                options.AddContext<SessionConfigurationJsonContext>();

                SessionConfiguration? configuration = JsonSerializer.Deserialize<SessionConfiguration>(stream, options);

                return configuration ?? throw new JsonException("Deserialize returned null.");
            }
            catch (JsonException ex)
            {
                throw new ArgumentException("Failed to deserialize a valid session configuration.", ex);
            }
        }
    }
}
