using KeyboardKata.Domain.Actions;
using KeyboardKata.Domain.Actions.Sources;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Sessions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;

namespace KeyboardKata.Domain.Tests
{
    public class InputProcessorTests
    {
        private readonly Mock<IKeyboardKata> _keyboardKata;
        private readonly IKeyboardActionSource _keyboardActionProvider;
        private readonly IInputProcessor _inputProcessor;

        public InputProcessorTests()
        {
            _keyboardKata = new Mock<IKeyboardKata>();
            _keyboardActionProvider = new LinearKeyboardActionSource(Enumerable.Empty<KeyboardAction>());
            _inputProcessor = new SessionState(_keyboardKata.Object, _keyboardActionProvider, new Mock<ILogger<SessionState>>().Object);
        }
    }
}
