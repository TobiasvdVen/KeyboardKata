using KeyboardKata.Domain.Tests.Helpers;
using Optional;
using Optional.Unsafe;
using System.Collections.Generic;
using Xunit;

namespace KeyboardKata.Domain.Tests
{
    public class SequenceTests
    {
        [Fact]
        public void GivenSingleInput_WhenMatched_ThenMatchFull()
        {
            Sequence sequence = new(Stubs.Down("Z"));

            Option<Match> match = sequence.Matches(Stubs.Down("Z"));

            Assert.Equal(Match.Full, match.ValueOrFailure());
        }

        [Fact]
        public void GivenModifierPlusKeySequence_WhenMatched_ThenMatchFull()
        {
            IEnumerable<Input> inputs = Stubs.Inputs(Stubs.Down("CTRL"), Stubs.Down("F"));

            Sequence sequence = new(Stubs.Inputs(Stubs.Down("CTRL"), Stubs.Down("F")));

            Option<Match> match = sequence.Matches(inputs);

            Assert.Equal(Match.Full, match.ValueOrFailure());
        }

        [Fact]
        public void GivenSimpleSequence_WhenMatchedWithWrongStartingInput_ThenNoMatch()
        {
            IEnumerable<Input> inputs = Stubs.Inputs(Stubs.Down("A"), Stubs.Down("S"), Stubs.Down("D"));

            Sequence sequence = new(Stubs.Inputs(Stubs.Down("S"), Stubs.Down("D")));

            Option<Match> match = sequence.Matches(inputs);

            Assert.False(match.HasValue);
        }

        [Fact]
        public void GivenSimpleSequence_WhenMatchedWithCorrectPartialInput_ThenMatchPartial()
        {
            IEnumerable<Input> inputs = Stubs.Inputs(Stubs.Down("A"));

            Sequence sequence = new(Stubs.Down("A"), Stubs.Down("S"), Stubs.Down("D"));

            Option<Match> match = sequence.Matches(inputs);

            Assert.Equal(Match.Partial, match.ValueOrFailure());
        }
    }
}
