using KeyboardKata.Domain;
using WindowsInput.Events;

namespace KeyboardKata.Windows
{
    public class WindowsKeyCodeMapper : IKeyCodeMapper
    {
        public string Descriptor(KeyCode keyCode)
        {
            return keyCode.ToString();
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
            int keyCode = (int)Enum.Parse<KeyCode>(descriptor);

            return Key(keyCode);
        }

        public Key Key(KeyCode keyCode)
        {
            return Key((int)keyCode);
        }
    }
}
