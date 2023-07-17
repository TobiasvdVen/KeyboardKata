using KeyboardKata.App.Commands;
using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using System.Collections.Generic;
using System.Linq;

namespace KeyboardKata.App.Shortcuts
{
    public class ShortcutProcessor : IPatternProcessor
    {
        private readonly IEnumerable<ShortcutCommand> _shortcutCommands;
        private readonly ICommandProcessor _commandProcessor;

        public ShortcutProcessor(ICommandProcessor commandProcessor, ShortcutCommand shortcutCommand) : this(commandProcessor, new[] { shortcutCommand })
        {

        }

        public ShortcutProcessor(ICommandProcessor commandProcessor, IEnumerable<ShortcutCommand> shortcutCommands)
        {
            _commandProcessor = commandProcessor;
            _shortcutCommands = shortcutCommands;
        }

        public void Process(IPattern pattern)
        {
            foreach (ShortcutCommand command in _shortcutCommands.Where(s => s.Shortcut == pattern))
            {
                _commandProcessor.ProcessAsync(command.Command);
            }
        }
    }
}
