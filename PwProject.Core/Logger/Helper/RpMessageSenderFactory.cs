using PwProject.Core.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwProject.Core.Logger.Helper
{
    public class RpMessageSenderFactory
    {
        public static IRpMessageSender GetSender(bool localRun)
        {
            return localRun ? new ReportPortalMessageSender()
                : new NunitMessageSender();
        }
    }
}
