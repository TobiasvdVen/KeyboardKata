using KeyboardKata.App.Commands;
using KeyboardKata.Domain.InputMatching;
using System;
using System.Threading.Tasks;

namespace KeyboardKata.App.Shortcuts
{
    public record AsyncShortcutCommand : AsyncCommand
    {
        public AsyncShortcutCommand(string identifier, Func<Task> action, IPattern shortcut) : base(identifier, action)
        {
            Shortcut = shortcut;
        }

        public IPattern Shortcut { get; }
    }
}
