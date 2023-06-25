using KeyboardKata.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using KeyboardKata.Domain.Actions;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Sessions;
using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.Sessions.Configuration;
using KeyboardKata.Domain.Actions.Pools;

#if WINDOWS
using KeyboardKata.Windows;
#endif

namespace KeyboardKata.Trainer
{
    public static class TrainerHosting
    {
        public static IHostBuilder AddKeyboardKataTrainer<TKeyboardKata>(this IHostBuilder hostBuilder) where TKeyboardKata : class, IKeyboardKata
        {
            return hostBuilder.AddKeyboardKataTrainer<TKeyboardKata>(BuildDefaultSettings());
        }

        public static IHostBuilder AddKeyboardKataTrainer<TKeyboardKata>(this IHostBuilder hostBuilder, SessionConfiguration settings) where TKeyboardKata : class, IKeyboardKata
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                string? sessionConfigurationFile = context.Configuration["config"];

                services.AddSingleton<TKeyboardKata>();
                services.AddSingleton<IKeyboardKata>(s => s.GetRequiredService<TKeyboardKata>());

                services.AddSingleton<SessionState>();
                services.AddSingleton<ISessionState, SessionState>(s => s.GetRequiredService<SessionState>());
                services.AddSingleton<IInputProcessor, QuitProcessor>(s => new QuitProcessor(
                    settings.QuitPattern,
                    s.GetRequiredService<SessionState>(),
                    s.GetRequiredService<IHostApplicationLifetime>()));

                services.AddSingleton<KeyboardActionSourceFactory>();
                services.AddSingleton(s => s.GetRequiredService<KeyboardActionSourceFactory>().Create(settings.Actions));
                services.AddSingleton<ILogger>(NullLogger.Instance);

                services.AddHostedService<KeyboardKataTrainerService>();

#if WINDOWS
                services.AddHostedService<WindowsInputService>();

                services.AddTransient<IKeyCodeMapper, WindowsKeyCodeMapper>();
                services.AddSingleton<WindowsInputDelegator>();
#else
                throw new PlatformNotSupportedException();
#endif
            });

            return hostBuilder;
        }

        public static IHostBuilder AddKeyboardKataTrainer<TKeyboardKata>(this IHostBuilder hostBuilder, Func<IServiceProvider, TKeyboardKata> keyboardKata) where TKeyboardKata : class, IKeyboardKata
        {
            return hostBuilder.AddKeyboardKataTrainer(BuildDefaultSettings(), keyboardKata);
        }

        public static IHostBuilder AddKeyboardKataTrainer<TKeyboardKata>(this IHostBuilder hostBuilder, SessionConfiguration settings, Func<IServiceProvider, TKeyboardKata> keyboardKata) where TKeyboardKata : class, IKeyboardKata
        {
            IHostBuilder builder = hostBuilder.AddKeyboardKataTrainer<TKeyboardKata>(settings);

            builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<TKeyboardKata>(keyboardKata);
                services.AddSingleton<IKeyboardKata>(s => s.GetRequiredService<TKeyboardKata>());
            });

            return builder;
        }

        private static SessionConfiguration BuildDefaultSettings()
        {
#if WINDOWS
            WindowsKeyCodeMapper windowsKeyCodeMapper = new();
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
    }
}
