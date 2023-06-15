namespace KeyboardKata.Domain.InputProcessing
{
    public interface IKeyCodeMapper
    {
        string Descriptor(int keyCode);
        string Descriptor(Key key);
        Key Key(int keyCode);
        Key Key(string descriptor);
    }
}
