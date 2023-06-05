using KeyboardKata.Domain;
using WindowsInput.Events;

namespace KeyboardKata.InputSources.Windows
{
    public class WindowsKeyCodeMapper : IKeyCodeMapper
    {
        public string Descriptor(KeyCode keyCode)
        {
            return keyCode.ToString();
        }

        public string Descriptor(int keyCode)
        {
            throw new NotImplementedException();
        }

        public string Descriptor(Key key)
        {
            throw new NotImplementedException();
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
    }
}
