namespace KeyboardKata.Domain.Actions
{
    public interface IKeyboardActionSource
    {
        KeyboardAction? GetKeyboardAction();
    }
}
