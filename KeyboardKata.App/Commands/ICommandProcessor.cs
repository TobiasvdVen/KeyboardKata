using System.Threading.Tasks;

namespace KeyboardKata.App.Commands
{
    public interface ICommandProcessor
    {
        Task ProcessAsync(Command command);
    }
}
