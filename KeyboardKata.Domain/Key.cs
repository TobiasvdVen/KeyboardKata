namespace KeyboardKata.Domain
{
    public abstract record Key : IEquatable<Key>
    {
        public abstract string DisplayName { get; }
    }
}