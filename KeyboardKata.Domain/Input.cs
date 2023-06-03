using Optional;

namespace KeyboardKata.Domain
{
    public record Input(Key Key, KeyPress KeyPress) : IMatchable
    {
        public Option<Match> Matches(Input input)
        {
            if (input.Equals(this))
            {
                return Option.Some(Match.Full);
            }

            return Option.None<Match>();
        }

        public Option<Match> Matches(IEnumerable<Input> inputs)
        {
            Input? input = inputs.SingleOrDefault();

            if (input is not null)
            {
                return Matches(input);
            }

            return Option.None<Match>();
        }
    }
}
