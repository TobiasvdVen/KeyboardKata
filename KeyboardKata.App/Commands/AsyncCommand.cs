using System;
using System.Threading.Tasks;

namespace KeyboardKata.App.Commands
{
    public record AsyncCommand
    {
        private readonly Func<Task> _action;

        public AsyncCommand(string identifier, Func<Task> action)
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
