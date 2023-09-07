namespace KeyboardKata.Domain.Sessions
{
    public class SessionRunner
    {
        private readonly ITrainerSession _trainerSession;

        public SessionRunner(ITrainerSession trainerSession)
        {
            _trainerSession = trainerSession;
            _trainerSession.Ended += TrainerSession_Ended;
        }

        public bool SessionRunning => _trainerSession.Active;
        public SessionResult? SessionResult { get; private set; }

        public void StartTrainer()
        {
            _trainerSession.Start();
        }

        public void StopTrainer()
        {
            _trainerSession.End();
        }

        public void Reset()
        {
            SessionResult = null;
        }

        private void TrainerSession_Ended(SessionResult? result)
        {
            SessionResult = result;
        }
    }
}
