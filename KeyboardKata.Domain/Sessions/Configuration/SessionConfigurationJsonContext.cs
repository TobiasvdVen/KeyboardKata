using System.Text.Json.Serialization;

namespace KeyboardKata.Domain.Sessions.Configuration
{
    [JsonSerializable(typeof(SessionConfiguration))]
    internal partial class SessionConfigurationJsonContext : JsonSerializerContext
    {

    }
}
