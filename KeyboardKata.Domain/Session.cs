namespace KeyboardKata.Domain
{
    public class Session : IKataSession, IInputProcessor
    {
        private readonly IKeyboardKata _kata;

        public Session(IKeyboardKata kata)
        {
            _kata = kata;
        }

        public void NextPrompt()
        {
            //_kata.Prompt(new KeyboardAction("Do something!", Stubs.Linear("Ctrl", "V")));
        }

        InputContinuation IInputProcessor.Process(Input input)
        {
            throw new NotImplementedException();
        }
    }
}
