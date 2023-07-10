using KeyboardKata.Domain.InputProcessing;
using Microsoft.Extensions.Logging;
using SharpHook;
using SharpHook.Native;

namespace KeyboardKata.Domain.InputDetection.SharpHook
{
    public class SharpHookInputDelegator
    {
        private readonly IInputProcessor _inputProcessor;
        private readonly SharpHookKeyCodeMapper _keyCodeMapper;
        private readonly ILogger<SharpHookInputDelegator> _logger;

        public SharpHookInputDelegator(IInputProcessor inputProcessor, SharpHookKeyCodeMapper keyCodeMapper, ILogger<SharpHookInputDelegator> logger)
        {
            _inputProcessor = inputProcessor;
            _keyCodeMapper = keyCodeMapper;
            _logger = logger;
        }

        public void Delegate(KeyboardHookEventArgs e)
        {
            _logger.LogTrace($"Delegating {e.RawEvent}.");

            KeyPress? keyPress = ParseKeyPress(e);

            if (keyPress is not null)
            {
                Key key = _keyCodeMapper.Key(e.Data.KeyCode);

                _logger.LogDebug($"Delegating {e.Data.KeyCode} ({key.ToString(_keyCodeMapper)}) {keyPress}");

                InputContinuation continuation = _inputProcessor.Process(new Input(key, keyPress.Value));

                e.SuppressEvent = continuation == InputContinuation.Unsafe;
            }
        }

        private KeyPress? ParseKeyPress(KeyboardHookEventArgs e)
        {
            return e.RawEvent.Type switch
            {
                EventType.KeyPressed => KeyPress.Down,
                EventType.KeyReleased => KeyPress.Up,
                _ => null,
            };
        }
    }
}
