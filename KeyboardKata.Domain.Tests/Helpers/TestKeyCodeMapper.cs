using KeyboardKata.Domain.InputProcessing;

namespace KeyboardKata.Domain.Tests.Helpers
{
    public class TestKeyCodeMapper : IKeyCodeMapper
    {
        public string Descriptor(int keyCode)
        {
            return keyCode.ToString();
        }

        public string Descriptor(Key key)
        {
            return key.ToString();
        }

        public Key Key(int keyCode)
        {
            return new TestKey(keyCode.ToString());
        }

        public Key Key(string descriptor)
        {
            return new TestKey(descriptor);
        }
    }
}
