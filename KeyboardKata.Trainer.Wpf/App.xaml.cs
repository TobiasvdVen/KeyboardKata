using KeyboardKata.Trainer.ViewModels;
using System.Windows;
using System.Windows.Interop;
using Vanara.PInvoke;

namespace KeyboardKata.Wpf
{
    public partial class App : Application
    {
        public void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(
                    new TrainerViewModel("Test prompt!"))
            };

            MainWindow.Show();

            ConfigureMainWindowTransparency();
        }

        public void Application_Exit(object sender, ExitEventArgs e)
        {

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
