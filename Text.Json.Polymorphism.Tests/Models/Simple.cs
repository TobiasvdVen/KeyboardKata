namespace Text.Json.Polymorphism.Tests.Models
{
    public record Simple
    {
        public Simple(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
