using KeyboardKata.Domain.Actions;
using KeyboardKata.Domain.InputMatching;
using KeyboardKata.Domain.InputProcessing;
using Microsoft.Extensions.Logging;

namespace KeyboardKata.Domain.Sessions
{
    public class SessionState : ISessionState, IInputProcessor
    {
        private readonly IKeyboardKata _kata;
        private readonly IKeyboardActionSource _keyboardActionProvider;
        private readonly ILogger<SessionState> _logger;

        public SessionState(IKeyboardKata kata, IKeyboardActionSource keyboardActionProvider, ILogger<SessionState> logger)
        {
            _kata = kata;
            _keyboardActionProvider = keyboardActionProvider;
            _logger = logger;
        }

        public KeyboardAction? CurrentAction { get; private set; }

        public void NextPrompt()
        {
            CurrentAction = _keyboardActionProvider.GetKeyboardAction();
            _kata.Prompt(CurrentAction);
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

            return InputContinuation.Safe;
        }
    }
}
