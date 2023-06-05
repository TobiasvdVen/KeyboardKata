namespace KeyboardKata.Domain
{
    public interface IKeyboardKata
    {
        void Prompt(KeyboardAction action);
        void Progress(KeyboardAction action, Sequence remaining);
        void Success(KeyboardAction action);
        void Failure(KeyboardAction action, Sequence actual);
    }
}
