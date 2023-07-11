using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Tests.Abstractions;
using KeyboardKata.Tests.Startup;
using System.Threading.Tasks;
using Xunit;
using Xunit.DependencyInjection;

namespace KeyboardKata.App.ImGui.Windows.Tests
{
    [Startup(typeof(WindowsStartup))]
    public class StartApp_StartTrainer_ExitTrainer
    {
        private readonly IAppLauncher _appLauncher;
        private readonly ITrainerFinder _trainerFinder;
        private readonly IKeyboard _keyboard;
        private readonly IKeyCodeMapper _mapper;

        public StartApp_StartTrainer_ExitTrainer(IAppLauncher appLauncher, ITrainerFinder trainerFinder, IKeyboard keyboard, IKeyCodeMapper mapper)
        {
            _appLauncher = appLauncher;
            _trainerFinder = trainerFinder;
            _keyboard = keyboard;
            _mapper = mapper;
        }

        [Fact]
        public async Task Test()
        {
            using IApp app = await _appLauncher.LaunchAppAsync();

            Assert.True(app.IsVisible);

            await _keyboard.PressAndReleaseAsync(_mapper.Key("S"));

            ITrainer trainer = await _trainerFinder.FindTrainerAsync();

            await _keyboard.PressAndReleaseAsync(_mapper.Key("Q"));

            Assert.False(_trainerFinder.TrainerIsRunning());
        }
    }
}
