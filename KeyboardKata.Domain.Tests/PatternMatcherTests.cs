using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Tests.Helpers;
using Xunit;

namespace KeyboardKata.Domain.Tests
{
    public class PatternMatcherTests
    {
        private readonly PatternMatcher _matcher;

        public PatternMatcherTests()
        {
            _matcher = new PatternMatcher();
        }

        [Fact]
        public void GivenSingleKeyPattern_WhenSingleKeyInput_ThenMatch()
        {
            Pattern pattern = Stubs.Pattern(Stubs.Down("A"));

            Input[] inputs = new[] { Stubs.Down("A") };

            Match match = _matcher.Evaluate(inputs, pattern);

            Assert.Equal(Match.Full, match);
        }
    }
}
