using KeyboardKata.App.Shortcuts;
using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using System.Collections.Generic;
using System.Linq;

namespace KeyboardKata.App.ViewModels
{
    public abstract class ViewModel : IPatternProcessor
    {
        private readonly ShortcutProcessor _shortcutProcessor;

        protected ViewModel()
        {
            _shortcutProcessor = new ShortcutProcessor(
                () => ShortcutCommands,
                () => AsyncShortcutCommands);
        }

        protected virtual IEnumerable<ShortcutCommand> ShortcutCommands => Enumerable.Empty<ShortcutCommand>();
        protected virtual IEnumerable<AsyncShortcutCommand> AsyncShortcutCommands => Enumerable.Empty<AsyncShortcutCommand>();

        public void Process(IPattern pattern)
        {
            _shortcutProcessor.Process(pattern);
        }
    }
}
