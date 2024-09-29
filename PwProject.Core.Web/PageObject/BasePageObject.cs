using Microsoft.Playwright;
using NUnit.Framework;

namespace PwProject.Core.Web.PageObject
{
    public class BasePageObject
    {
        protected IPage Page;

        private IReporter _reporter;
        protected AnnotationHelper annotationHelper;

        public BasePageObject(IPage page)
        {
            this.Page = page;
            _reporter = new ConsoleReporter();
            annotationHelper = new AnnotationHelper(_reporter);
        }

        public async Task TakeScreenShootAsync(string name)
        {
            var screenImage = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, name + "-" + Guid.NewGuid().ToString() + ".png");
            var imageBytes = await Page.ScreenshotAsync(new PageScreenshotOptions { FullPage = true });
            File.WriteAllBytes(screenImage, imageBytes);
            TestContext.AddTestAttachment(screenImage);
        }

        public async Task GotoPageAsync(string page)
        {
            this.annotationHelper.AddAnnotation(AnnotationType.Step, "Go to the page: '" + page + "'");
            await Page.GotoAsync(page);
        }

        public List<Annotation> GetAnnotations()
        {
            return this.annotationHelper.GetAnnotations();
        }

        public void AddDescription(string description)
        {
            this.annotationHelper.AddAnnotation(AnnotationType.Description, description);
        }

        public void AddName(string description)
        {
            this.annotationHelper.AddAnnotation(AnnotationType.Name, description);
        }
    }
}
