using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WindowsInput;
using WindowsInput.Events;
using WindowsInput.Events.Sources;

namespace KeyboardKata.InputSources.Windows
{
    public class WindowsInputService : IHostedService
    {
        private readonly WindowsInputDelegator _delegator;
        private readonly ILogger<WindowsInputService> _logger;

        private IKeyboardEventSource? _keyboard;

        public WindowsInputService(WindowsInputDelegator delegator, ILogger<WindowsInputService> logger)
        {
            _delegator = delegator;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("WindowsInputService starting.");

            _keyboard = Capture.Global.KeyboardAsync();
            _keyboard.KeyDown += OnKeyDown;
            _keyboard.KeyUp += OnKeyUp;

            return Task.CompletedTask;
        }

        private void OnKeyDown(object? sender, EventSourceEventArgs<KeyDown> e)
        {
            _delegator.KeyDown(e);
        }

        private void OnKeyUp(object? sender, EventSourceEventArgs<KeyUp> e)
        {
            _delegator.KeyUp(e);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("WindowsInputService stopped.");

            if (_keyboard is not null)
            {
                _keyboard.KeyDown -= OnKeyDown;
                _keyboard.KeyUp -= OnKeyUp;

                _keyboard.Dispose();
            }

            return Task.CompletedTask;
        }
    }
}
