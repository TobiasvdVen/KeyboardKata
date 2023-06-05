namespace KeyboardKata.Domain
{
    public record Key(int KeyCode)
    {
        public override string ToString()
        {
            return KeyCode.ToString();
        }

        public string ToString(IKeyCodeMapper mapper)
        {
            return mapper.Descriptor(KeyCode);
        }
    }
}