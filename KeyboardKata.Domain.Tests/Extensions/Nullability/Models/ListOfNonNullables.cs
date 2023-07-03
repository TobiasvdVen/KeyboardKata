using System.Collections.Generic;

namespace KeyboardKata.Domain.Tests.Extensions.Nullability.Models
{
    internal class ListOfNonNullables
    {
        public ListOfNonNullables(List<NestedNonNullable> things)
        {
            Things = things;
        }

        public List<NestedNonNullable> Things { get; }
    }
}
