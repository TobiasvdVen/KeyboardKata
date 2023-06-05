namespace KeyboardKata.Domain.Tests.Helpers
{
    public record TestKey : Key
    {
        public TestKey(string keyName) : base(keyName.GetHashCode())
        {
            KeyName = keyName;
        }

        public string KeyName { get; }
    }
}
