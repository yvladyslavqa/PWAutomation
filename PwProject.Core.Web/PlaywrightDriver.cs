using Microsoft.Playwright;
using PwProject.Core.Web.Component;
using System.Collections;

namespace PwProject.Core.Web
{
    public class PlaywrightDriver
    {
        public IPage Page { get; set; }
        public IBrowserContext Context { get; set; }

        public async Task<IPage> InitalizePlaywright()
        {
            var browser = await InitBrowserAsync();
            Context = await browser.NewContextAsync();
            Page = await Context.NewPageAsync();
            return Page;
        }

        private async Task<IBrowser> InitBrowserAsync()
        {
            var playwright = await Playwright.CreateAsync();

            var browserConfig = new BrowserDriverConfig()
            {
                BrowserType = "Chromium",
                Headless = false,
            };
            BrowserTypeLaunchOptions launchOptions = new BrowserTypeLaunchOptions { Headless = browserConfig.Headless};
            return browserConfig.BrowserType switch
            {
                "Chromium" => await playwright.Chromium.LaunchAsync(launchOptions),
                "Firefox" => await playwright.Firefox.LaunchAsync(launchOptions),
                "Webkit" => await playwright.Webkit.LaunchAsync(launchOptions),
                _ => await playwright.Chromium.LaunchAsync(launchOptions)
            };
        }

        public async Task<IPage> InitalizePlaywrightTracingAsync()
        {
            var browser = await InitBrowserAsync();
            Context = await browser.NewContextAsync();
            // Sample for tracing
            await Context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true
            });
            Page = await Context.NewPageAsync();
            return Page;
        }
    }
}
