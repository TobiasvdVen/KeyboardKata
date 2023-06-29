namespace KeyboardKata.Domain.Tests.Extensions.Nullability.Models
{
    internal class NonNullableProperty
    {
        public NonNullableProperty(Hello notNullable)
        {
            NotNullable = notNullable;
        }

        public Hello NotNullable { get; }
    }
}
