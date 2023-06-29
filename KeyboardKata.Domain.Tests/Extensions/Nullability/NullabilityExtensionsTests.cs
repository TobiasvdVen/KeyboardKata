using KeyboardKata.Domain.Extensions.Nullability;
using KeyboardKata.Domain.Tests.Extensions.Nullability.Models;
using System.Linq;
using System.Reflection;
using Xunit;

namespace KeyboardKata.Domain.Tests.Extensions.Nullability
{
    public class NullabilityExtensionsTests
    {
        [Fact]
        public void IsNullable_GivenNullableProperty_ReturnsTrue()
        {
            PropertyInfo propertyInfo = typeof(NullableProperty).GetProperty(nameof(NullableProperty.Nullable))!;

            Assert.True(propertyInfo.IsNullable());
        }

        [Fact]
        public void IsNullable_GivenNonNullableProperty_ReturnsFalse()
        {
            PropertyInfo propertyInfo = typeof(NonNullableProperty).GetProperty(nameof(NonNullableProperty.NotNullable))!;

            Assert.False(propertyInfo.IsNullable());
        }

        [Fact]
        public void IsNullable_GivenNullableField_ReturnsTrue()
        {
            FieldInfo fieldInfo = typeof(NullableField).GetField(nameof(NullableField.nullable))!;

            Assert.True(fieldInfo.IsNullable());
        }

        [Fact]
        public void IsNullable_GivenNonNullableField_ReturnsFalse()
        {
            FieldInfo fieldInfo = typeof(NonNullableField).GetField(nameof(NonNullableField.notNullable))!;

            Assert.False(fieldInfo.IsNullable());
        }

        [Fact]
        public void IsNullable_GivenNullableParameter_ReturnsTrue()
        {
            ConstructorInfo constructor = typeof(NullableField).GetConstructors().Single();

            ParameterInfo parameterInfo = constructor
                .GetParameters()
                .Where(p => p.Name == "nullable")
                .Single();

            Assert.True(parameterInfo.IsNullable());
        }

        [Fact]
        public void IsNullable_GivenNonNullableParameter_ReturnsFalse()
        {
            ConstructorInfo constructor = typeof(NonNullableField).GetConstructors().Single();

            ParameterInfo parameterInfo = constructor.GetParameters().Where(p => p.Name == "notNullable").Single();

            Assert.False(parameterInfo.IsNullable());
        }

        [Fact]
        public void IsNullable_GivenBothNullableAndNotProperty_DeterminesNullabilityForBoth()
        {
            PropertyInfo nullable = typeof(BothProperties).GetProperty(nameof(BothProperties.Nullable))!;
            PropertyInfo notNullable = typeof(BothProperties).GetProperty(nameof(BothProperties.NotNullable))!;

            Assert.True(nullable.IsNullable());
            Assert.False(notNullable.IsNullable());
        }
    }
}
