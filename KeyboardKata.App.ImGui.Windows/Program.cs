using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace KeyboardKata.App.ImGui.Windows
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureAppConfiguration((context, appConfig) =>
            {
                Stream? stream = Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream("KeyboardKata.App.ImGui.Windows.appsettings.json") ?? throw new Exception("No settings found!");

                appConfig.AddJsonStream(stream);
                appConfig.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
            });

            builder.AddKeyboardKata().UseImGui();

            using IHost host = builder.Build();
            await host.StartAsync();

            ImGuiService imGui = host.Services.GetRequiredService<ImGuiService>();
            imGui.Run();

            await host.StopAsync();
        }
    }
}
