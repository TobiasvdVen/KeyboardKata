using KeyboardKata.App.Shortcuts;
using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Tests.Helpers;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace KeyboardKata.App.Tests.Shortcuts
{
    public class ShortcutProcessorTests
    {
        [Fact]
        public void GivenSingleInputShortcut_WhenInput_ThenExecuteCommand()
        {
            Mock<Func<Task>> action = new();

            IPattern pattern = Stubs.Pattern(Stubs.Down("S"));

            ShortcutProcessor shortcutProcessor = new(new ShortcutCommand("TestCommand", action.Object, pattern));
            InputProcessor inputProcessor = new(shortcutProcessor, pattern);

            inputProcessor.Process(Stubs.Down("S"));

            action.Verify(a => a(), Times.Once);
        }
    }
}
