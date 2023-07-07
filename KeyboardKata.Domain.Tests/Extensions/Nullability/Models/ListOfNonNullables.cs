using System.Collections.Generic;

namespace KeyboardKata.Domain.Tests.Extensions.Nullability.Models
{
    internal class ListOfNonNullables
    {
        public ListOfNonNullables(IEnumerable<NestedNonNullable> things)
        {
            Things = things;
        }

        public IEnumerable<NestedNonNullable> Things { get; }
    }
}
