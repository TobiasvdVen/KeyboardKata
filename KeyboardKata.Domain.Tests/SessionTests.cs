using KeyboardKata.Domain.Tests.Helpers;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace KeyboardKata.Domain.Tests
{
    public class SessionTests
    {
        private readonly SessionState _session;
        private readonly Mock<IKeyboardKata> _kata;
        private readonly Mock<IKeyboardActionProvider> _actionProvider;

        public SessionTests()
        {
            _kata = new Mock<IKeyboardKata>();
            _actionProvider = new Mock<IKeyboardActionProvider>();
            _session = new SessionState(_kata.Object, _actionProvider.Object, new NullLogger<SessionState>());
        }

        [Fact]
        public void GivenNewSession_WhenNextPrompt_ThenPrompt()
        {
            KeyboardAction action = new("Do something!", Stubs.Pattern(Stubs.Down("A")));

            _actionProvider.Setup(p => p.GetKeyboardAction()).Returns(action);

            _session.NextPrompt();

            _kata.Verify(k => k.Prompt(action), Times.Once);
        }

        [Fact]
        public void GivenPrompt_WhenCorrectInput_ThenSuccess()
        {
            KeyboardAction action = new("Do something!", Stubs.Pattern(Stubs.Down("A")));

            _actionProvider.Setup(p => p.GetKeyboardAction()).Returns(action);

            _session.NextPrompt();
            _session.Process(Stubs.Down("A"));

            _kata.Verify(k => k.Success(action), Times.Once);
        }

        [Fact]
        public void GivenSuccessfulInput_WhenAllKeyReleased_ThenNextPrompt()
        {
            KeyboardAction action = new("Do something!", Stubs.Pattern(Stubs.Down("A")));

            _actionProvider.Setup(p => p.GetKeyboardAction()).Returns(action);

            _session.NextPrompt();
            _session.Process(Stubs.Down("A"));
            _session.Process(Stubs.Up("A"));

            _kata.Verify(k => k.Prompt(action), Times.Exactly(2));
        }
    }
}
