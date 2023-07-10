using KeyboardKata.Domain.InputProcessing;
using SharpHook.Native;

namespace KeyboardKata.Domain.InputDetection.SharpHook
{
    public class SharpHookKeyCodeMapper : IKeyCodeMapper
    {
        public string Descriptor(KeyCode keyCode)
        {
            string? asString = keyCode.ToString();

            // Skip "Vc" prefix
            return asString[2..];
        }

        public string Descriptor(int keyCode)
        {
            KeyCode windowsKeyCode = (KeyCode)keyCode;

            return Descriptor(windowsKeyCode);
        }

        public string Descriptor(Key key)
        {
            return Descriptor(key.KeyCode);
        }

        public Key Key(int keyCode)
        {
            return new Key(keyCode);
        }

        public Key Key(string descriptor)
        {
            int keyCode = (int)Enum.Parse<KeyCode>($"Vc{descriptor}");

            return Key(keyCode);
        }

        public Key Key(KeyCode keyCode)
        {
            return Key((int)keyCode);
        }
    }
}
