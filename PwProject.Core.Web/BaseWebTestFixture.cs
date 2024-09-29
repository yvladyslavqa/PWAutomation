using Microsoft.Playwright;
using NUnit.Framework;
namespace PwProject.Core.Web
{
    public class BaseWebTestFixture
    {
        //private readonly ILogger _logger;
        //private AssertionScope _assertionScope;
        //private readonly List<Exception> _exceptionList;
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
            string tracePath = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "trace.zip");
            // Stop tracing and export it into a zip archive.
            await _context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = tracePath
            });
            TestContext.AddTestAttachment(tracePath);
        }
    }
}
