using Microsoft.Extensions.Hosting;

namespace KeyboardKata.Domain
{
    public class QuitProcessor : IInputProcessor
    {
        private readonly Pattern _quitPattern;
        private readonly IInputProcessor _inputProcessor;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public QuitProcessor(Pattern quitPattern, IInputProcessor inputProcessor, IHostApplicationLifetime applicationLifetime)
        {
            _quitPattern = quitPattern;
            _inputProcessor = inputProcessor;
            _applicationLifetime = applicationLifetime;
        }

        public InputContinuation Process(Input input)
        {
            PatternMatcher matcher = new();

            if (matcher.Evaluate(new List<Input>() { input }, _quitPattern) == Match.Full)
            {
                _applicationLifetime.StopApplication();

                return InputContinuation.Unsafe;
            }

            _inputProcessor.Process(input);

            return InputContinuation.Safe;
        }
    }
}
