using Microsoft.Playwright;
using PwProject.Core.Logger;

namespace PwProject.Core.Web.Component
{
    public class UiElement : BaseLocator
    {
        public UiElement(IPage page, string locator, AnnotationHelper annotationHelper) : base(page, locator, annotationHelper)
        {

        }

        public async Task ClickAsync()
        {
            this.AnnotationHelper.AddAnnotation(AnnotationType.Step, "Click in the button: '" + "'");
            await this.Locator.HighlightAsync();
            await this.Locator.ClickAsync();
        }
    }
}
