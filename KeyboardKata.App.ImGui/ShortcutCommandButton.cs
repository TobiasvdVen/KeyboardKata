using KeyboardKata.App.Shortcuts;

namespace KeyboardKata.App.ImGui
{
    public class ShortcutCommandButton
    {
        private readonly ShortcutCommand _command;

        public ShortcutCommandButton(ShortcutCommand command)
        {
            _command = command;
        }

        public bool Button()
        {
            return Button(_command.ToString());
        }

        public bool Button(string overrideName)
        {
            bool clicked = ImGuiNET.ImGui.Button(overrideName);

            if (clicked)
            {
                _command.Execute();
            }

            return clicked;
        }
    }
}
