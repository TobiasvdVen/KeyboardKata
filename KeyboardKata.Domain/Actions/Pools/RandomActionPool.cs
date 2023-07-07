namespace KeyboardKata.Domain.Actions.Pools
{
    public record RandomActionPool : KeyboardActionPool
    {
        public RandomActionPool(IEnumerable<KeyboardActionPool> actions, int? repeats) : base(actions, repeats)
        {
        }
    }
}
