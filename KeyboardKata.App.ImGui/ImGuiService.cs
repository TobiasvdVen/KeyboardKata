using ImGuiNET;
using KeyboardKata.App.ImGui.Views;
using System.Numerics;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;
using static ImGuiNET.ImGui;

namespace KeyboardKata.App.ImGui
{
    public class ImGuiService : IImGuiViewHolder, IAppVisibility
    {
        private Sdl2Window? _window;

        public ImGuiView? View { get; set; }
        public bool Visible
        {
            get => _window?.Visible ?? false;
            set
            {
                if (_window is not null)
                {
                    _window.Visible = value;
                }
            }
        }

        public void Run()
        {
            WindowCreateInfo windowInfo = new()
            {
                X = 100,
                Y = 100,
                WindowWidth = 800,
                WindowHeight = 600,
                WindowTitle = "Keyboard Kata"
            };

            _window = VeldridStartup.CreateWindow(windowInfo);
            using GraphicsDevice graphicsDevice = VeldridStartup.CreateGraphicsDevice(_window);

            using CommandList commands = graphicsDevice.ResourceFactory.CreateCommandList();

            using ImGuiRenderer imGuiRenderer = new(
                graphicsDevice,
                graphicsDevice.MainSwapchain.Framebuffer.OutputDescription,
                _window.Width,
                _window.Height);

            _window.Resized += () =>
            {
                imGuiRenderer.WindowResized(_window.Width, _window.Height);
                graphicsDevice.MainSwapchain.Resize((uint)_window.Width, (uint)_window.Height);
            };

            while (_window.Exists)
            {
                if (Visible)
                {
                    InputSnapshot snapshot = _window.PumpEvents();
                    imGuiRenderer.Update(1.0f / 60, snapshot);
                    BeginMainMenuBar();

                    SetNextWindowPos(Vector2.Zero);
                    SetNextWindowSize(new Vector2(800, 600));
                    Begin(" ", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar);

                    View?.Render();

                    End();

                    EndMainMenuBar();

                    commands.Begin();
                    commands.SetFramebuffer(graphicsDevice.MainSwapchain.Framebuffer);
                    commands.ClearColorTarget(0, new RgbaFloat(0, 0, 0.2f, 1.0f));

                    imGuiRenderer.Render(graphicsDevice, commands);

                    commands.End();

                    graphicsDevice.SubmitCommands(commands);
                    graphicsDevice.SwapBuffers();
                }
            }

            DestroyContext();
        }
    }
}
