using KeyboardKata.Domain;
using Moq;
using WindowsInput.Events;
using WindowsInput.Events.Sources;
using Xunit;

namespace KeyboardKata.InputSources.Windows.Tests
{
    public class WindowsInputDelegatorTests
    {
        private readonly Mock<IInputProcessor> _inputProcessor;
        private readonly WindowsInputDelegator _inputDelegator;

        public WindowsInputDelegatorTests()
        {
            _inputProcessor = new Mock<IInputProcessor>(MockBehavior.Strict);
            _inputDelegator = new WindowsInputDelegator(_inputProcessor.Object);
        }

        [Fact]
        public void GivenKeyDown_ThenInputIsDelegatedToProcessor()
        {
            EventSourceEventArgs<KeyDown> keyDownEvent = KeyDown(KeyCode.K);

            _inputProcessor.Setup(p => p.Process(It.IsAny<Input>())).Returns(InputContinuation.Safe).Verifiable();

            _inputDelegator.KeyDown(keyDownEvent);

            _inputProcessor.Verify(p => p.Process(It.Is<Input>(input => InputEquals(input, KeyCode.K, KeyPress.Down))), Times.Once);
        }

        [Fact]
        public void GivenKeyUp_ThenInputIsDelegatedToProcessor()
        {
            EventSourceEventArgs<KeyUp> keyUpEvent = KeyUp(KeyCode.U);

            _inputProcessor.Setup(p => p.Process(It.IsAny<Input>())).Returns(InputContinuation.Safe).Verifiable();

            _inputDelegator.KeyUp(keyUpEvent);

            _inputProcessor.Verify(p => p.Process(It.Is<Input>(input => InputEquals(input, KeyCode.U, KeyPress.Up))), Times.Once);
        }

        [Fact]
        public void GivenKeyDown_WhenProcessorReturnsUnsafeContinuation_ThenStopFurtherInputProcessing()
        {
            EventSourceEventArgs<KeyDown> keyDownEvent = KeyDown(KeyCode.A);

            _inputProcessor.Setup(p => p.Process(It.IsAny<Input>())).Returns(InputContinuation.Unsafe).Verifiable();

            _inputDelegator.KeyDown(keyDownEvent);

            Assert.False(keyDownEvent.Next_Hook_Enabled);
        }

        [Fact]
        public void GivenKeyUp_WhenProcessorReturnsUnsafeContinuation_ThenStopFurtherInputProcessing()
        {
            EventSourceEventArgs<KeyUp> keyUpEvent = KeyUp(KeyCode.B);

            _inputProcessor.Setup(p => p.Process(It.IsAny<Input>())).Returns(InputContinuation.Unsafe).Verifiable();

            _inputDelegator.KeyUp(keyUpEvent);

            Assert.False(keyUpEvent.Next_Hook_Enabled);
        }

        private EventSourceEventArgs<KeyDown> KeyDown(KeyCode keyCode)
        {
            return new(0, new KeyDown(keyCode), new object());
        }

        private EventSourceEventArgs<KeyUp> KeyUp(KeyCode keyCode)
        {
            return new(0, new KeyUp(keyCode), new object());
        }

        private bool InputEquals(Input input, KeyCode keyCode, KeyPress keyPress)
        {
            return input.Key == new Key((int)keyCode) && input.KeyPress == keyPress;
        }
    }
}