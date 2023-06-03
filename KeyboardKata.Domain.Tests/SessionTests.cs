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
        public void GivenSingleInputSequence_WhenCorrectInput_ThenSuccess()
        {

        }
    }
}
