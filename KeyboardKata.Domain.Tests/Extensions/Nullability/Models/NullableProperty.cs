namespace KeyboardKata.Domain.Tests.Extensions.Nullability.Models
{
    internal class NullableProperty
    {
        public NullableProperty(Hello? nullable)
        {
            Nullable = nullable;
        }

        public Hello? Nullable { get; }
    }
}
