namespace KeyboardKata.Domain.Actions.Sources
{
    public class SingleKeyboardActionSource : IKeyboardActionSource
    {
        private readonly KeyboardAction _action;

        public SingleKeyboardActionSource(KeyboardAction action)
        {
            _action = action;

            Depleted = false;
        }

        public bool Depleted { get; private set; }

        public KeyboardAction? GetKeyboardAction()
        {
            return Depleted ? null : _action;
        }
    }
}
