using KeyboardKata.Domain.Actions.Pools;
using KeyboardKata.Domain.Actions.Sources;

namespace KeyboardKata.Domain.Actions
{
    public class KeyboardActionSourceFactory
    {
        public IKeyboardActionSource Create(KeyboardActionPool pool)
        {
            return pool switch
            {
                SingleActionPool singleActionPool => new SingleKeyboardActionSource(singleActionPool.Action),
                LinearActionPool linearActionPool => new LinearKeyboardActionSource(linearActionPool.Actions.Select(Create)),
                _ => throw new ArgumentException($"Unknown keyboard action pool of type {pool.GetType()}.")
            };
        }
    }
}
