namespace KeyboardKata.Domain.Actions.Pools
{
    public abstract record KeyboardActionPool
    {
        protected KeyboardActionPool(IEnumerable<KeyboardActionPool> actions, int? repeats)
        {
            Actions = actions;
            Repeats = repeats;
        }

        public IEnumerable<KeyboardActionPool> Actions { get; }
        public int? Repeats { get; }
    }
}
