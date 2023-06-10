using KeyboardKata.Domain.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;

namespace KeyboardKata.Domain.Tests
{
    public class InputProcessorTests
    {
        private readonly Mock<IKeyboardKata> _keyboardKata;
        private readonly IKeyboardActionProvider _keyboardActionProvider;
        private readonly IInputProcessor _inputProcessor;

        public InputProcessorTests()
        {
            _keyboardKata = new Mock<IKeyboardKata>();
            _keyboardActionProvider = new ExampleKeyboardActionProvider(new TestKeyCodeMapper());
            _inputProcessor = new Session(_keyboardKata.Object, _keyboardActionProvider, new Mock<ILogger<Session>>().Object);
        }
    }
}
