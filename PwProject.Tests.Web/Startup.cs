using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;
using PwProject.Core;
using PwProject.Core.Autofac;
using PwProject.Core.Logger;
using PwProject.Core.Logger.Helper;
using PwProject.Core.Logger.Interfaces;
using PwProject.Core.Web;
using PwProject.Tests.Web.Infrastructure;
using System.Runtime.CompilerServices;
[assembly: Log(
    AttributeTargetMemberAttributes = MulticastAttributes.Protected | MulticastAttributes.Internal | MulticastAttributes.Public,
    AttributeTargetTypes = "OCM.Tests.Web.*")]
namespace PwProject.Tests.Web
{
    public class Startup
    {
        private static IConfiguration Configuration { get; set; }
        private static AppConfiguration? _appConfiguration;

        public Startup() { }

        [ModuleInitializer]
        public static void Setup()
        {
            var startupFileLocation = Path.GetDirectoryName(typeof(Startup).Assembly.Location);

            var builder = new ConfigurationBuilder()
                .SetBasePath(startupFileLocation)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("environment.qa.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            ConfigureServices();

            EnableLogging();
        }

        private static void EnableLogging()
        {
            var localRun = _appConfiguration.LaunchConfiguration.LocalRun;
            IRpMessageSender messageSender = RpMessageSenderFactory.GetSender(localRun);

            var target = new ReportPortalWebTarget(messageSender)
            {
                Name = "ReportPortalTarget",
                Layout = "${message}"
            };

            var postSharpService = new PostSharpService(target);

            postSharpService.EnableLogging();
        }

        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddOptions();

            _appConfiguration = new AppConfiguration();

            Configuration.Bind("Environment", _appConfiguration.EnvironmentConfiguration);
            Configuration.Bind("Launch", _appConfiguration.LaunchConfiguration);

            services.AddSingleton(_appConfiguration);
            services.AddSingleton(_appConfiguration.EnvironmentConfiguration);
            services.AddSingleton(_appConfiguration.LaunchConfiguration);
            services.AddSingleton<BrowserDriverConfig>();
            services.AddSingleton<PlaywrightDriver>();
            services.AddSingleton<IReporter, ConsoleReporter>();
            var builder = new ContainerBuilder();
            builder.RegisterModule<CoreModule>();
            builder.RegisterModule(new TestsModule(Configuration!, _appConfiguration));

            var container = builder.Build();

            DependencyResolver.SetResolver(container);

            return new AutofacServiceProvider(container);
        }
    }
}
