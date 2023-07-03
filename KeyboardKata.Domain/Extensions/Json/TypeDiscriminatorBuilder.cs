namespace KeyboardKata.Domain.Extensions.Json
{
    public class TypeDiscriminatorBuilder<T> where T : class
    {
        private readonly Dictionary<string, Type> _types;

        public TypeDiscriminatorBuilder()
        {
            _types = new Dictionary<string, Type>(); ;
        }

        public TypeDiscriminatorJsonConverter<T> BuildConverter()
        {
            return new TypeDiscriminatorJsonConverter<T>(new RegisteredTypeDiscriminatorMap(_types));
        }

        public void Register(Type type, string discriminator)
        {
            _types[discriminator] = type;
        }
    }
}
