namespace KeyboardKata.Domain.Tests.Extensions.Nullability.Models
{
    internal class NestedNonNullable
    {
        public NestedNonNullable(NonNullableProperty property, NonNullableField field)
        {
            Property = property;
            Field = field;
        }

        public NonNullableProperty Property { get; }
        public readonly NonNullableField Field;
    }
}
