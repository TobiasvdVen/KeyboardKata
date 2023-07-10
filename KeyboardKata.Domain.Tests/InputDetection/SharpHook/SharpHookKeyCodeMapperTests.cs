using KeyboardKata.Domain.InputDetection.SharpHook;
using KeyboardKata.Domain.InputProcessing;
using SharpHook.Native;
using Xunit;

namespace KeyboardKata.Domain.Tests.InputDetection.SharpHook
{
    public class SharpHookKeyCodeMapperTests
    {
        private readonly SharpHookKeyCodeMapper _mapper;

        public SharpHookKeyCodeMapperTests()
        {
            _mapper = new SharpHookKeyCodeMapper();
        }

        [Theory]
        [InlineData(KeyCode.VcL, "L")]
        [InlineData(KeyCode.VcLeftControl, "LeftControl")]
        public void GivenKeyCode_WhenMapped_ThenReturnDescriptor(KeyCode keyCode, string expected)
        {
            Assert.Equal(expected, _mapper.Descriptor(keyCode));
        }

        [Fact]
        public void GivenDescriptor_WhenMapped_ThenReturnKey()
        {
            Key key = _mapper.Key("LeftControl");

            Assert.Equal(0x1D, key.KeyCode);
        }
    }
}
