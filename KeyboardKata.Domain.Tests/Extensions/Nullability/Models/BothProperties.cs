namespace KeyboardKata.Domain.Tests.Extensions.Nullability.Models
{
    internal class BothProperties
    {
        public BothProperties(Hello notNullable, Hello? nullable)
        {
            NotNullable = notNullable;
            Nullable = nullable;
        }

        public Hello NotNullable { get; }
        public Hello? Nullable { get; }
    }
}
