using KeyboardKata.Domain.InputMatching;
using System;
using System.Threading.Tasks;

namespace KeyboardKata.App.Shortcuts
{
    public class ShortcutCommandManager : IShortcutCommands
    {
        private readonly ShortcutCommandRegistry _shortcutCommandRegistry;

        public ShortcutCommandManager(ShortcutCommandRegistry shortcutCommandRegistry)
        {
            _shortcutCommandRegistry = shortcutCommandRegistry;
        }

        public ShortcutCommand GetShortcut(string identifier, Action action)
        {
            IPattern shortcut = _shortcutCommandRegistry.GetShortcut(identifier);

            return new ShortcutCommand(identifier, action, shortcut);
        }

        public AsyncShortcutCommand GetShortcut(string identifier, Func<Task> action)
        {
            IPattern shortcut = _shortcutCommandRegistry.GetShortcut(identifier);

            return new AsyncShortcutCommand(identifier, action, shortcut);
        }
    }
}
