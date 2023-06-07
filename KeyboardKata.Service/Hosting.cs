using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using KeyboardKata.Service;

#if WINDOWS
using KeyboardKata.Windows;
#endif

namespace KeyboardKata.Domain
{
    public static class Hosting
    {
        public static IHostBuilder AddKeyboardKata<TKeyboardKata>(this IHostBuilder hostBuilder) where TKeyboardKata : class, IKeyboardKata
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddScoped<IKeyboardKata, TKeyboardKata>();

                services.AddScoped<IKataSession, Session>();
                services.AddScoped<IInputProcessor, Session>();
                services.AddScoped<IKeyboardActionProvider, ExampleKeyboardActionProvider>();
                services.AddSingleton<ILogger>(NullLogger.Instance);


#if WINDOWS
                services.AddHostedService<WindowsInputService>();
                services.AddHostedService<KeyboardKataService>();

                services.AddTransient<IKeyCodeMapper, WindowsKeyCodeMapper>();
                services.AddScoped<WindowsInputDelegator>();
#else
                throw new PlatformNotSupportedException();
#endif
            });
            return hostBuilder;
        }
    }
}
