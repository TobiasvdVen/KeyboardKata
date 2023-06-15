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

        private void TrainerSession_Ended(SessionResult result)
        {
            TrainerActive = false;
        }
    }
}
