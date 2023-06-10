namespace KeyboardKata.Trainer.ViewModels
{
    public class MainViewModel
    {
        public TrainerViewModel TrainerViewModel { get; }

        public MainViewModel(TrainerViewModel trainerViewModel)
        {
            TrainerViewModel = trainerViewModel;
        }
    }
}
