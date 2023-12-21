using System;
using System.Threading.Tasks;

namespace KeyboardKata.App.Shortcuts
{
    public interface IShortcutCommands
    {
        AsyncShortcutCommand GetShortcut(string identifier, Func<Task> action);
        ShortcutCommand GetShortcut(string identifier, Action action);
    }
}