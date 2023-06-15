using KeyboardKata.Domain.Sessions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KeyboardKata.Trainer
{
    public class KeyboardKataTrainerService : IHostedService
    {
        private readonly ISessionState _session;
        private readonly ILogger<KeyboardKataTrainerService> _logger;

        public KeyboardKataTrainerService(ISessionState session, ILogger<KeyboardKataTrainerService> logger)
        {
            _session = session;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(KeyboardKataTrainerService)} starting.");

            _session.NextPrompt();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(KeyboardKataTrainerService)} stopping.");

            return Task.CompletedTask;
        }
    }
}
