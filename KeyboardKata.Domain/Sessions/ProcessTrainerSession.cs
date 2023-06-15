using System.Diagnostics;

namespace KeyboardKata.Domain.Sessions
{
    public sealed class ProcessTrainerSession : ITrainerSession, IDisposable
    {
        private readonly ProcessStartInfo _startInfo;
        private Process? _process;

        public ProcessTrainerSession(ProcessStartInfo startInfo)
        {
            _startInfo = startInfo;
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
            _process.Exited += Process_Exited;
        }

        public void End()
        {
            if (_process is not null)
            {
                _process.Exited -= Process_Exited;
                _process = null;

                Ended?.Invoke(new SessionResult());
            }
        }

        public void Dispose()
        {
            _process?.Dispose();
        }

        private void Process_Exited(object? sender, EventArgs e)
        {
            End();
        }
    }
}
