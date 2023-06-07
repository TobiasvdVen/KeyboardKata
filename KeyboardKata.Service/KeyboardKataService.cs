using KeyboardKata.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KeyboardKata.Service
{
    public class KeyboardKataService : IHostedService
    {
        private readonly IKataSession _session;
        private readonly ILogger<KeyboardKataService> _logger;

        public KeyboardKataService(IKataSession session, ILogger<KeyboardKataService> logger)
        {
            _session = session;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(KeyboardKataService)} starting.");

            _session.NextPrompt();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(KeyboardKataService)} stopping.");

            return Task.CompletedTask;
        }
    }
}
