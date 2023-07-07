namespace KeyboardKata.Domain.Extensions.Json
{
    public class TypeDiscriminatorBuilder<T> where T : class
    {
        private readonly Dictionary<string, Type> _types;

        public TypeDiscriminatorBuilder()
        {
            _types = new Dictionary<string, Type>();

            TypeDiscriminatorIdentifier = "type";
        }

        public Func<string, string>? DefaultMutator { get; set; }
        public string TypeDiscriminatorIdentifier { get; set; }

        public TypeDiscriminatorJsonConverter<T> BuildConverter()
        {
            return new TypeDiscriminatorJsonConverter<T>(TypeDiscriminatorIdentifier, new RegisteredTypeDiscriminatorMap(_types));
        }

        public TypeDiscriminatorBuilder<T> Register(Type type, string discriminator)
        {
            _types[discriminator] = type;

            return this;
        }

        public TypeDiscriminatorBuilder<T> Register(Type type)
        {
            if (DefaultMutator is not null)
            {
                return Register(type, DefaultMutator);
            }

            return Register(type, type.Name);
        }

        public TypeDiscriminatorBuilder<T> Register(Type type, Func<string, string> mutator)
        {
            return Register(type, mutator(type.Name));
        }
    }
}
