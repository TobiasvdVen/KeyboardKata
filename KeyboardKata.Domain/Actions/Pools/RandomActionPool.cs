namespace KeyboardKata.Domain.Actions.Pools
{
    public record RandomActionPool : KeyboardActionPool
    {
        public RandomActionPool(IEnumerable<KeyboardActionPool> random, int? repeats) : base(repeats)
        {
            Random = random;
        }

        public IEnumerable<KeyboardActionPool> Random { get; }
        public override IEnumerable<KeyboardActionPool> Actions => Random;
    }
}
