using KeyboardKata.Domain.InputMatching;

namespace KeyboardKata.Domain.Sessions.Configuration
{
    public class SessionConfiguration
    {
        public SessionConfiguration(IPattern quitPattern)
        {
            QuitPattern = quitPattern;
        }

        public IPattern QuitPattern { get; }
    }
}
