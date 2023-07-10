using KeyboardKata.Domain;
using KeyboardKata.Domain.Actions;
using KeyboardKata.Domain.InputDetection.SharpHook;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Sessions;
using KeyboardKata.Domain.Sessions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace KeyboardKata.Trainer
{
    public static class TrainerHosting
    {
        public static IServiceCollection AddKeyboardKataTrainer<TKeyboardKata>(this IServiceCollection services,
            IConfiguration appConfiguration)
            where TKeyboardKata : class, IKeyboardKata
        {
            SessionConfiguration configuration = LoadSessionConfiguration(appConfiguration);

            return services.AddKeyboardKataTrainer<TKeyboardKata>(configuration);
        }

        public static IServiceCollection AddKeyboardKataTrainer<TKeyboardKata>(this IServiceCollection services,
            Func<IServiceProvider, TKeyboardKata> keyboardKata,
            IConfiguration appConfiguration)
            where TKeyboardKata : class, IKeyboardKata
        {
            SessionConfiguration configuration = LoadSessionConfiguration(appConfiguration);

            return services.AddKeyboardKataTrainer(keyboardKata, configuration);
        }

        public static IServiceCollection AddKeyboardKataTrainer<TKeyboardKata>(this IServiceCollection services,
            SessionConfiguration sessionConfiguration)
            where TKeyboardKata : class, IKeyboardKata
        {
            services.AddSingleton<TKeyboardKata>();

            return services.RegisterKeyboardKataTrainerServices<TKeyboardKata>(sessionConfiguration);
        }

        public static IServiceCollection AddKeyboardKataTrainer<TKeyboardKata>(this IServiceCollection services,
            Func<IServiceProvider, TKeyboardKata> keyboardKata,
            SessionConfiguration sessionConfiguration)
            where TKeyboardKata : class, IKeyboardKata
        {
            services.AddSingleton<TKeyboardKata>(keyboardKata);

            return services.RegisterKeyboardKataTrainerServices<TKeyboardKata>(sessionConfiguration);
        }

        public static SessionConfiguration LoadSessionConfiguration(IConfiguration appConfiguration)
        {
            string configPath = appConfiguration["Session"] ?? throw new ArgumentException("Context configuration did not contain a path to a trainer session config!");

            using FileStream configContents = new(configPath, FileMode.Open, FileAccess.Read);

            SharpHookKeyCodeMapper keyCodeMapper = new();
            SessionConfigurationReader configurationReader = new(keyCodeMapper);

            return configurationReader.Read(configContents);
        }

        private static IServiceCollection RegisterKeyboardKataTrainerServices<TKeyboardKata>(this IServiceCollection services, SessionConfiguration sessionConfiguration) where TKeyboardKata : class, IKeyboardKata
        {
            services.AddSingleton<IKeyboardKata>(s => s.GetRequiredService<TKeyboardKata>());

            services.AddSingleton<SessionState>();
            services.AddSingleton<ISessionState, SessionState>(s => s.GetRequiredService<SessionState>());
            services.AddSingleton<IInputProcessor, QuitProcessor>(s => new QuitProcessor(
                sessionConfiguration.QuitPattern,
                s.GetRequiredService<SessionState>(),
                s.GetRequiredService<IHostApplicationLifetime>()));

            services.AddSingleton<KeyboardActionSourceFactory>();
            services.AddSingleton(s => s.GetRequiredService<KeyboardActionSourceFactory>().Create(sessionConfiguration.Actions));
            services.AddSingleton<ILogger>(NullLogger.Instance);

            services.AddHostedService<KeyboardKataTrainerService>();

            services.AddHostedService<SharpHookInputService>();

            services.AddTransient<IKeyCodeMapper, SharpHookKeyCodeMapper>();
            services.AddTransient<SharpHookKeyCodeMapper>();
            services.AddSingleton<SharpHookInputDelegator>();

            return services;
        }
    }
}
