using KeyboardKata.App.Shortcuts;
using KeyboardKata.Domain.Sessions;

namespace KeyboardKata.App.ViewModels
{
    public class MainViewModel
    {
        private readonly ITrainerSession _trainerSession;
        private readonly IAppVisibility _appVisibility;

        public MainViewModel(ITrainerSession trainerSession, IShortcutCommands commands, IAppVisibility appVisibility)
        {
            _trainerSession = trainerSession;
            _appVisibility = appVisibility;

            StartSessionCommand = commands.GetShortcut("StartSession", StartSession);
            ResetCommand = commands.GetShortcut("ResetSessionResult", ResetSessionResult);

            _trainerSession.Ended += TrainerSession_Ended;
        }

        public ShortcutCommand StartSessionCommand { get; }
        public ShortcutCommand ResetCommand { get; }
        public SessionResult? SessionResult { get; private set; }

        public void StartSession()
        {
            _appVisibility.Visible = false;

            _trainerSession.Start();
        }

        public void ResetSessionResult()
        {
            _trainerSession.End();
            SessionResult = null;

            _appVisibility.Visible = true;
        }

        private void TrainerSession_Ended(SessionResult? result)
        {
            SessionResult = result;
        }
    }
}
