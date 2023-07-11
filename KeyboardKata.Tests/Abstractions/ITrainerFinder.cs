using System.Threading.Tasks;

namespace KeyboardKata.Tests.Abstractions
{
    public interface ITrainerFinder
    {
        Task<ITrainer> FindTrainerAsync();
        bool TrainerIsRunning();
    }
}
