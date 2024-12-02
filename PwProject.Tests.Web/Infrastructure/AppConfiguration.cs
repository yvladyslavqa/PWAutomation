using PwProject.Core.Configuration;
using PwProject.Core.Web;

namespace PwProject.Tests.Web.Infrastructure
{
    public class AppConfiguration
    {
        public EnvironmentConfiguration EnvironmentConfiguration { get; set; } = new();

        public LaunchConfiguration LaunchConfiguration { get; set; } = new();

        public BrowserDriverConfig BrowserDriverConfig { get; set; } = new();
    }
}
