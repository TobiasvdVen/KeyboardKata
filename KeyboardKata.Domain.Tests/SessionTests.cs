using KeyboardKata.Domain.Tests.Helpers;
using Moq;
using Xunit;

namespace KeyboardKata.Domain.Tests
{
    public class SessionTests
    {
        private readonly Session _session;
        private readonly Mock<IKeyboardKata> _kata;

        public SessionTests()
        {
            _kata = new Mock<IKeyboardKata>();
            _session = new Session(_kata.Object);
        }

        [Fact]
        public void GivenNewSession_WhenNextPrompt_ThenKeyboardKataIsPrompted()
        {
            KeyboardAction action = new("Do something!", Stubs.Pattern(Stubs.Down("A")));

            _session.NextPrompt();

            _kata.Verify(k => k.Prompt(action), Times.Once);
        }
    }
}
