namespace KeyboardKata.Domain.Tests.Extensions.Json.Models
{
    public class ColorComponent : IComponent
    {
        public ColorComponent(HasColor hasColor)
        {
            HasColor = hasColor;
        }

        public HasColor HasColor { get; }
    }
}
