using NUnit.Framework;

namespace PwProject.Tests.Web
{
    [SetUpFixture]
    public class BaseTestFixture
    {
        public BaseTestFixture()
        {
        }

        [OneTimeSetUp]
        public async Task OneTimeSetUpAsync()
        {
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDownAsync()
        {
        }
    }
}
