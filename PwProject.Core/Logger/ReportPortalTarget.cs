using NLog;
using NLog.Targets;
using PwProject.Core.Logger.Interfaces;
using ReportPortal.Shared.Execution.Logging;
using System.Text.RegularExpressions;

namespace PwProject.Core.Logger
{
    [Target("ReportPortal")]
    public abstract class ReportPortalTarget : TargetWithLayout
    {
        private static Regex _spacesAtStartPattern = new(@"^\s*");

        protected Dictionary<LogLevel, LogMessageLevel> LevelMap = new Dictionary<LogLevel, LogMessageLevel>();

        private readonly IRpMessageSender _rpMessageSender;

        public ReportPortalTarget(IRpMessageSender rpMessageSender)
        {
            LevelMap[LogLevel.Fatal] = LogMessageLevel.Fatal;
            LevelMap[LogLevel.Error] = LogMessageLevel.Error;
            LevelMap[LogLevel.Warn] = LogMessageLevel.Warning;
            LevelMap[LogLevel.Info] = LogMessageLevel.Info;
            LevelMap[LogLevel.Debug] = LogMessageLevel.Debug;
            LevelMap[LogLevel.Trace] = LogMessageLevel.Trace;

            _rpMessageSender = rpMessageSender;
        }

        protected override void Write(LogEventInfo logEvent)
        {
            var formattedMessage = GetFormattedMessage(logEvent);

            if (!string.IsNullOrEmpty(formattedMessage))
            {
                byte[] attachment = GetAttachment(logEvent);

                _rpMessageSender.SendMessage(
                    text: formattedMessage,
                    level: LevelMap[logEvent.Level],
                    time: logEvent.TimeStamp.ToUniversalTime(),
                    attachment: attachment);
            }
        }

        private string GetFormattedMessage(LogEventInfo logEvent)
        {
            var logMessage = RenderLogEvent(Layout, logEvent);

            if (logMessage.EndsWith("|Succeeded."))
            {
                return null;
            }

            logMessage = logMessage.Replace("|Starting.", string.Empty);

            // PostSharp adds spaces at the beginning, indicating method calls nesting.
            // If spaces count >= 4, report portal by unknown reason marks log message with a green color.
            // To avoid it, spaces should be replaced.
            var match = _spacesAtStartPattern.Match(logMessage)?.Value;

            return match == null ? logMessage :
                    _spacesAtStartPattern.Replace(logMessage, new string('-', match.Length));
        }

        public abstract byte[] GetAttachment(LogEventInfo logEventInfo);
    }
}
