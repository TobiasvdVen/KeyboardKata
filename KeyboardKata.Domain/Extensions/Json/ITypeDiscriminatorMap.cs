namespace KeyboardKata.Domain.Extensions.Json
{
    public interface ITypeDiscriminatorMap
    {
        bool CanResolve(string discriminator);
        Type ResolveType(string discriminator);
    }
}
