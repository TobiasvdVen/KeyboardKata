namespace KeyboardKata.Domain.Tests.Extensions.Json.Models
{
    public class HasComponent
    {
        public HasComponent(IComponent someComponent)
        {
            SomeComponent = someComponent;
        }

        public IComponent SomeComponent { get; }
    }
}
