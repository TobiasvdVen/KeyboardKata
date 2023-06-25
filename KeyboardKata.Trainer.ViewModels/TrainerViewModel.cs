using CommunityToolkit.Mvvm.ComponentModel;
using KeyboardKata.Domain;
using KeyboardKata.Domain.Actions;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Sessions;
using System.Text.Json;

namespace KeyboardKata.Trainer.ViewModels
{
    public partial class TrainerViewModel : ObservableObject, IKeyboardKata
    {
        [ObservableProperty]
        private string _currentPrompt;

        public TrainerViewModel()
        {
            CurrentPrompt = "Welcome to Keyboard Kata!";
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

        public void Finish(SessionResult result)
        {
            CurrentPrompt = "All done!";

            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            options.AddContext<SessionResultJsonContext>();

            string json = JsonSerializer.Serialize(result, options);

            Console.WriteLine(json);
        }
    }
}
