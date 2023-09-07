using KeyboardKata.App.Shortcuts;
using KeyboardKata.Domain.Sessions;

namespace KeyboardKata.App.ViewModels
{
    public class MainViewModel
    {
        private readonly SessionRunner _sessionRunner;
        private readonly IAppVisibility _appVisibility;

        public MainViewModel(IShortcutCommands commands, SessionRunner sessionRunner, IAppVisibility appVisibility)
        {
            _sessionRunner = sessionRunner;
            _appVisibility = appVisibility;

            StartSessionCommand = commands.GetShortcut("StartSession", StartSession);
            ResetCommand = commands.GetShortcut("ResetSessionResult", ResetSessionResult);
        }

        public ShortcutCommand StartSessionCommand { get; }
        public ShortcutCommand ResetCommand { get; }
        public SessionResult? SessionResult => _sessionRunner.SessionResult;


        public void StartSession()
        {
            _appVisibility.Visible = false;

            _sessionRunner.StartTrainer();
        }

        public void ResetSessionResult()
        {
            _sessionRunner.Reset();

            _appVisibility.Visible = true;
        }
    }
}
