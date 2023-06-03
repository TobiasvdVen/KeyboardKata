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
            throw new NotImplementedException();
        }

        InputContinuation IInputProcessor.Process(Input input)
        {
            throw new NotImplementedException();
        }
    }
}
