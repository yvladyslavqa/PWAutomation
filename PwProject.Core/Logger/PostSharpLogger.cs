using PostSharp.Patterns.Diagnostics;
using PwProject.Core.Logger.Interfaces;
using static PostSharp.Patterns.Diagnostics.FormattedMessageBuilder;

namespace PwProject.Core.Logger
{
    public class PostSharpLogger : ILogger
    {
        public void Debug(string message)
        {
            LogSource.Get().Debug.Write(Formatted(message));
        }

        public void Error(string message, Exception exception)
        {
            LogSource.Get().Error.Write(Formatted(message), exception);
        }

        public void Error(string message)
        {
            LogSource.Get().Error.Write(Formatted(message));
        }

        public void Info(string message)
        {
            LogSource.Get().Info.Write(Formatted(message));
        }

        public void Warning(string message)
        {
            LogSource.Get().Warning.Write(Formatted(message));
        }
    }
}
