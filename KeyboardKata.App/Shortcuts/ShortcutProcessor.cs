using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using System.Collections.Generic;
using System.Linq;

namespace KeyboardKata.App.Shortcuts
{
    public class ShortcutProcessor : IPatternProcessor
    {
        private readonly IEnumerable<ShortcutCommand> _shortcutCommands;

        public ShortcutProcessor(ShortcutCommand shortcutCommand) : this(new[] { shortcutCommand })
        {

        }

        public ShortcutProcessor(IEnumerable<ShortcutCommand> shortcutCommands)
        {
            _shortcutCommands = shortcutCommands;
        }

        public void Process(IPattern pattern)
        {
            foreach (ShortcutCommand command in _shortcutCommands.Where(s => s.Shortcut == pattern))
            {
                command.Execute();
            }
        }
    }
}
