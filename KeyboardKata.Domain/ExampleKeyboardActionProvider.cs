namespace KeyboardKata.Domain
{
    internal class ExampleKeyboardActionProvider : IKeyboardActionProvider
    {
        private readonly IKeyCodeMapper _keyCodeMapper;

        public ExampleKeyboardActionProvider(IKeyCodeMapper keyCodeMapper)
        {
            _keyCodeMapper = keyCodeMapper;
        }

        public KeyboardAction GetKeyboardAction()
        {
            return new KeyboardAction("Copy something!", new Pattern(new List<SubPattern>()
            {
                new SubPattern(_keyCodeMapper.Key("C"), new List<Key>() { _keyCodeMapper.Key("LControl") })
            }));
        }
    }
}
