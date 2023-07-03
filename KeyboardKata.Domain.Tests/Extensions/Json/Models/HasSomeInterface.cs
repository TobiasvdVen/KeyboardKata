namespace KeyboardKata.Domain.Tests.Extensions.Json.Models
{
    public class HasSomeInterface
    {
        public HasSomeInterface(ISomeInterface someInterface)
        {
            SomeInterface = someInterface;
        }

        public ISomeInterface SomeInterface { get; }
    }
}
