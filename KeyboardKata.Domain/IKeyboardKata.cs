namespace KeyboardKata.Domain
{
    public interface IKeyboardKata
    {
        void Prompt(Sequence sequence, string prompt);
        void Progress(Sequence progress, Sequence remaining, string prompt);
        void Success(Sequence sequence, string prompt);
        void Failure(Sequence sequence, Sequence actual, string prompt);
    }
}
