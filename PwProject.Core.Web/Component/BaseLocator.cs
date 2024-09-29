using Microsoft.Playwright;

namespace PwProject.Core.Web.Component
{
    public class BaseLocator
    {
        protected ILocator Locator { get; set; }
        protected IPage Page { get; }
        protected AnnotationHelper AnnotationHelper { get; }

        public BaseLocator(IPage page, string locator, AnnotationHelper annotationHelper)
        {
            this.Locator = page.Locator(locator);
            this.Page = page;
            this.AnnotationHelper = annotationHelper;
        }

    }
}
