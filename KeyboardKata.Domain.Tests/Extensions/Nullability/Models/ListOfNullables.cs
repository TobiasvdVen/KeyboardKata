using System.Collections.Generic;

namespace KeyboardKata.Domain.Tests.Extensions.Nullability.Models
{
    internal class ListOfNullables
    {
        public ListOfNullables(List<Hello?> nullableThings)
        {
            NullableThings = nullableThings;
        }

        public List<Hello?> NullableThings { get; }
    }
}
