namespace KeyboardKata.Domain
{
    public class PatternMatcher
    {
        public Match Evaluate(InputStream inputs, Pattern pattern)
        {
            IEnumerator<SubPattern> patterns = pattern.SubPatterns.GetEnumerator();

            Input? next = null;

            do
            {
                next = inputs.Next();

                if (patterns.Current.KeyPress == next.Key)
                {
                    return Match.Full;
                }
                patterns.Current
            }
            while (next != null);

            throw new NotImplementedException();
        }
    }
}
