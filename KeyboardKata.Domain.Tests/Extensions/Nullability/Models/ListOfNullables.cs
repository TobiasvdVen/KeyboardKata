using System.Collections.Generic;

namespace KeyboardKata.Domain.Tests.Extensions.Nullability.Models
{
    internal class ListOfNullables
    {
        public ListOfNullables(IEnumerable<NestedNonNullable?> nullableThings)
        {
            NullableThings = nullableThings;
        }

        public IEnumerable<NestedNonNullable?> NullableThings { get; }
    }
}
