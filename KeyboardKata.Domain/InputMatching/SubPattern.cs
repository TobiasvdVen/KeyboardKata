using KeyboardKata.Domain.InputProcessing;

namespace KeyboardKata.Domain.InputMatching
{
    public record SubPattern(Key KeyPress, IEnumerable<Key> Modifiers)
    {
    }
}
