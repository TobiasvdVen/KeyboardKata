namespace KeyboardKata.Domain.Tests.Extensions.Json.Models
{
    public class NumberComponent : IComponent
    {
        public int Number { get; }

        public NumberComponent(int number)
        {
            Number = number;
        }
    }
}
