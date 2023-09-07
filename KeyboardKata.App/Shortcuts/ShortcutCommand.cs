using KeyboardKata.App.Commands;
using KeyboardKata.Domain.InputMatching;
using System;
using System.Threading.Tasks;

namespace KeyboardKata.App.Shortcuts
{
    public record ShortcutCommand : Command
    {
        public ShortcutCommand(string identifier, Func<Task> action, IPattern shortcut) : base(identifier, action)
        {
            Shortcut = shortcut;
        }

        public IPattern Shortcut { get; }
    }
}
