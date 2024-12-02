using NLog.Config;
using NLog;
using PostSharp.Patterns.Diagnostics.Backends.NLog;
using PostSharp.Patterns.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwProject.Core.Logger
{
    public class PostSharpService
    {
        private readonly ReportPortalTarget _reportPortalTarget;

        public PostSharpService(ReportPortalTarget reportPortalTarget)
        {
            _reportPortalTarget = reportPortalTarget;
        }

        public void EnableLogging()
        {
            InitializeNLoggingBackend();
            LogManager.EnableLogging();
        }

        private void InitializeNLoggingBackend()
        {
            LoggingServices.DefaultBackend = new NLogLoggingBackend(
                new LogFactory(GetNLogConfig()));
        }

        private LoggingConfiguration GetNLogConfig()
        {
            var nlogConfig = new LoggingConfiguration();
            nlogConfig.AddTarget(_reportPortalTarget);
            nlogConfig.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Trace, _reportPortalTarget));

            return nlogConfig;
        }
    }
}
