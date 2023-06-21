using System.Text.Json.Serialization;

namespace KeyboardKata.Domain.Sessions
{
    [JsonSerializable(typeof(SessionResult))]
    public partial class SessionResultJsonContext : JsonSerializerContext
    {

    }
}
