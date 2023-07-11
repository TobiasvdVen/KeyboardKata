using KeyboardKata.Domain.InputProcessing;
using System.Threading.Tasks;

namespace KeyboardKata.Tests.Abstractions
{
    public interface IKeyboard
    {
        Task<KeyDown> PressAsync(Key key);
        void Release(Key key);
        Task PressAndReleaseAsync(Key key);
    }
}
