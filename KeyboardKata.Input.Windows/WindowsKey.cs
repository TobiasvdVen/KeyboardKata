using KeyboardKata.Domain;
using WindowsInput.Events;

namespace KeyboardKata.InputSources.Windows
{
    internal record WindowsKey : Key
    {
        public WindowsKey(KeyCode keyCode)
        {
            KeyCode = keyCode;
        }

        public override string DisplayName => KeyCode.ToString();
        public KeyCode KeyCode { get; }

        public override string ToString()
        {
            return KeyCode.ToString();
        }
    }
}
