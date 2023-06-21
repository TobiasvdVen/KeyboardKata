using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KeyboardKata.Domain.Sessions;

namespace KeyboardKata.App.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ITrainerSession _trainerSession;

        [ObservableProperty]
        private bool _trainerActive;

        [ObservableProperty]
        private SessionResult? _sessionResult;

        public MainViewModel(ITrainerSession trainerSession)
        {
            _trainerSession = trainerSession;
            _trainerSession.Ended += TrainerSession_Ended;
        }

        [RelayCommand]
        public void StartTrainer()
        {
            _trainerSession.Start();
            TrainerActive = true;
        }

        [RelayCommand]
        public void StopTrainer()
        {
            _trainerSession.End();
        }

        [RelayCommand]
        public void Reset()
        {
            SessionResult = null;
        }

        private void TrainerSession_Ended(SessionResult? result)
        {
            TrainerActive = false;
            SessionResult = result;
        }
    }
}
