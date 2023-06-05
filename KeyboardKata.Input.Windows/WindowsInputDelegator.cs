using KeyboardKata.Domain;
using WindowsInput.Events;
using WindowsInput.Events.Sources;

namespace KeyboardKata.InputSources.Windows
{
    public class WindowsInputDelegator
    {
        private readonly IInputProcessor _processor;

        public WindowsInputDelegator(IInputProcessor processor)
        {
            _processor = processor;
        }

        public void KeyDown(EventSourceEventArgs<KeyDown> keyDown)
        {
            ProcessInput(new Input(new Key(((int)keyDown.Data.Key)), KeyPress.Down), keyDown);
        }

        public void KeyUp(EventSourceEventArgs<KeyUp> keyUp)
        {
            ProcessInput(new Input(new Key(((int)keyUp.Data.Key)), KeyPress.Up), keyUp);
        }

        private void ProcessInput(Input input, EventSourceEventArgs eventSource)
        {
            InputContinuation continuation = _processor.Process(input);

            eventSource.Next_Hook_Enabled = continuation == InputContinuation.Safe;
        }
    }
}
