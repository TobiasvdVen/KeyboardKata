namespace KeyboardKata.Domain
{
    public class Session : IKataSession, IInputProcessor
    {
        private readonly IKeyboardKata _kata;
        private readonly IKeyboardActionProvider _keyboardActionProvider;

        public Session(IKeyboardKata kata, IKeyboardActionProvider keyboardActionProvider)
        {
            _kata = kata;
            _keyboardActionProvider = keyboardActionProvider;
        }

        public void NextPrompt()
        {
            KeyboardAction next = _keyboardActionProvider.GetKeyboardAction();
            _kata.Prompt(next);
        }

        public InputContinuation Process(Input input)
        {
            throw new NotImplementedException();
        }
    }
}
