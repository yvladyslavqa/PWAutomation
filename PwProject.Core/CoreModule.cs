using Autofac;
using FluentAssertions.Execution;
using PwProject.Core.Assertion;
using PwProject.Core.Logger;
using PwProject.Core.Logger.Interfaces;

namespace PwProject.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostSharpLogger>().As<ILogger>().SingleInstance();
            builder.RegisterType<SoftAssertStrategy>().As<IAssertionStrategy>();
        }
    }
}
