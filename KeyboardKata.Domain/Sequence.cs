using Optional;
using Optional.Collections;

namespace KeyboardKata.Domain
{
    public record Sequence(IEnumerable<IMatchable> Elements) : IMatchable
    {
        public Sequence(params IMatchable[] elements) : this((IEnumerable<IMatchable>)elements)
        {
        }

        public Option<Match> Matches(Input input)
        {
            return Elements.SingleOrNone().FlatMap(e => e.Matches(input));
        }

        public Option<Match> Matches(IEnumerable<Input> inputs)
        {
            using IEnumerator<IMatchable> enumeratorElements = Elements.GetEnumerator();
            using IEnumerator<Input> enumeratorInputs = inputs.GetEnumerator();

            int matchCount = 0;

            while (enumeratorElements.MoveNext() && enumeratorInputs.MoveNext())
            {
                Option<Match> match = enumeratorElements.Current.Matches(enumeratorInputs.Current);

                if (!match.HasValue)
                {
                    break;
                }

                ++matchCount;
            }

            if (matchCount == 0)
            {
                return Option.None<Match>();
            }

            if (matchCount == Elements.Count())
            {
                return Option.Some(Match.Full);
            }

            return Option.Some(Match.Partial);
        }
    }
}
