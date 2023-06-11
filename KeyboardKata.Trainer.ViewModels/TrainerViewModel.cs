using CommunityToolkit.Mvvm.ComponentModel;
using KeyboardKata.Domain;

namespace KeyboardKata.Trainer.ViewModels
{
    public partial class TrainerViewModel : ObservableObject, IKeyboardKata
    {
        [ObservableProperty]
        private string _currentPrompt;

        public TrainerViewModel(string initialPrompt)
        {
            _currentPrompt = initialPrompt;
        }

        public void Failure(KeyboardAction action, IEnumerable<Key> actual)
        {
            throw new NotImplementedException();
        }

        public void Progress(KeyboardAction action, IEnumerable<Key> remaining)
        {
            throw new NotImplementedException();
        }

        public void Success(KeyboardAction action)
        {
            CurrentPrompt = "You did it!";
        }

        public void Prompt(KeyboardAction action)
        {
            CurrentPrompt = action.Prompt;
        }
    }
}
