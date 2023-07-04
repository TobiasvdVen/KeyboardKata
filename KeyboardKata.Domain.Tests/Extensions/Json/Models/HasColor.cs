namespace KeyboardKata.Domain.Tests.Extensions.Json.Models
{
    public class HasColor
    {
        public HasColor(IColor someInterface)
        {
            SomeInterface = someInterface;
        }

        public IColor SomeInterface { get; }
    }
}
