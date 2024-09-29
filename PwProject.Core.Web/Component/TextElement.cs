using Microsoft.Playwright;

namespace PwProject.Core.Web.Component
{
    public class TextElement : BaseLocator
    {
        public TextElement(IPage page, string locator, AnnotationHelper annotationHelper) : base(page, locator, annotationHelper)
        {

        }

        /// <summary>
        /// Sends key down, key press, keyup for each character
        /// </summary>
        /// <param name="value">Value to fill</param>
        /// <remarks>Doesn't clear input</remarks>
        /// <returns></returns>
        public async Task TypeAsync(string value)
        {
            this.AnnotationHelper.AddAnnotation(AnnotationType.Step, "Type the value: '" + value + "' in the input: '" + value + "'");
            await this.Locator.HighlightAsync();
            await this.Locator.PressSequentiallyAsync(value);
        }

        /// <summary>
        /// Set input value like paste
        /// </summary>
        /// <param name="value">Value to fill</param>
        /// <remarks>Clear input value</remarks>
        /// <returns></returns>
        public async Task FillAsync(string value)
        {
            this.AnnotationHelper.AddAnnotation(AnnotationType.Step, "Fill the input: '" + "' with the value: '" + value + "'");
            await this.Locator.FillAsync(value);
        }

        /// <summary>
        /// Get the value
        /// </summary>
        /// <returns>Input value</returns>
        public List<string> GetAllTextContent()
        {
            this.AnnotationHelper.AddAnnotation(AnnotationType.Assert, "Try to get all values");
            this.Locator.HighlightAsync();
            return this.Locator.AllTextContentsAsync().Result.ToList();
        }
    }
}
