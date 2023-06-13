using Microsoft.Extensions.Logging;

namespace KeyboardKata.Domain
{
    public class SessionState : ISessionState, IInputProcessor
    {
        private readonly IKeyboardKata _kata;
        private readonly IKeyboardActionProvider _keyboardActionProvider;
        private readonly ILogger<SessionState> _logger;

        public SessionState(IKeyboardKata kata, IKeyboardActionProvider keyboardActionProvider, ILogger<SessionState> logger)
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

            bool keyMatches = input.Key == CurrentAction.Pattern.SubPatterns.First().KeyPress;

            if (keyMatches && input.KeyPress == KeyPress.Down)
            {
                _kata.Success(CurrentAction);
            }
            else if (keyMatches && input.KeyPress == KeyPress.Up)
            {
                NextPrompt();
            }

            return InputContinuation.Safe;
        }
    }
}
