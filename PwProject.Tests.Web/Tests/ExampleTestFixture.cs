using NUnit.Framework;
using PwProject.Core.Autofac;
using PwProject.Core.Configuration;
using PwProject.Core.Web;
using PwProject.Domain;
using PwProject.WebInteraction.ReportPortal.Pages;
namespace PwProject.Tests.Web.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class ExampleTestsFixture : BaseWebTestFixture
    {
        private LoginPage _loginPage;
        private EnvironmentConfiguration _environmentConfiguration;

        public ExampleTestsFixture()
        {
            _environmentConfiguration = DependencyResolver.Resolve<EnvironmentConfiguration>();
        }

        User createUser = new User()
        {
            Email = "default",
            Password = "1q2w3e",
        };

        [Test]
        public async Task Test_NavigateToReportPortal_Login_InccorectUser()
        {
            _loginPage = new LoginPage(_page);
            await _loginPage.GotoAsync(_environmentConfiguration.ReportPortal);

            await _loginPage.LoginAsync(createUser);

            await _loginPage.VerifyErrorMessage("User name may contain only Latin, numeric characters, hyphen, underscore, dot (from 1 to 128 symbols)");
        }
    }
}
