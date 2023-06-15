using KeyboardKata.Domain.InputProcessing;

namespace KeyboardKata.Domain.InputMatching
{
    public record ExactMatchPattern(IEnumerable<Input> Pattern) : IPattern
    {
        public Match Matches(IEnumerable<Input> inputs)
        {
            if (inputs.SequenceEqual(Pattern))
            {
                return Match.Full;
            }

            return Match.None;
        }
    }
}
