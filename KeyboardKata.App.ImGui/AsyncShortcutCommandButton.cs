using KeyboardKata.App.Shortcuts;

namespace KeyboardKata.App.ImGui
{
    public class AsyncShortcutCommandButton
    {
        private readonly AsyncShortcutCommand _command;

        private readonly SemaphoreSlim _semaphore;

        private Task? _task;

        public AsyncShortcutCommandButton(AsyncShortcutCommand command)
        {
            _command = command;

            _semaphore = new SemaphoreSlim(1);
        }

        public bool Button()
        {
            return Button(_command.ToString());
        }

        public bool Button(string overrideName)
        {
            _semaphore.Wait();

            try
            {
                if (_task is not null)
                {
                    ImGuiNET.ImGui.Button("...");
                    return false;
                }

                bool clicked = ImGuiNET.ImGui.Button(overrideName);

                if (clicked)
                {
                    _task = _command.Execute();
                }

                return clicked;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
