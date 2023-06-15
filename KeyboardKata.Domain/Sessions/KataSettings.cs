using KeyboardKata.Domain.InputMatching;

namespace KeyboardKata.Domain.Sessions
{
    public class KataSettings
    {
        public required Pattern QuitPattern { get; init; }
    }
}
