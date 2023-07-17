using KeyboardKata.App.Commands;
using KeyboardKata.App.Shortcuts;
using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Tests.Helpers;
using Moq;
using Xunit;

namespace KeyboardKata.App.Tests.Shortcuts
{
    public class ShortcutProcessorTests
    {
        private readonly Mock<ICommandProcessor> _commandProcessor;

        public ShortcutProcessorTests()
        {
            _commandProcessor = new Mock<ICommandProcessor>();
        }

        [Fact]
        public void GivenSingleInputShortcut_WhenInput_ThenExecuteCommand()
        {
            Command command = new Mock<Command>().Object;
            IPattern pattern = Stubs.Pattern(Stubs.Down("S"));

            ShortcutProcessor shortcutProcessor = new(_commandProcessor.Object, new ShortcutCommand(command, pattern));
            InputProcessor inputProcessor = new(shortcutProcessor, pattern);

            inputProcessor.Process(Stubs.Down("S"));

            _commandProcessor.Verify(p => p.ProcessAsync(command), Times.Once);
        }
    }
}
