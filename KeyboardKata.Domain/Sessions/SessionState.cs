using KeyboardKata.Domain.Actions;
using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using Microsoft.Extensions.Logging;

namespace KeyboardKata.Domain.Sessions
{
    public class SessionState : ISessionState, IInputProcessor
    {
        private readonly IKeyboardKata _kata;
        private readonly IKeyboardActionSource _actionSource;
        private readonly ILogger<SessionState> _logger;

        public SessionState(IKeyboardKata kata, IKeyboardActionSource actionSource, ILogger<SessionState> logger)
        {
            _kata = kata;
            _actionSource = actionSource;
            _logger = logger;

            Mistakes = 0;
        }

        public KeyboardAction? CurrentAction { get; private set; }

        public int Mistakes { get; private set; }

        public void NextPrompt()
        {
            CurrentAction = _actionSource.GetKeyboardAction();

            if (CurrentAction is not null)
            {
                _kata.Prompt(CurrentAction);
            }
            else
            {
                _kata.Finish(new SessionResult(Mistakes));
            }
        }

        public InputContinuation Process(Input input)
        {
            if (CurrentAction == null)
            {
                _logger.LogWarning($"No current action!");

                return InputContinuation.Safe;
            }

            _logger.LogDebug($"Processing {input}");

            bool keyMatches = CurrentAction.Pattern.Matches(new List<Input>() { input }) == Match.Full;

            if (keyMatches)
            {
                _kata.Success(CurrentAction);
            }
            else if (input.KeyPress == KeyPress.Up)
            {
                NextPrompt();
            }
            else
            {
                ++Mistakes;
            }

            return InputContinuation.Safe;
        }
    }
}
