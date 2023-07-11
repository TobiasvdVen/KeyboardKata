using KeyboardKata.Domain.InputDetection.SharpHook;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Tests.Abstractions;
using KeyboardKata.Tests.Infrastructure.SharpHook;
using KeyboardKata.Tests.Infrastructure.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace KeyboardKata.Tests.Startup
{
    public class WindowsStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAppLauncher, WindowsAppLauncher>();
            services.AddScoped<ITrainerFinder, WpfTrainerFinder>();
            services.AddScoped<IKeyboard, SharpHookKeyboard>();
            services.AddTransient<IKeyCodeMapper, SharpHookKeyCodeMapper>();
            services.AddTransient<SharpHookKeyCodeMapper>();
        }
    }
}
