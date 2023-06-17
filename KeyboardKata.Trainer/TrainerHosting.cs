using KeyboardKata.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using KeyboardKata.Domain.Actions;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Sessions;
using KeyboardKata.Domain.InputMatching;

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

        public static IHostBuilder AddKeyboardKataTrainer<TKeyboardKata>(this IHostBuilder hostBuilder, KataSettings settings) where TKeyboardKata : class, IKeyboardKata
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IKeyboardKata, TKeyboardKata>();

                services.AddSingleton<SessionState>();
                services.AddSingleton<ISessionState, SessionState>(s => s.GetRequiredService<SessionState>());
                services.AddSingleton<IInputProcessor, QuitProcessor>(s => new QuitProcessor(
                    settings.QuitPattern,
                    s.GetRequiredService<SessionState>(),
                    s.GetRequiredService<IHostApplicationLifetime>()));

                services.AddSingleton<IKeyboardActionProvider, ExampleKeyboardActionProvider>();
                services.AddSingleton<ILogger>(NullLogger.Instance);

#if WINDOWS
                services.AddHostedService<WindowsInputService>();
                services.AddHostedService<KeyboardKataTrainerService>();

                services.AddTransient<IKeyCodeMapper, WindowsKeyCodeMapper>();
                services.AddSingleton<WindowsInputDelegator>();
#else
                throw new PlatformNotSupportedException();
#endif
            });

            return hostBuilder;
        }

        public static IHostBuilder AddKeyboardKataTrainer<TKeyboardKata>(this IHostBuilder hostBuilder, TKeyboardKata keyboardKata) where TKeyboardKata : class, IKeyboardKata
        {
            return hostBuilder.AddKeyboardKataTrainer(BuildDefaultSettings(), keyboardKata);
        }

        public static IHostBuilder AddKeyboardKataTrainer<TKeyboardKata>(this IHostBuilder hostBuilder, KataSettings settings, TKeyboardKata keyboardKata) where TKeyboardKata : class, IKeyboardKata
        {
            IHostBuilder builder = hostBuilder.AddKeyboardKataTrainer<TKeyboardKata>(settings);

            builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IKeyboardKata>(keyboardKata);
            });

            return builder;
        }

        private static KataSettings BuildDefaultSettings()
        {
#if WINDOWS
            WindowsKeyCodeMapper windowsKeyCodeMapper = new();
            KataSettings defaultSettings = new()
            {
                QuitPattern = new ExactMatchPattern(
                    new Input[] { new Input(windowsKeyCodeMapper.Key(WindowsInput.Events.KeyCode.Q), KeyPress.Down) })
            };

            return defaultSettings;
#else
            throw new PlatformNotSupportedException();
#endif
        }
    }
}
