using KeyboardKata.Tests.Abstractions;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KeyboardKata.Tests.Infrastructure.Windows
{
    public class WpfTrainerFinder : ITrainerFinder
    {
        public Task<ITrainer> FindTrainerAsync()
        {
            Process? trainer = FindTrainer();

            Assert.NotNull(trainer);

            return Task.FromResult<ITrainer>(new WpfTrainer());
        }

        public bool TrainerIsRunning()
        {
            return FindTrainer() is not null;
        }

        private Process? FindTrainer()
        {
            return Process.GetProcessesByName("Keyboard Kata Trainer.exe").FirstOrDefault();
        }
    }
}
