using KeyboardKata.Domain.InputProcessing;
using System;

namespace KeyboardKata.Tests.Abstractions
{
    public class KeyDown : IDisposable
    {
        private readonly IKeyboard _keyboard;

        public Key Key { get; }

        public KeyDown(Key key, IKeyboard keyboard)
        {
            Key = key;
            _keyboard = keyboard;
        }

        public void Release()
        {
            Dispose();
        }

        public void Dispose()
        {
            _keyboard.Release(Key);
        }
    }
}
