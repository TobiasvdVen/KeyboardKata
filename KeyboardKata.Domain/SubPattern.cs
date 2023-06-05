namespace KeyboardKata.Domain
{
    public record SubPattern(Key KeyPress, IEnumerable<Key> Modifiers)
    {
    }
}
