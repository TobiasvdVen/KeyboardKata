using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Tests.Helpers;
using Microsoft.Extensions.Hosting;
using Moq;
using Xunit;

namespace KeyboardKata.Domain.Tests
{
    public class QuitProcessorTests
    {
        private readonly Mock<IHostApplicationLifetime> _applicationLifetime;
        private readonly Mock<IInputProcessor> _inputProcessor;

        public QuitProcessorTests()
        {
            _applicationLifetime = new Mock<IHostApplicationLifetime>();
            _inputProcessor = new Mock<IInputProcessor>();
        }

        [Fact]
        public void GivenSingleKeyQuitAction_WhenProcessed_ThenQuit()
        {
            ExactMatchPattern quitPattern = Stubs.Pattern(Stubs.Down("Q"));
            QuitProcessor quitProcessor = new(quitPattern, _inputProcessor.Object, _applicationLifetime.Object);

            quitProcessor.Process(Stubs.Down("Q"));

            _applicationLifetime.Verify(a => a.StopApplication(), Times.Once);
            _inputProcessor.Verify(p => p.Process(It.IsAny<Input>()), Times.Never);
        }

        [Fact]
        public void GivenSingleKeyQuitAction_WhenUnrelatedInputProcessed_ThenForwardInput()
        {
            ExactMatchPattern quitPattern = Stubs.Pattern(Stubs.Down("Q"));
            QuitProcessor quitProcessor = new(quitPattern, _inputProcessor.Object, _applicationLifetime.Object);

            quitProcessor.Process(Stubs.Down("P"));

            _inputProcessor.Verify(p => p.Process(It.IsAny<Input>()), Times.Once);
        }
    }
}
