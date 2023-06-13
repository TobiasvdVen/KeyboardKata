namespace KeyboardKata.Domain
{
    public delegate void TrainerSessionEnded(SessionResult result);

    public interface ITrainerSession
    {
        void Start();
        void End();

        event TrainerSessionEnded? Ended;

        bool Active { get; }
    }
}
