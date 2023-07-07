namespace KeyboardKata.Domain.Tests.Extensions.Json.Models
{
    public class HasColor
    {
        public HasColor(IColor color)
        {
            Color = color;
        }

        public IColor Color { get; }
    }
}
