using KeyboardKata.App.ViewModels;
using static ImGuiNET.ImGui;

namespace KeyboardKata.App.ImGui.Views
{
    public class MainView : ImGuiView
    {
        private readonly MainViewModel _viewModel;

        private readonly ShortcutCommandButton _startSession;
        private readonly ShortcutCommandButton _reset;

        public MainView(MainViewModel viewModel)
        {
            _viewModel = viewModel;

            _startSession = new ShortcutCommandButton(_viewModel.StartSessionCommand);
            _reset = new ShortcutCommandButton(_viewModel.ResetCommand);
        }

        public override void Render()
        {
            if (_viewModel.SessionResult is null)
            {
                _startSession.Button();
            }
            else
            {
                Text($"You made {_viewModel.SessionResult.Mistakes} mistakes!");

                _reset.Button();
            }
        }
    }
}
