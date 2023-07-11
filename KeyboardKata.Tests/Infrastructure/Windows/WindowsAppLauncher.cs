using KeyboardKata.Tests.Abstractions;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KeyboardKata.Tests.Infrastructure.Windows
{
    public class WindowsAppLauncher : IAppLauncher
    {
        public async Task<IApp> LaunchAppAsync()
        {
            string path = "F:\\Projects\\KeyboardKata\\Publish\\Keyboard Kata.exe";

            Process app = Process.Start(path);

            await Task.Delay(2000);

            return new WindowsApp(app);
        }
    }
}
