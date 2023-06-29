namespace KeyboardKata.Domain.Tests.Extensions.Nullability.Models
{
    internal class NonNullableField
    {
        public NonNullableField(Hello notNullable)
        {
            this.notNullable = notNullable;
        }

        public readonly Hello notNullable;
    }
}
