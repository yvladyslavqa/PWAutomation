using NUnit.Framework;
using PwProject.Core.Web;
using PwProject.Domain;
using PwProject.WebInteraction.ReportPortal.Pages;
namespace PwProject.Tests.Web
{
    [TestFixture]
    public class ExampleTestsFixture : BaseWebTestFixture
    {
        protected LoginPage _loginPage;

        User createUser = new User()
        {
            Email = "testUser@test.com",
            Password = "password",
        };

        [Test]
        public async Task Test_NavigateToReportPortal_Login_InccorectUser()
        {
            _loginPage = new LoginPage(_page);
            // Navigate to Google homepage
            await _loginPage.GotoAsync();

            await _loginPage.LoginAsync(createUser);

            await _loginPage.VerifyErrorMessage("User name may contain only Latin, numeric characters, hyphen, underscore, dot (from 1 to 128 symbols)");
        }
    }
}
