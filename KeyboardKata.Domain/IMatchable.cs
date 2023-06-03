using Optional;

namespace KeyboardKata.Domain
{
    public interface IMatchable
    {
        Option<Match> Matches(Input input);
        Option<Match> Matches(IEnumerable<Input> inputs);
    }
}
