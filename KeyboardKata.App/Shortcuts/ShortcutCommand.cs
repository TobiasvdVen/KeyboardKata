using KeyboardKata.App.Commands;
using KeyboardKata.Domain.InputMatching;

namespace KeyboardKata.App.Shortcuts
{
    public record ShortcutCommand(Command Command, IPattern Shortcut)
    {
    }
}
