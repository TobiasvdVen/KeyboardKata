using System;
using System.Threading.Tasks;

namespace KeyboardKata.App.Commands
{
    public record Command
    {
        private readonly Func<Task> _action;

        public Command(string identifier, Func<Task> action)
        {
            Identifier = identifier;
            _action = action;
        }

        public string Identifier { get; }

        public Task Execute()
        {
            return _action();
        }
    }
}
