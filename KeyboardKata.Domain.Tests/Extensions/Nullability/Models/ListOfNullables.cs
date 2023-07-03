using System.Collections.Generic;

namespace KeyboardKata.Domain.Tests.Extensions.Nullability.Models
{
    internal class ListOfNullables
    {
        public ListOfNullables(List<NestedNonNullable?> nullableThings)
        {
            NullableThings = nullableThings;
        }

        public List<NestedNonNullable?> NullableThings { get; }
    }
}
