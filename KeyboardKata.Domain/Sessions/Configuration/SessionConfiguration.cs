using KeyboardKata.Domain.Actions.Sources;
using KeyboardKata.Domain.InputMatching;

namespace KeyboardKata.Domain.Sessions.Configuration
{
    public class SessionConfiguration
    {
        public SessionConfiguration(ExactMatchPattern quitPattern, LinearKeyboardActionSource actions)
        {
            QuitPattern = quitPattern;
            Actions = actions;
        }

        public ExactMatchPattern QuitPattern { get; }
        public LinearKeyboardActionSource Actions { get; }
    }
}
