using Autofac;
using Microsoft.Extensions.Configuration;
using PwProject.WebInteraction.ReportPortal.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = Autofac.Module;

namespace PwProject.Tests.Web.Infrastructure
{
    public class TestsModule : Module
    {
        private readonly IConfiguration _configuration;
        private readonly AppConfiguration _appConfiguration;

        public TestsModule(IConfiguration configuration, AppConfiguration appConfiguration)
        {
            _configuration = configuration;
            _appConfiguration = appConfiguration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var webInteractionsTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Single(assembly => assembly.GetName().Name == "PwProject.WebInteraction")
                .GetTypes();

            builder.RegisterInstance(_appConfiguration.EnvironmentConfiguration);
            builder.Register(c => _configuration).As<IConfiguration>();

            builder.RegisterType<LoginPage>().AsSelf().PropertiesAutowired();

            webInteractionsTypes
                .Where(t => t.Namespace?.StartsWith("PwProject.WebInteraction") == true
                && (t.Namespace.Contains("Steps") || t.Namespace.Contains("Pages")))
                .ToList().ForEach(t => builder.RegisterType(t).AsSelf().PropertiesAutowired());
        }
    }
}
