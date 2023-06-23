using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Sessions.Configuration.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Text.Json.Polymorphism;

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
                JsonSerializerOptions options = BuildJsonOptions();

                JsonSerializerOptions nullableSafeOptions = BuildJsonOptions(o =>
                {
                    o.Converters.Add(new NullableJsonConverter(options));
                });

                SessionConfiguration? configuration = JsonSerializer.Deserialize<SessionConfiguration>(stream, nullableSafeOptions);

                return configuration ?? throw new JsonException("Deserialize returned null.");
            }
            catch (JsonException ex)
            {
                throw new ArgumentException("Failed to deserialize a valid session configuration.", ex);
            }
        }

        private JsonSerializerOptions BuildJsonOptions(Action<JsonSerializerOptions>? customize = null)
        {
            JsonSerializerOptions options = new()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new KeyJsonConverter(_keyCodeMapper));
            options.Converters.Add(new KeyboardActionPoolJsonConverter());
            options.Converters.Add(new PatternJsonConverter());

            if (customize is not null)
            {
                customize(options);
            }

            options.AddContext<SessionConfigurationJsonContext>();

            return options;
        }
    }
}
