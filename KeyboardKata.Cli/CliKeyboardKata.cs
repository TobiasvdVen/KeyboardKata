using KeyboardKata.Domain;

namespace KeyboardKata.Cli
{
    internal class CliKeyboardKata : IKeyboardKata
    {
        public void Failure(KeyboardAction action, IEnumerable<Key> actual)
        {
            throw new NotImplementedException();
        }

        public void Progress(KeyboardAction action, IEnumerable<Key> remaining)
        {
            throw new NotImplementedException();
        }

        public void Prompt(KeyboardAction action)
        {
            Console.WriteLine(action.Prompt);
        }

        public void Success(KeyboardAction action)
        {
            throw new NotImplementedException();
        }
    }
}
