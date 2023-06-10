namespace KeyboardKata.Trainer.ViewModels
{
    public class TrainerViewModel
    {
        public TrainerViewModel(string prompt)
        {
            Prompt = prompt;
        }

        public string Prompt { get; }
    }
}
