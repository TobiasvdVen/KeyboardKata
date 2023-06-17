using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KeyboardKata.App.ImGui
{
    public static class AppImGuiHosting
    {
        public static void UseImGui(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<ImGuiService>();
            });
        }
    }
}
