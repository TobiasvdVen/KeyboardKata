using KeyboardKata.Domain.InputProcessing;
using Microsoft.Extensions.Logging;
using WindowsInput.Events;
using WindowsInput.Events.Sources;

namespace KeyboardKata.Windows
{
    public class WindowsInputDelegator
    {
        private readonly IInputProcessor _processor;
        private readonly ILogger<WindowsInputDelegator> _logger;

        public WindowsInputDelegator(IInputProcessor processor, ILogger<WindowsInputDelegator> logger)
        {
            _processor = processor;
            _logger = logger;
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
            _logger.LogTrace($"ProcessInput: {input}");

            InputContinuation continuation = _processor.Process(input);

            eventSource.Next_Hook_Enabled = continuation == InputContinuation.Safe;
        }
    }
}
