using KeyboardKata.Domain.Actions.Pools;
using KeyboardKata.Domain.InputMatching;
using System.Text.Json.Serialization;

namespace KeyboardKata.Domain.Sessions.Configuration
{
    [JsonSerializable(typeof(SessionConfiguration))]
    [JsonSerializable(typeof(SingleActionPool))]
    [JsonSerializable(typeof(ExactMatchPattern))]
    internal partial class SessionConfigurationJsonContext : JsonSerializerContext
    {

    }
}
