using KeyboardKata.Domain.InputProcessing;

namespace KeyboardKata.Domain.InputMatching
{
    public record ExactMatchPattern(IEnumerable<Input> Inputs) : IPattern
    {
        public Match Matches(IEnumerable<Input> inputs)
        {
            if (inputs.SequenceEqual(Inputs))
            {
                return Match.Full;
            }

            return Match.None;
        }
    }
}
