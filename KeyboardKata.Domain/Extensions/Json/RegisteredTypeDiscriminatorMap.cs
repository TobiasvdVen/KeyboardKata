namespace KeyboardKata.Domain.Extensions.Json
{
    public class RegisteredTypeDiscriminatorMap : ITypeDiscriminatorMap
    {
        private readonly Dictionary<string, Type> _types;

        public RegisteredTypeDiscriminatorMap(Dictionary<string, Type> types)
        {
            _types = types;
        }

        public Type ResolveType(string discriminator)
        {
            return _types[discriminator];
        }
    }
}
