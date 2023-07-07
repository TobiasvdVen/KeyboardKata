using KeyboardKata.Domain.InputMatching;

namespace KeyboardKata.Domain.Actions.Pools
{
    public record SingleActionPool : KeyboardActionPool
    {
        public SingleActionPool(string prompt, IPattern pattern, int? repeats) : base(Enumerable.Empty<KeyboardActionPool>(), repeats)
        {
            Prompt = prompt;
            Pattern = pattern;

            Action = new KeyboardAction(Prompt, Pattern);
        }

        public string Prompt { get; }
        public IPattern Pattern { get; }

        public KeyboardAction Action { get; }
    }
}
