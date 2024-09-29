using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PwProject.Core;
using PwProject.Core.Autofac;
using PwProject.Core.Web;
using System.Runtime.CompilerServices;

namespace PwProject.Tests.Web
{
    public class Startup
    {
        private static IConfiguration Configuration { get; set; }

        private static BrowserDriverConfig _browserDriverConfig;
        public Startup() { }

        [ModuleInitializer]
        public static void Setup()
        {
            var startupFileLocation = Path.GetDirectoryName(typeof(Startup).Assembly.Location);

            var builder = new ConfigurationBuilder()
                .SetBasePath(startupFileLocation)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            ConfigureServices();
        }

        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<BrowserDriverConfig>();
            services.AddSingleton<PlaywrightDriver>();
            services.AddSingleton<IReporter, ConsoleReporter>(); // Your logger implementation here
            var builder = new ContainerBuilder();
            //builder.RegisterModule<CoreModule>();
            //builder.RegisterModule(new TestModule(Configuration));

            var container = builder.Build();

            DependencyResolver.SetResolver(container);

            return new AutofacServiceProvider(container);
        }
    }
}
