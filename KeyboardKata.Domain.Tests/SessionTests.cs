using KeyboardKata.Domain.Tests.Helpers;
using Moq;
using Xunit;

namespace KeyboardKata.Domain.Tests
{
    public class SessionTests
    {
        private readonly Session _session;
        private readonly Mock<IKeyboardKata> _kata;
        private readonly Mock<IKeyboardActionProvider> _actionProvider;

        public SessionTests()
        {
            _kata = new Mock<IKeyboardKata>();
            _actionProvider = new Mock<IKeyboardActionProvider>();
            _session = new Session(_kata.Object, _actionProvider.Object);
        }

        [Fact]
        public void GivenNewSession_WhenNextPrompt_ThenKeyboardKataIsPrompted()
        {
            KeyboardAction action = new("Do something!", Stubs.Pattern(Stubs.Down("A")));

            _actionProvider.Setup(p => p.GetKeyboardAction()).Returns(action);

            _session.NextPrompt();

            _kata.Verify(k => k.Prompt(action), Times.Once);
        }
    }
}
