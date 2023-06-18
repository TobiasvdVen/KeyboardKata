namespace KeyboardKata.Domain.Actions
{
    public class LinearKeyboardActionSource : IKeyboardActionSource
    {
        private readonly List<KeyboardAction> _actions;
        private int _index;

        public LinearKeyboardActionSource(IEnumerable<KeyboardAction> actions)
        {
            _actions = actions.ToList();
            _index = 0;
        }

        private int NextIndex => _index++ % _actions.Count;

        public KeyboardAction GetKeyboardAction()
        {
            return _actions[NextIndex];
        }
    }
}
