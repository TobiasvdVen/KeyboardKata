using KeyboardKata.Tests.Abstractions;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KeyboardKata.Tests.Infrastructure.Windows
{
    public class WindowsApp : IApp
    {
        private readonly Process _process;

        public WindowsApp(Process process)
        {
            _process = process;
        }

        public bool IsVisible => IsWindowVisible(_process.MainWindowHandle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        public void Dispose()
        {
            _process.Kill();
        }
    }
}
