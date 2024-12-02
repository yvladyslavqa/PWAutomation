using FluentAssertions.Execution;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using PwProject.Core.Logger.Interfaces;
using System.Diagnostics;
namespace PwProject.Core.Web
{
    public class BaseWebTestFixture
    {
        private readonly ILogger _logger;
        private AssertionScope _assertionScope;
        private readonly List<Exception> _exceptionList;
        protected IPage _page;
        private IBrowserContext _context;

        [SetUp]
        public async Task SetUp()
        {
            PlaywrightDriver playwrightDriver = new PlaywrightDriver();
            _page = await playwrightDriver.InitalizePlaywrightTracingAsync();
            _context = playwrightDriver.Context;
        }

        [TearDown]
        public async Task TearDownAsync()
        {
            byte[] screenshot = null;

            try
            {
                var testStatus = TestContext.CurrentContext.Result.Outcome.Status;

                if (testStatus == TestStatus.Failed)
                {
                    _logger.Error("Test failed with an error");
                }
                try
                {
                    string tracePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "trace.zip");
                    // Stop tracing and export it into a zip archive.
                    await _context.Tracing.StopAsync(new TracingStopOptions
                    {
                        Path = tracePath
                    });
                    TestContext.AddTestAttachment(tracePath);
                }
                catch
                {
                    _logger.Error("There are no screenshots available");
                }
                _assertionScope.Dispose();
            }
            catch (Exception ex)
            {
                _exceptionList.Add(ex);
                Assert.Fail(ex.Message);
            }
        }
    }
}
