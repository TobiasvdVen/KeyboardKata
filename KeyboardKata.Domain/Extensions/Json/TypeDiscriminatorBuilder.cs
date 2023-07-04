namespace KeyboardKata.Domain.Extensions.Json
{
    public class TypeDiscriminatorBuilder<T> where T : class
    {
        private readonly Dictionary<string, Type> _types;
        private readonly Func<string, string>? _defaultMutator;

        public TypeDiscriminatorBuilder()
        {
            _types = new Dictionary<string, Type>();
        }

        public TypeDiscriminatorBuilder(Func<string, string> defaultMutator) : this()
        {
            _defaultMutator = defaultMutator;
        }

        public TypeDiscriminatorJsonConverter<T> BuildConverter()
        {
            return new TypeDiscriminatorJsonConverter<T>(new RegisteredTypeDiscriminatorMap(_types));
        }

        public void Register(Type type, string discriminator)
        {
            _types[discriminator] = type;
        }

        public void Register(Type type)
        {
            if (_defaultMutator is not null)
            {
                Register(type, _defaultMutator);
                return;
            }

            Register(type, type.Name);
        }

        public void Register(Type type, Func<string, string> mutator)
        {
            Register(type, mutator(type.Name));
        }
    }
}
