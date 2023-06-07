using KeyboardKata.Domain;

#if WINDOWS
using KeyboardKata.InputSources.Windows;
#endif

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KeyboardKata.Cli
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args);

            using IHost host = builder
                .UseKeyboardKata()
                .ConfigureServices(services =>
                {
                    services.AddScoped<IKeyboardKata, CliKeyboardKata>();

                    ConfigureInputService(services);

                    services.AddLogging(logging =>
                    {
                        logging.SetMinimumLevel(LogLevel.Trace).AddConsole();
                    });
                })
                .Build();

            await host.RunAsync();

            using IServiceScope scope = host.Services.CreateScope();
            IKataSession session = scope.ServiceProvider.GetRequiredService<IKataSession>();

            session.NextPrompt();

            await host.WaitForShutdownAsync();
        }

        static void ConfigureInputService(IServiceCollection services)
        {
#if WINDOWS
            services.AddHostedService<WindowsInputService>();
            services.AddTransient<IKeyCodeMapper, WindowsKeyCodeMapper>();
            services.AddScoped<WindowsInputDelegator>();
#else
            throw new PlatformNotSupportedException();
#endif
        }
    }
}