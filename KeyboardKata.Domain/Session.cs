using Microsoft.Extensions.Logging;

namespace KeyboardKata.Domain
{
    public class Session : IKataSession, IInputProcessor
    {
        private readonly IKeyboardKata _kata;
        private readonly IKeyboardActionProvider _keyboardActionProvider;
        private readonly ILogger<Session> _logger;

        public Session(IKeyboardKata kata, IKeyboardActionProvider keyboardActionProvider, ILogger<Session> logger)
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

            bool keyMatches = input.Key == CurrentAction?.Pattern.SubPatterns.First().KeyPress;

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
