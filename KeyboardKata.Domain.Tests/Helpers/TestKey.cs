namespace KeyboardKata.Domain.Tests.Helpers
{
    internal record TestKey : Key
    {
        private readonly string _name;

        public TestKey(string name)
        {
            _name = name;
        }

        public override string DisplayName => _name;
    }
}
