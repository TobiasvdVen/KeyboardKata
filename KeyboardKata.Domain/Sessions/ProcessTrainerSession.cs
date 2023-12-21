using System.Diagnostics;
using System.Text.Json;

namespace KeyboardKata.Domain.Sessions
{
    public sealed class ProcessTrainerSession : ITrainerSession, IDisposable
    {
        private readonly ProcessStartInfo _startInfo;
        private Process? _process;

        private SessionResult? _result;

        public ProcessTrainerSession(string trainerPath)
        {
            _startInfo = new ProcessStartInfo(trainerPath)
            {
                RedirectStandardOutput = true
            };
        }

        public event TrainerSessionEnded? Ended;

        public bool Active => _process is not null && !_process.HasExited;

        public void Start()
        {
            _process = Process.Start(_startInfo);

            if (_process is null)
            {
                throw new Exception("Trainer session process failed to start!");
            }

            _process.EnableRaisingEvents = true;
            _process.OutputDataReceived += OutputDataReceived;
            _process.Exited += Process_Exited;

            _process.BeginOutputReadLine();
        }

        public void End()
        {
            if (_process is not null)
            {
                _process.Exited -= Process_Exited;
                _process.Dispose();
                _process = null;

                Ended?.Invoke(_result);
            }
        }

        public void Dispose()
        {
            _process?.Dispose();
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data is string content)
            {
                JsonSerializerOptions options = new()
                {
                    AllowTrailingCommas = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                options.AddContext<SessionResultJsonContext>();

                try
                {
                    _result = JsonSerializer.Deserialize<SessionResult>(content, options);
                }
                catch (Exception) { }
            }
        }

        private void Process_Exited(object? sender, EventArgs e)
        {
            End();
        }
    }
}
