namespace PwProject.Core.Web
{
    public class BrowserDriverConfig
    {
        public string BrowserType { get; set; } = "Chromium"; // Default to Chromium
        public bool Headless { get; set; } = false; // Default to headless mode
    }
}
