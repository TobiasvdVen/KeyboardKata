using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KeyboardKata.Domain
{
    public static class Hosting
    {
        public static IHostBuilder UseKeyboardKata(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddScoped<IKataSession, Session>();
                services.AddScoped<IInputProcessor, Session>();
                //services.AddSingleton<ILogger>(NullLogger.Instance);
            });

            return hostBuilder;
        }
    }
}
