using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharpHook;

namespace KeyboardKata.Domain.InputDetection.SharpHook
{
    public class SharpHookInputService : IHostedService
    {
        private readonly SharpHookInputDelegator _delegator;
        private readonly ILogger<SharpHookInputService> _logger;

        private IGlobalHook? _hook;

        public SharpHookInputService(SharpHookInputDelegator delegator, ILogger<SharpHookInputService> logger)
        {
            _delegator = delegator;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(SharpHookInputService)} starting.");

            _hook = new SimpleGlobalHook();

            _hook.KeyPressed += KeyPressed;
            _hook.KeyReleased += KeyReleased;

            _ = _hook.RunAsync();

            return Task.CompletedTask;
        }

        private void KeyPressed(object? sender, KeyboardHookEventArgs e)
        {
            _delegator.Delegate(e);
        }

        private void KeyReleased(object? sender, KeyboardHookEventArgs e)
        {
            _delegator.Delegate(e);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(SharpHookInputService)} stopping.");

            if (_hook is not null)
            {
                _hook.KeyPressed -= KeyPressed;
                _hook.KeyReleased -= KeyReleased;

                _hook.Dispose();
            }

            return Task.CompletedTask;
        }
    }
}
