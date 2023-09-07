using System;

namespace KeyboardKata.App.Shortcuts
{
    public interface IShortcutCommands
    {
        ShortcutCommand GetShortcut(string identifier, Action action);
    }
}