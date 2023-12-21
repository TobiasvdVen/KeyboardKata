using System;

namespace KeyboardKata.App.Commands
{
    public record Command
    {
        private readonly Action _action;

        public Command(string identifier, Action action)
        {
            Identifier = identifier;
            _action = action;
        }

        public string Identifier { get; }

        public void Execute()
        {
            _action();
        }
    }
}
