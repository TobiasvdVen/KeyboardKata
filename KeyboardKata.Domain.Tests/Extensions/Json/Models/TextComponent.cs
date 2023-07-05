namespace KeyboardKata.Domain.Tests.Extensions.Json.Models
{
    public class TextComponent : IComponent
    {
        public TextComponent(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
