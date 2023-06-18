using KeyboardKata.Domain.InputMatching;

namespace KeyboardKata.Domain.Sessions.Configuration
{
    public class SessionConfiguration
    {
        public SessionConfiguration(ExactMatchPattern quitPattern)
        {
            QuitPattern = quitPattern;
        }

        public ExactMatchPattern QuitPattern { get; }
    }
}
