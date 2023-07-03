namespace KeyboardKata.Domain.Extensions.Json
{
    public interface ITypeDiscriminatorMap
    {
        Type ResolveType(string discriminator);
    }
}
