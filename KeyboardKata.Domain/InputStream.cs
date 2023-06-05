using System.Collections;

namespace KeyboardKata.Domain
{
    public class InputStream : IEnumerable<Input>
    {
        private readonly Input[] _inputs;

        public InputStream(params Input[] inputs)
        {
            _inputs = inputs;
        }

        public int Position { get; private set; }

        public IEnumerator<Input> GetEnumerator() => ((IEnumerable<Input>)_inputs).GetEnumerator();

        public Input Next()
        {
            return _inputs[Position++];
        }

        IEnumerator IEnumerable.GetEnumerator() => _inputs.GetEnumerator();
    }
}
