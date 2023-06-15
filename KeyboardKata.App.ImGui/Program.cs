using ImGuiNET;
using KeyboardKata.App.ViewModels;
using KeyboardKata.Domain.Sessions;
using System.Diagnostics;
using System.Numerics;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;
using Gui = ImGuiNET.ImGui;

namespace KeyboardKata.App.ImGui
{
    internal class Program
    {
        static void Main(string[] _)
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

            using ProcessTrainerSession trainerSession = new(
                new ProcessStartInfo("F:\\Projects\\KeyboardKata\\KeyboardKata.Trainer.Wpf\\bin\\Debug\\net7.0-windows\\KeyboardKata.Trainer.Wpf.exe"));

            trainerSession.Ended += (r) =>
            {
                window.Visible = true;
            };

            MainViewModel mainViewModel = new(trainerSession);

            while (window.Exists)
            {
                InputSnapshot snapshot = window.PumpEvents();
                imGuiRenderer.Update(1.0f / 60, snapshot);
                Gui.BeginMainMenuBar();

                Gui.SetNextWindowPos(Vector2.Zero);
                Gui.SetNextWindowSize(new Vector2(800, 600));
                Gui.Begin(" ", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar);

                if (Gui.Button("Start"))
                {
                    if (!mainViewModel.TrainerActive)
                    {
                        mainViewModel.StartTrainer();
                        window.Visible = false;
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

            Gui.DestroyContext();
        }
    }
}
