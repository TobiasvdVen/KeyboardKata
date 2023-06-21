using ImGuiNET;
using KeyboardKata.App.ViewModels;
using System.Numerics;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;
using Gui = ImGuiNET.ImGui;

namespace KeyboardKata.App.ImGui
{
    public class ImGuiService
    {
        private readonly MainViewModel _mainViewModel;

        public ImGuiService(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
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

            Sdl2Window window = VeldridStartup.CreateWindow(windowInfo);
            using GraphicsDevice graphicsDevice = VeldridStartup.CreateGraphicsDevice(window);

            using CommandList commands = graphicsDevice.ResourceFactory.CreateCommandList();

            using ImGuiRenderer imGuiRenderer = new(
                graphicsDevice,
                graphicsDevice.MainSwapchain.Framebuffer.OutputDescription,
                window.Width,
                window.Height);

            window.Resized += () =>
            {
                imGuiRenderer.WindowResized(window.Width, window.Height);
                graphicsDevice.MainSwapchain.Resize((uint)window.Width, (uint)window.Height);
            };

            while (window.Exists)
            {
                if (_mainViewModel.TrainerActive)
                {
                    window.Visible = false;
                }
                else
                {
                    window.Visible = true;

                    InputSnapshot snapshot = window.PumpEvents();
                    imGuiRenderer.Update(1.0f / 60, snapshot);
                    Gui.BeginMainMenuBar();

                    Gui.SetNextWindowPos(Vector2.Zero);
                    Gui.SetNextWindowSize(new Vector2(800, 600));
                    Gui.Begin(" ", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar);

                    if (_mainViewModel.SessionResult is null)
                    {
                        if (Gui.Button("Start"))
                        {
                            _mainViewModel.StartTrainer();
                        }
                    }
                    else
                    {
                        Gui.Text($"You made {_mainViewModel.SessionResult.Mistakes} mistakes!");

                        if (Gui.Button("Ok"))
                        {
                            _mainViewModel.Reset();
                        }
                    }

                    Gui.End();

                    Gui.EndMainMenuBar();

                    commands.Begin();
                    commands.SetFramebuffer(graphicsDevice.MainSwapchain.Framebuffer);
                    commands.ClearColorTarget(0, new RgbaFloat(0, 0, 0.2f, 1.0f));

                    imGuiRenderer.Render(graphicsDevice, commands);

                    commands.End();

                    graphicsDevice.SubmitCommands(commands);
                    graphicsDevice.SwapBuffers();
                }
            }

            Gui.DestroyContext();
        }
    }
}
