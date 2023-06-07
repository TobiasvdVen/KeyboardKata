namespace KeyboardKata.Domain
{
    public class ExampleKeyboardActionProvider : IKeyboardActionProvider
    {
        private readonly IKeyCodeMapper _keyCodeMapper;

        public ExampleKeyboardActionProvider(IKeyCodeMapper keyCodeMapper)
        {
            _keyCodeMapper = keyCodeMapper;
        }

        public KeyboardAction GetKeyboardAction()
        {
            return new KeyboardAction("Type the letter \"C\"!", new Pattern(new List<SubPattern>()
            {
                new SubPattern(_keyCodeMapper.Key("C"), Enumerable.Empty<Key>())
            }));
        }
    }
}
