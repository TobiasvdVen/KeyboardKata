namespace KeyboardKata.Domain.Extensions.Json
{
    public class TypeDiscriminatorBuilder<T> where T : class
    {
        private readonly Dictionary<string, Type> _types;

        public TypeDiscriminatorBuilder()
        {
            _types = new Dictionary<string, Type>();
        }

        public Func<string, string>? DefaultMutator { get; set; }

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
            if (DefaultMutator is not null)
            {
                Register(type, DefaultMutator);
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
