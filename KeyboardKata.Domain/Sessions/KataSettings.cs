using KeyboardKata.Domain.InputMatching;

namespace KeyboardKata.Domain.Sessions
{
    public class KataSettings
    {
        public required IPattern QuitPattern { get; init; }
    }
}
