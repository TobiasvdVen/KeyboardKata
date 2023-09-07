using KeyboardKata.App.ImGui.Views;

namespace KeyboardKata.App.ImGui
{
    internal interface IImGuiViewHolder
    {
        ImGuiView? View { get; set; }
    }
}
