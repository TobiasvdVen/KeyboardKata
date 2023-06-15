using KeyboardKata.Domain.InputProcessing;

namespace KeyboardKata.Domain.InputMatching
{
    public interface IPattern
    {
        Match Matches(IEnumerable<Input> inputs);
    }
}
