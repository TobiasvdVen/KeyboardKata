namespace KeyboardKata.Domain.Actions.Pools
{
    public record LinearActionPool : KeyboardActionPool
    {
        public LinearActionPool(IEnumerable<KeyboardActionPool> linear, int? repeats) : base(repeats)
        {
            Linear = linear;
        }

        public IEnumerable<KeyboardActionPool> Linear { get; }
        public override IEnumerable<KeyboardActionPool> Actions => Linear;
    }
}
