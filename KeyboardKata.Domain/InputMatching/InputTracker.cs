using KeyboardKata.Domain.InputProcessing;

namespace KeyboardKata.Domain.InputMatching
{
    public class InputTracker
    {
        private readonly List<Input> _inputs;

        public InputTracker()
        {
            _inputs = new List<Input>();

            KeysDown = Enumerable.Empty<Key>();
        }

        public IEnumerable<Input> Inputs => _inputs;

        public IEnumerable<Key> KeysDown { get; }

        public void Add(Input input)
        {
            _inputs.Add(input);
        }

        public void Reset()
        {
            _inputs.Clear();
        }
    }
}
