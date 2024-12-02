using NLog;
using NLog.Targets;
using PwProject.Core.Logger.Interfaces;

namespace PwProject.Core.Logger
{
    [Target("ReportPortal")]
    public class ReportPortalWebTarget : ReportPortalTarget
    {
        public ReportPortalWebTarget(IRpMessageSender rpMessageSender) : base(rpMessageSender)
        {
        }

        public override byte[] GetAttachment(LogEventInfo logEventInfo)
        {
            return null;
        }
    }
}
