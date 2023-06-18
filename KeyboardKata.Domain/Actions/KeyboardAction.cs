using KeyboardKata.Domain.InputMatching;

namespace KeyboardKata.Domain.Actions
{
    public record KeyboardAction(string Prompt, IPattern Pattern) : IKeyboardActionSource
    {
        public KeyboardAction GetKeyboardAction() => this;
    }
}
