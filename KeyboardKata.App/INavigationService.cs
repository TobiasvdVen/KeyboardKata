namespace KeyboardKata.App
{
    public interface INavigationService<T>
    {
        T Current { get; }

        void GoTo(T destination);
        void GoBack();
    }
}
