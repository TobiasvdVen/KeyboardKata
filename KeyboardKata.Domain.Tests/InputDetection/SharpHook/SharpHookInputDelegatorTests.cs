using KeyboardKata.Domain.InputDetection.SharpHook;
using KeyboardKata.Domain.InputProcessing;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SharpHook;
using SharpHook.Native;
using Xunit;

namespace KeyboardKata.Domain.Tests.InputDetection.SharpHook
{
    public class SharpHookInputDelegatorTests
    {
        private readonly Mock<IInputProcessor> _inputProcessor;
        private readonly SharpHookInputDelegator _inputDelegator;

        public SharpHookInputDelegatorTests()
        {
            _inputProcessor = new Mock<IInputProcessor>(MockBehavior.Strict);
            _inputDelegator = new SharpHookInputDelegator(_inputProcessor.Object, new SharpHookKeyCodeMapper(), new NullLogger<SharpHookInputDelegator>());
        }

        [Fact]
        public void GivenKeyDown_ThenInputIsDelegatedToProcessor()
        {
            KeyboardHookEventArgs keyDownEvent = KeyDown(KeyCode.VcK);

            _inputProcessor.Setup(p => p.Process(It.IsAny<Input>())).Returns(InputContinuation.Safe).Verifiable();

            _inputDelegator.Delegate(keyDownEvent);

            _inputProcessor.Verify(p => p.Process(It.Is<Input>(input => InputEquals(input, KeyCode.VcK, KeyPress.Down))), Times.Once);
        }

        [Fact]
        public void GivenKeyUp_ThenInputIsDelegatedToProcessor()
        {
            KeyboardHookEventArgs keyUpEvent = KeyUp(KeyCode.VcU);

            _inputProcessor.Setup(p => p.Process(It.IsAny<Input>())).Returns(InputContinuation.Safe).Verifiable();

            _inputDelegator.Delegate(keyUpEvent);

            _inputProcessor.Verify(p => p.Process(It.Is<Input>(input => InputEquals(input, KeyCode.VcU, KeyPress.Up))), Times.Once);
        }

        [Fact]
        public void GivenKeyDown_WhenProcessorReturnsUnsafeContinuation_ThenStopFurtherInputProcessing()
        {
            KeyboardHookEventArgs keyDownEvent = KeyDown(KeyCode.VcA);

            _inputProcessor.Setup(p => p.Process(It.IsAny<Input>())).Returns(InputContinuation.Unsafe).Verifiable();

            _inputDelegator.Delegate(keyDownEvent);

            Assert.True(keyDownEvent.SuppressEvent);
        }

        [Fact]
        public void GivenKeyUp_WhenProcessorReturnsUnsafeContinuation_ThenStopFurtherInputProcessing()
        {
            KeyboardHookEventArgs keyUpEvent = KeyUp(KeyCode.VcB);

            _inputProcessor.Setup(p => p.Process(It.IsAny<Input>())).Returns(InputContinuation.Unsafe).Verifiable();

            _inputDelegator.Delegate(keyUpEvent);

            Assert.True(keyUpEvent.SuppressEvent);
        }

        private KeyboardHookEventArgs KeyDown(KeyCode keyCode)
        {
            return new KeyboardHookEventArgs(new UioHookEvent()
            {
                Type = EventType.KeyPressed,
                Keyboard = new KeyboardEventData()
                {
                    KeyCode = keyCode
                }
            });
        }

        private KeyboardHookEventArgs KeyUp(KeyCode keyCode)
        {
            return new KeyboardHookEventArgs(new UioHookEvent()
            {
                Type = EventType.KeyReleased,
                Keyboard = new KeyboardEventData()
                {
                    KeyCode = keyCode
                }
            });
        }

        private bool InputEquals(Input input, KeyCode keyCode, KeyPress keyPress)
        {
            return input.Key == new Key((int)keyCode) && input.KeyPress == keyPress;
        }
    }
}
