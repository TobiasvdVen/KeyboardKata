using KeyboardKata.Domain.Exceptions;
using KeyboardKata.Domain.InputMatching;
using System.Collections.Concurrent;

namespace KeyboardKata.App.Shortcuts
{
    public class ShortcutCommandRegistry
    {
        private ConcurrentDictionary<string, IPattern> _shortcuts;

        public ShortcutCommandRegistry()
        {
            _shortcuts = new ConcurrentDictionary<string, IPattern>();
        }

        public IPattern GetShortcut(string identifier)
        {
            if (_shortcuts.TryGetValue(identifier, out IPattern? shortcut))
            {
                return shortcut;
            }

            throw new KeyboardKataException($"Shortcut for command {identifier} not found.");
        }

        public void SetShortcut(string identifier, IPattern shortcut)
        {
            _shortcuts[identifier] = shortcut;
        }
    }
}
