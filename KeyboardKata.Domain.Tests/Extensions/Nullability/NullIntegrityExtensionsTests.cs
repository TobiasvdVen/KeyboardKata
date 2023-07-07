using KeyboardKata.Domain.Extensions.Nullability;
using KeyboardKata.Domain.Tests.Extensions.Nullability.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace KeyboardKata.Domain.Tests.Extensions.Nullability
{
    public class NullIntegrityExtensionsTests
    {
        [Fact]
        public void GivenFullyInitialized_ThenHasIntegrity()
        {
            BothProperties fullyInitialized = new(notNullable: new Hello(), nullable: new Hello());

            Assert.True(fullyInitialized.HasNullIntegrity());
        }

        [Fact]
        public void GivenSomeNullablesAreNull_ThenHasIntegrity()
        {
            BothProperties acceptablyInitialized = new(notNullable: new Hello(), nullable: null);

            Assert.True(acceptablyInitialized.HasNullIntegrity());
        }

        [Fact]
        public void GivenSomeNull_ThenDoesNotHaveIntegrity()
        {
            BothProperties notAcceptablyInitialized = new(notNullable: null!, nullable: new Hello());

            Assert.False(notAcceptablyInitialized.HasNullIntegrity());
        }

        [Fact]
        public void GivenNestedNullProperty_ThenDoesNotHaveIntegrity()
        {
            NestedNonNullable nestedNull = new(property: new NonNullableProperty(null!), field: new NonNullableField(new Hello()));

            Assert.False(nestedNull.HasNullIntegrity());
        }

        [Fact]
        public void GivenNoIntegrity_ThenIdentifyOffendingMember()
        {
            NestedNonNullable nestedNull = new(property: new NonNullableProperty(null!), field: new NonNullableField(new Hello()));

            Assert.False(nestedNull.HasNullIntegrity(out string errors));

            Assert.Contains("Property.NotNullable", errors);
        }

        [Fact]
        public void GivenNestedNullField_ThenDoesNotHaveIntegrity()
        {
            NestedNonNullable nestedNull = new(property: new NonNullableProperty(new Hello()), field: new NonNullableField(null!));

            Assert.False(nestedNull.HasNullIntegrity());
        }

        [Fact]
        public void GivenListOfNullables_ThenHasIntegrity()
        {
            ListOfNullables listOfNullables = new(
                new List<NestedNonNullable?>()
                {
                    null
                });

            Assert.True(listOfNullables.HasNullIntegrity());
        }

        [Fact]
        public void GivenInitializedList_ThenHasIntegrity()
        {
            ListOfNonNullables listOfNonNullables = new(
                new List<NestedNonNullable>()
                {
                    new NestedNonNullable(new NonNullableProperty(new Hello()), new NonNullableField(new Hello()))
                });

            Assert.True(listOfNonNullables.HasNullIntegrity());
        }

        [Fact]
        public void GivenListWithNull_ThenDoesNotHaveIntegrity()
        {
            ListOfNonNullables listOfNonNullables = new(
                new List<NestedNonNullable>()
                {
                    null!
                });

            Assert.False(listOfNonNullables.HasNullIntegrity());
        }

        [Fact]
        public void GivenNoIntegrityList_ThenIdentifyOffendingElement()
        {
            ListOfNonNullables listOfNonNullables = new(
                new List<NestedNonNullable>()
                {
                    new NestedNonNullable(new NonNullableProperty(new Hello()), new NonNullableField(new Hello())),
                    null!
                });

            Assert.False(listOfNonNullables.HasNullIntegrity(out string errorSummary));
            Assert.Contains("Things[1]", errorSummary);
        }

        [Fact]
        public void GivenList_WhenElementDoesNotHaveIntegrity_ThenDoesNotHaveIntegrity()
        {
            ListOfNonNullables listOfNonNullables = new(
                new List<NestedNonNullable>()
                {
                    new NestedNonNullable(new NonNullableProperty(null!), new NonNullableField(new Hello())),
                });

            Assert.False(listOfNonNullables.HasNullIntegrity());
        }

        [Fact]
        public void GivenList_WhenElementDoesNotHaveIntegrity_ThenIdentifyOffendingElementAndMember()
        {
            ListOfNonNullables listOfNonNullables = new(
                new List<NestedNonNullable>()
                {
                    new NestedNonNullable(new NonNullableProperty(new Hello()), new NonNullableField(new Hello())),
                    new NestedNonNullable(new NonNullableProperty(null!), new NonNullableField(new Hello())),
                });

            Assert.False(listOfNonNullables.HasNullIntegrity(out string errorSummary));
            Assert.Contains("Things[1].Property.NotNullable", errorSummary);
        }

        [Fact]
        public void GivenEmptyEnumerable_ThenHasIntegrity()
        {
            ListOfNonNullables listOfNonNullables = new(Enumerable.Empty<NestedNonNullable>());

            Assert.True(listOfNonNullables.HasNullIntegrity());
        }
    }
}
