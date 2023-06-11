using CommunityToolkit.Mvvm.ComponentModel;

namespace KeyboardKata.Trainer.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private TrainerViewModel _trainerViewModel;

        public MainViewModel(TrainerViewModel trainerViewModel)
        {
            _trainerViewModel = trainerViewModel;
        }
    }
}
