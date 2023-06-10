using KeyboardKata.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

#if WINDOWS
using KeyboardKata.Windows;
#endif

namespace KeyboardKata.Trainer
{
    public static class Hosting
    {
        public static IHostBuilder AddKeyboardKata<TKeyboardKata>(this IHostBuilder hostBuilder) where TKeyboardKata : class, IKeyboardKata
        {
#if WINDOWS
            WindowsKeyCodeMapper windowsKeyCodeMapper = new();
            TrainerSettings defaultSettings = new()
            {
                QuitPattern = new Pattern(
                    new SubPattern[] { new SubPattern(windowsKeyCodeMapper.Key(WindowsInput.Events.KeyCode.Q), Enumerable.Empty<Key>()) })
            };

            return hostBuilder.AddKeyboardKata<TKeyboardKata>(defaultSettings);
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static IHostBuilder AddKeyboardKata<TKeyboardKata>(this IHostBuilder hostBuilder, TrainerSettings settings) where TKeyboardKata : class, IKeyboardKata
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IKeyboardKata, TKeyboardKata>();

                services.AddSingleton<Session>();
                services.AddSingleton<IKataSession, Session>(s => s.GetRequiredService<Session>());
                services.AddSingleton<IInputProcessor, QuitProcessor>(s => new QuitProcessor(
                    settings.QuitPattern,
                    s.GetRequiredService<Session>(),
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
    }
}
