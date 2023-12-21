using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;

namespace KeyboardKata.App.Shortcuts
{
    public class DefaultShortcuts
    {
        private readonly IKeyCodeMapper _keyCodeMapper;

        public DefaultShortcuts(IKeyCodeMapper keyCodeMapper)
        {
            _keyCodeMapper = keyCodeMapper;
        }

        public ShortcutCommandRegistry BuildRegistry()
        {
            ShortcutCommandRegistry registry = new();

            registry.SetShortcut("StartSession",
                new ExactMatchPattern(new Input[] { new Input(_keyCodeMapper.Key("S"), KeyPress.Down) }));

            registry.SetShortcut("ResetSessionResult",
                new ExactMatchPattern(new Input[] { new Input(_keyCodeMapper.Key("R"), KeyPress.Down) }));

            return registry;
        }
    }
}
