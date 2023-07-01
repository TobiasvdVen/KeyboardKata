using System.Collections.Generic;

namespace KeyboardKata.Domain.Tests.Extensions.Nullability.Models
{
    internal class ListOfNonNullables
    {
        public ListOfNonNullables(List<Hello> things)
        {
            Things = things;
        }

        public List<Hello> Things { get; }
    }
}
