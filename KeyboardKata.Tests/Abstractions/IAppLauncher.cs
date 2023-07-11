using System.Threading.Tasks;

namespace KeyboardKata.Tests.Abstractions
{
    public interface IAppLauncher
    {
        Task<IApp> LaunchAppAsync();
    }
}
