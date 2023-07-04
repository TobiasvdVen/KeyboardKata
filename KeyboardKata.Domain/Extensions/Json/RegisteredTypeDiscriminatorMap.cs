namespace KeyboardKata.Domain.Extensions.Json
{
    public class RegisteredTypeDiscriminatorMap : ITypeDiscriminatorMap
    {
        private readonly Dictionary<string, Type> _types;

        public RegisteredTypeDiscriminatorMap(Dictionary<string, Type> types)
        {
            _types = types;
        }

        public bool CanResolve(string discriminator) => _types.ContainsKey(discriminator);

        public Type ResolveType(string discriminator) => _types[discriminator];
    }
}
