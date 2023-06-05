using KeyboardKata.Domain;
using WindowsInput.Events;
using Xunit;

namespace KeyboardKata.InputSources.Windows.Tests
{
    public class WindowsKeyCodeMapperTests
    {
        private readonly WindowsKeyCodeMapper _mapper;

        public WindowsKeyCodeMapperTests()
        {
            _mapper = new WindowsKeyCodeMapper();
        }

        [Theory]
        [InlineData(KeyCode.L, "L")]
        [InlineData(KeyCode.LControl, "LControl")]
        public void GivenKeyCode_WhenMapped_ThenReturnDescriptor(KeyCode keyCode, string expected)
        {
            Assert.Equal(expected, _mapper.Descriptor(keyCode));
        }

        [Fact]
        public void GivenDescriptor_WhenMapped_ThenReturnKey()
        {
            Key key = _mapper.Key("LControl");

            Assert.Equal(0xA2, key.KeyCode);
        }
    }
}
