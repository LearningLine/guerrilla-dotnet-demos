namespace TheScheduler
{
    public interface ILogger
    {
        void Message(string message, params object[] args);
        void Message(string message);
    }
}