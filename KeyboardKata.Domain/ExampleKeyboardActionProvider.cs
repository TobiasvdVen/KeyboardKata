namespace KeyboardKata.Domain
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
                new KeyboardAction("Type the letter \"C\"!", new Pattern(new List<SubPattern>()
                {
                    new SubPattern(_keyCodeMapper.Key("C"), Enumerable.Empty<Key>())
                })),
                new KeyboardAction("Type the letter \"K\"!", new Pattern(new List<SubPattern>()
                {
                    new SubPattern(_keyCodeMapper.Key("K"), Enumerable.Empty<Key>())
                }))
            };

            return actions[(_counter++) % actions.Length];
        }
    }
}
