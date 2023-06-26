using KeyboardKata.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using KeyboardKata.Domain.Actions;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Sessions;
using KeyboardKata.Domain.Sessions.Configuration;
using KeyboardKata.Domain.Actions.Pools;
using KeyboardKata.Domain.InputMatching;
using Microsoft.Extensions.Configuration;

#if WINDOWS
using KeyboardKata.Windows;
#endif

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
            string configPath = appConfiguration["config"] ?? throw new ArgumentException("Context configuration did not contain a path to a trainer session config!");

            using FileStream configContents = new(configPath, FileMode.Open, FileAccess.Read);


#if WINDOWS
            WindowsKeyCodeMapper windowsKeyCodeMapper = new();
            SessionConfigurationReader configurationReader = new(windowsKeyCodeMapper);

            configurationReader.Read(configContents);

            SessionConfiguration defaultSettings = new(
                quitPattern: new ExactMatchPattern(
                    new Input[] { new Input(windowsKeyCodeMapper.Key(WindowsInput.Events.KeyCode.Q), KeyPress.Down) }),

                new LinearActionPool(new SingleActionPool[]
                {
                    new SingleActionPool("Type the letter \"C\"!", new ExactMatchPattern(new List<Input>()
                    {
                        new Input(windowsKeyCodeMapper.Key("C"), KeyPress.Down)
                    }), repeats: 0),
                    new SingleActionPool("Type the letter \"K\"!", new ExactMatchPattern(new List<Input>()
                    {
                        new Input(windowsKeyCodeMapper.Key("K"), KeyPress.Down)
                    }), repeats: 0)
                }, repeats: 0)
            );

            return defaultSettings;
#else
            throw new PlatformNotSupportedException();
#endif
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

#if WINDOWS
            services.AddHostedService<WindowsInputService>();

            services.AddTransient<IKeyCodeMapper, WindowsKeyCodeMapper>();
            services.AddSingleton<WindowsInputDelegator>();
#else
            throw new PlatformNotSupportedException();
#endif

            return services;
        }
    }
}
