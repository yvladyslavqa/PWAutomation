using PwProject.Core.Logger.Interfaces;
using ReportPortal.Shared.Execution.Logging;

namespace PwProject.Core.Logger
{
    public class ReportPortalMessageSender : IRpMessageSender
    {
        public void SendMessage(string text, LogMessageLevel level, DateTime time, byte[] attachment)
        {
            var attach = attachment is null ? null :
                new LogMessageAttachment("image/jpg", attachment);

            ReportPortal.Shared.Context.Current.Log.Message(new LogMessage(text)
            {
                Level = level,
                Time = time,
                Attachment = attach
            });
        }
    }
}
