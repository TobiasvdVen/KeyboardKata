using KeyboardKata.Domain.InputMatching;
using Microsoft.Extensions.Hosting;

namespace KeyboardKata.Domain.InputProcessing
{
    public class QuitProcessor : IInputProcessor
    {
        private readonly IPattern _quitPattern;
        private readonly IInputProcessor _inputProcessor;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public QuitProcessor(IPattern quitPattern, IInputProcessor inputProcessor, IHostApplicationLifetime applicationLifetime)
        {
            _quitPattern = quitPattern;
            _inputProcessor = inputProcessor;
            _applicationLifetime = applicationLifetime;
        }

        public InputContinuation Process(Input input)
        {
            if (_quitPattern.Matches(new List<Input>() { input }) == Match.Full)
            {
                _applicationLifetime.StopApplication();

                return InputContinuation.Unsafe;
            }

            _inputProcessor.Process(input);

            return InputContinuation.Safe;
        }
    }
}
