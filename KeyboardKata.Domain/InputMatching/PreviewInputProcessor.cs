using KeyboardKata.Domain.InputProcessing;

namespace KeyboardKata.Domain.InputMatching
{
    public class PreviewInputProcessor : IInputProcessor
    {
        private readonly List<InputPreview> _preview;
        private readonly InputTracker _tracker;

        public PreviewInputProcessor(IPattern pattern)
        {
            _preview = new List<InputPreview>();
            _tracker = new InputTracker();

            Pattern = pattern;
        }

        public IPattern Pattern { get; }
        public IEnumerable<InputPreview> Preview => _preview;

        public InputContinuation Process(Input input)
        {
            _tracker.Add(input);

            throw new NotImplementedException();
        }
    }
}
