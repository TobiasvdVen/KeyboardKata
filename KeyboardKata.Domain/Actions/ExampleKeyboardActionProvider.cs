using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;

namespace KeyboardKata.Domain.Actions
{
    public class ExampleKeyboardActionProvider : IKeyboardActionProvider
    {
        private readonly IKeyCodeMapper _keyCodeMapper;

        private int _counter = 0;

        public ExampleKeyboardActionProvider(IKeyCodeMapper keyCodeMapper)
        {
            _keyCodeMapper = keyCodeMapper;
        }

        public KeyboardAction GetKeyboardAction()
        {
            KeyboardAction[] actions = new KeyboardAction[]
            {
                new KeyboardAction("Type the letter \"C\"!", new ExactMatchPattern(new List<Input>()
                {
                    new Input(_keyCodeMapper.Key("C"), KeyPress.Down)
                })),
                new KeyboardAction("Type the letter \"K\"!", new ExactMatchPattern(new List<Input>()
                {
                    new Input(_keyCodeMapper.Key("K"), KeyPress.Down)
                }))
            };

            return actions[_counter++ % actions.Length];
        }
    }
}
