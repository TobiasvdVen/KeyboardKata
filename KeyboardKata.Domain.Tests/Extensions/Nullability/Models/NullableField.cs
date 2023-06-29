namespace KeyboardKata.Domain.Tests.Extensions.Nullability.Models
{
    internal class NullableField
    {
        public NullableField(Hello? nullable)
        {
            this.nullable = nullable;
        }

        public readonly Hello? nullable;
    }
}
