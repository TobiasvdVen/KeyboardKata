using KeyboardKata.Trainer;
using KeyboardKata.Trainer.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using Vanara.PInvoke;

namespace KeyboardKata.Wpf
{
    public partial class App : Application
    {
        private readonly Dispatcher _dispatcher;
        private IHost? _host;

        public App()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        public void Application_Startup(object sender, StartupEventArgs e)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(e.Args);

            builder.ConfigureServices(services =>
            {
                services.AddSingleton<MainViewModel>();
            });

            builder.AddKeyboardKataTrainer(s => new TrainerViewModel());

            _host = builder.Build();

            MainWindow = new MainWindow()
            {
                DataContext = _host.Services.GetRequiredService<MainViewModel>(),
                WindowState = WindowState.Maximized
            };

            MainWindow.Show();
            ConfigureMainWindowTransparency();

            _host.Start();

            IHostApplicationLifetime applicationLifetime = _host.Services.GetRequiredService<IHostApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(() =>
            {
                _dispatcher.Invoke(() => MainWindow.Close());
            });
        }

        public void Application_Exit(object sender, ExitEventArgs e)
        {
            _host?.WaitForShutdown();
            _host?.Dispose();
        }

        private void ConfigureMainWindowTransparency()
        {
            nint handle = new WindowInteropHelper(MainWindow).Handle;

            int style = User32.GetWindowLong(handle, User32.WindowLongFlags.GWL_EXSTYLE);

            style |= (int)User32.WindowStylesEx.WS_EX_TRANSPARENT;
            style |= (int)User32.WindowStylesEx.WS_EX_NOACTIVATE;

            User32.SetWindowLong(handle, User32.WindowLongFlags.GWL_EXSTYLE, style);
        }
    }
}
