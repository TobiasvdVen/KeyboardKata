﻿using KeyboardKata.Domain.Extensions.Nullability;
using KeyboardKata.Domain.Tests.Extensions.Nullability.Models;
using Xunit;

namespace KeyboardKata.Domain.Tests.Extensions.Nullability
{
    public class NullIntegrityExtensionsTests
    {
        [Fact]
        public void GivenFullyInitialized_WhenIntegrityTested_ThenHasIntegrity()
        {
            BothProperties fullyInitialized = new(notNullable: new Hello(), nullable: new Hello());

            Assert.True(fullyInitialized.HasNullIntegrity());
        }

        [Fact]
        public void GivenSomeNullablesAreNull_WhenIntegrityTested_ThenHasIntegrity()
        {
            BothProperties acceptablyInitialized = new(notNullable: new Hello(), nullable: null);

            Assert.True(acceptablyInitialized.HasNullIntegrity());
        }

        [Fact]
        public void GivenSomeNull_WhenIntegrityTested_ThenDoesNotHaveIntegrity()
        {
            BothProperties notAcceptablyInitialized = new(notNullable: null!, nullable: new Hello());

            Assert.False(notAcceptablyInitialized.HasNullIntegrity());
        }

        [Fact]
        public void GivenNestedNullProperty_WhenIntegrityTested_ThenDoesNotHaveIntegrity()
        {
            NestedNonNullable nestedNull = new(property: new NonNullableProperty(null!), field: new NonNullableField(new Hello()));

            Assert.False(nestedNull.HasNullIntegrity());
        }

        [Fact]
        public void GivenNoIntegrity_WhenIntegrityTested_ThenIdentifyOffendingMember()
        {
            NestedNonNullable nestedNull = new(property: new NonNullableProperty(null!), field: new NonNullableField(new Hello()));

            Assert.False(nestedNull.HasNullIntegrity(out string errors));

            Assert.Contains($"{nameof(NestedNonNullable.Property)}.{nameof(NonNullableProperty.NotNullable)}", errors);
        }

        [Fact]
        public void GivenNestedNullField_WhenIntegrityTested_ThenDoesNotHaveIntegrity()
        {
            NestedNonNullable nestedNull = new(property: new NonNullableProperty(new Hello()), field: new NonNullableField(null!));

            Assert.False(nestedNull.HasNullIntegrity());
        }
    }
}
