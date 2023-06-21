namespace KeyboardKata.Domain.Actions.Pools
{
    public abstract record KeyboardActionPool
    {
        protected KeyboardActionPool(int? repeats)
        {
            Repeats = repeats;
        }

        public abstract IEnumerable<KeyboardActionPool> Actions { get; }
        public int? Repeats { get; }
    }
}
