namespace KeyboardKata.Domain.Actions.Pools
{
    public record LinearActionPool : KeyboardActionPool
    {
        public LinearActionPool(IEnumerable<KeyboardActionPool> actions, int? repeats) : base(actions, repeats)
        {
        }
    }
}
