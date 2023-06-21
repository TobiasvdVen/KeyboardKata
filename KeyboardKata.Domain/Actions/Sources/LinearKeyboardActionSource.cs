namespace KeyboardKata.Domain.Actions.Sources
{
    public class LinearKeyboardActionSource : IKeyboardActionSource
    {
        private readonly List<IKeyboardActionSource> _actions;
        private int _index;

        public LinearKeyboardActionSource(IEnumerable<IKeyboardActionSource> actions)
        {
            _actions = actions.ToList();
            _index = 0;
        }

        public KeyboardAction? GetKeyboardAction()
        {
            if (_index >= _actions.Count)
            {
                return null;
            }

            return _actions[_index++].GetKeyboardAction();
        }
    }
}
