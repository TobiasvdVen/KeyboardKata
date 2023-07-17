using KeyboardKata.Domain.InputMatching;

namespace KeyboardKata.Domain.InputProcessing
{
    public class InputProcessor : IInputProcessor
    {
        private readonly IPatternProcessor _patternProcessor;
        private readonly IPattern _pattern;

        public InputProcessor(IPatternProcessor patternProcessor, IPattern pattern)
        {
            _patternProcessor = patternProcessor;
            _pattern = pattern;
        }

        public InputContinuation Process(Input input)
        {
            if (_pattern.Matches(new[] { input }) == Match.Full)
            {
                _patternProcessor.Process(_pattern);

                return InputContinuation.Safe;
            }

            return InputContinuation.Safe;
        }
    }
}
