namespace KeyboardKata.Domain
{
    public interface IKeyboardKata
    {
        void Prompt(KeyboardAction action);
        void Progress(KeyboardAction action, IEnumerable<Key> remaining);
        void Success(KeyboardAction action);
        void Failure(KeyboardAction action, IEnumerable<Key> actual);
    }
}
