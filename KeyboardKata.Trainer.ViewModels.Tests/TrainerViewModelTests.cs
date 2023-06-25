using KeyboardKata.Domain.Actions;
using KeyboardKata.Domain.Tests.Helpers;
using Xunit;

namespace KeyboardKata.Trainer.ViewModels.Tests
{
    public class TrainerViewModelTests
    {
        private readonly TrainerViewModel _trainerViewModel;

        public TrainerViewModelTests()
        {
            _trainerViewModel = new TrainerViewModel();
        }

        [Fact]
        public void WhenPrompt_ThenShowPrompt()
        {
            _trainerViewModel.Prompt(new KeyboardAction("Do something!", Stubs.Pattern(Stubs.Down("C"))));

            Assert.Equal("Do something!", _trainerViewModel.CurrentPrompt);
        }

        [Fact]
        public void WhenSuccess_ThenPromptShowsSuccessMessage()
        {
            _trainerViewModel.Success(new KeyboardAction("Do something!", Stubs.Pattern(Stubs.Down("N"))));

            Assert.Equal("You did it!", _trainerViewModel.CurrentPrompt);
        }
    }
}
