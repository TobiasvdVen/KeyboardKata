using KeyboardKata.Domain.InputProcessing;

namespace KeyboardKata.Domain.InputMatching
{
    public class PatternMatcher
    {
        public Match Evaluate(IEnumerable<Input> inputs, Pattern pattern)
        {
            IEnumerator<Input> inputsEnumerator = inputs.GetEnumerator();
            IEnumerator<SubPattern> patterns = pattern.SubPatterns.GetEnumerator();

            inputsEnumerator.MoveNext();
            patterns.MoveNext();

            if (inputsEnumerator.Current.Key == patterns.Current.KeyPress)
            {
                return Match.Full;
            }

            return Match.None;
        }
    }
}
