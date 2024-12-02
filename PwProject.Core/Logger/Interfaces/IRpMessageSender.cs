using ReportPortal.Shared.Execution.Logging;
using System;

namespace PwProject.Core.Logger.Interfaces
{
    public interface IRpMessageSender
    {
        void SendMessage(string text, LogMessageLevel level, DateTime time, byte[] attachment);
    }
}
