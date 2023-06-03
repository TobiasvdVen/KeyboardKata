using KeyboardKata.Domain.Tests.Helpers;
using Optional;
using Optional.Unsafe;
using Xunit;

namespace KeyboardKata.Domain.Tests
{
    public class InputTests
    {
        [Fact]
        public void GivenSingleInput_WhenMatchedAgainstItself_ThenMatchFull()
        {
            Input input = Stubs.Down("A");

            Option<Match> match = input.Matches(input);

            Assert.Equal(Match.Full, match.ValueOrFailure());
        }
    }
}
