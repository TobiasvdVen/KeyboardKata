using KeyboardKata.App.Shortcuts;
using KeyboardKata.App.ViewModels;
using KeyboardKata.Domain.Sessions;
using KeyboardKata.Domain.Tests.Helpers;
using Moq;
using Xunit;

namespace KeyboardKata.App.Tests.ViewModels
{
    public class MainViewModelTests
    {
        private readonly MainViewModel _mainViewModel;
        private readonly Mock<IAppVisibility> _appVisibilityMock;
        private readonly Mock<ITrainerSession> _trainerSessionMock;

        public MainViewModelTests()
        {
            TestKeyCodeMapper testKeyCodeMapper = new();
            DefaultShortcuts defaultShortcuts = new(testKeyCodeMapper);
            ShortcutCommandRegistry shortcutCommandRegistry = defaultShortcuts.BuildRegistry();
            ShortcutCommandManager shortcutCommandManager = new(shortcutCommandRegistry);

            _trainerSessionMock = new Mock<ITrainerSession>();
            _appVisibilityMock = new Mock<IAppVisibility>();

            _mainViewModel = new MainViewModel(_trainerSessionMock.Object, shortcutCommandManager, _appVisibilityMock.Object);
        }

        [Fact]
        public void Test()
        {
            _mainViewModel.StartSessionCommand.Execute();

            _trainerSessionMock.Verify(s => s.Start(), Times.Once());
        }
    }
}
