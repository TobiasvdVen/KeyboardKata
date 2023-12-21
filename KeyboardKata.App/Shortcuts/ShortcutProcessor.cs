using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyboardKata.App.Shortcuts
{
    public class ShortcutProcessor : IPatternProcessor
    {
        private readonly Func<IEnumerable<ShortcutCommand>> _shortcutCommands;
        private readonly Func<IEnumerable<AsyncShortcutCommand>> _asyncShortcutCommands;

        public ShortcutProcessor(
            Func<IEnumerable<ShortcutCommand>> shortcutCommands,
            Func<IEnumerable<AsyncShortcutCommand>> asyncShortcutCommands)
        {
            _shortcutCommands = shortcutCommands;
            _asyncShortcutCommands = asyncShortcutCommands;
        }

        public ShortcutProcessor(Func<IEnumerable<ShortcutCommand>> shortcutCommands)
            : this(shortcutCommands, () => Enumerable.Empty<AsyncShortcutCommand>())
        {
        }

        public ShortcutProcessor(Func<IEnumerable<AsyncShortcutCommand>> asyncShortcutCommands)
            : this(() => Enumerable.Empty<ShortcutCommand>(), asyncShortcutCommands)
        {
        }

        public ShortcutProcessor(
            Func<ShortcutCommand> shortcutCommand,
            Func<AsyncShortcutCommand> asyncShortcutCommand)
        {
            _shortcutCommands = () => new ShortcutCommand[] { shortcutCommand() };
            _asyncShortcutCommands = () => new AsyncShortcutCommand[] { asyncShortcutCommand() };
        }

        public ShortcutProcessor(Func<ShortcutCommand> shortcutCommand)
            : this(() => new ShortcutCommand[] { shortcutCommand() }, () => Enumerable.Empty<AsyncShortcutCommand>())
        {
        }

        public ShortcutProcessor(Func<AsyncShortcutCommand> asyncShortcutCommand)
            : this(() => Enumerable.Empty<ShortcutCommand>(), () => new AsyncShortcutCommand[] { asyncShortcutCommand() })
        {
        }

        public void Process(IPattern pattern)
        {
            foreach (ShortcutCommand command in _shortcutCommands().Where(s => s.Shortcut == pattern))
            {
                command.Execute();
            }

            foreach (AsyncShortcutCommand command in _asyncShortcutCommands().Where(s => s.Shortcut == pattern))
            {
                command.Execute();
            }
        }
    }
}
