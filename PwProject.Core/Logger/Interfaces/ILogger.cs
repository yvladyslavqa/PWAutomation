namespace PwProject.Core.Logger.Interfaces
{
    public interface ILogger
    {
        void Debug(string message);

        void Info(string message);

        void Warning(string message);

        void Error(string message, Exception exception);

        void Error(string message);
    }
}
