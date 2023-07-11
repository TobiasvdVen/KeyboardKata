using KeyboardKata.Domain.InputDetection.SharpHook;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Tests.Abstractions;
using SharpHook;
using SharpHook.Native;
using System.Threading.Tasks;
using Xunit;

namespace KeyboardKata.Tests.Infrastructure.SharpHook
{
    public class SharpHookKeyboard : IKeyboard
    {
        private readonly EventSimulator _eventSimulator;
        private readonly SharpHookKeyCodeMapper _keyCodeMapper;

        public SharpHookKeyboard(SharpHookKeyCodeMapper keyCodeMapper)
        {
            _eventSimulator = new EventSimulator();
            _keyCodeMapper = keyCodeMapper;
        }

        public async Task PressAndReleaseAsync(Key key)
        {
            using KeyDown keyDown = await PressAsync(key);
        }

        public Task<KeyDown> PressAsync(Key key)
        {
            UioHookResult result = _eventSimulator.SimulateKeyPress(_keyCodeMapper.KeyCode(key));

            Assert.Equal(UioHookResult.Success, result);

            return Task.FromResult(new KeyDown(key, this));
        }

        public void Release(Key key)
        {
            UioHookResult result = _eventSimulator.SimulateKeyRelease(_keyCodeMapper.KeyCode(key));

            Assert.Equal(UioHookResult.Success, result);
        }
    }
}
