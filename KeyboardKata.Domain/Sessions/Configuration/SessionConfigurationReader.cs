using KeyboardKata.Domain.Actions.Pools;
using KeyboardKata.Domain.Extensions.Json;
using KeyboardKata.Domain.Extensions.Nullability;
using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Sessions.Configuration.Json;
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
                JsonSerializerOptions options = BuildJsonOptions();

                SessionConfiguration? configuration = JsonSerializer.Deserialize<SessionConfiguration>(stream, options);

                if (configuration is null)
                {
                    throw new JsonException("Deserialize returned null.");
                }

                if (!configuration.HasNullIntegrity(out string errorSummary))
                {
                    throw new JsonException(errorSummary);
                }

                return configuration;
            }
            catch (JsonException ex)
            {
                throw new ArgumentException("Failed to deserialize a valid session configuration.", ex);
            }
        }

        private JsonSerializerOptions BuildJsonOptions()
        {
            JsonSerializerOptions options = new()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new KeyJsonConverter(_keyCodeMapper));

            TypeDiscriminatorBuilder<IPattern> patternDiscriminator = new()
            {
                DefaultMutator = (m) => m.Replace("Pattern", string.Empty)
            };

            patternDiscriminator.Register(typeof(ExactMatchPattern));

            TypeDiscriminatorBuilder<KeyboardActionPool> poolDiscriminator = new()
            {
                DefaultMutator = (m) => m.Replace("ActionPool", string.Empty)
            };

            poolDiscriminator.Register(typeof(SingleActionPool));
            poolDiscriminator.Register(typeof(LinearActionPool));

            options.Converters.Add(patternDiscriminator.BuildConverter());
            options.Converters.Add(poolDiscriminator.BuildConverter());

            options.AddContext<SessionConfigurationJsonContext>();

            return options;
        }
    }
}
