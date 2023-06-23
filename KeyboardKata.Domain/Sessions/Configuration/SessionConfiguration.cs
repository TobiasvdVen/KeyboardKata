using KeyboardKata.Domain.Actions.Pools;
using KeyboardKata.Domain.InputMatching;

namespace KeyboardKata.Domain.Sessions.Configuration
{
    public class SessionConfiguration
    {
        public SessionConfiguration(IPattern quitPattern, KeyboardActionPool actions)
        {
            QuitPattern = quitPattern;
            Actions = actions;
        }

        public IPattern QuitPattern { get; }
        public KeyboardActionPool Actions { get; }
    }
}
