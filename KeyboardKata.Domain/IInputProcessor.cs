namespace KeyboardKata.Domain
{
    public interface IInputProcessor
    {
        InputContinuation Process(Input input);
    }
}
