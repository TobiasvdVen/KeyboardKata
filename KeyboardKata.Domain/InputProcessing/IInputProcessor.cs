namespace KeyboardKata.Domain.InputProcessing
{
    public interface IInputProcessor
    {
        InputContinuation Process(Input input);
    }
}
