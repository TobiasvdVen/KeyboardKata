using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Tests.Helpers;
using Xunit;

namespace KeyboardKata.Domain.Tests
{
    public class ExactMatchPatternTests
    {
        [Fact]
        public void GivenSingleKeyPattern_WhenSingleKeyInput_ThenMatch()
        {
            ExactMatchPattern pattern = Stubs.Pattern(Stubs.Down("A"));

            Input[] inputs = new[] { Stubs.Down("A") };

            Match match = pattern.Matches(inputs);

            Assert.Equal(Match.Full, match);
        }
    }
}
