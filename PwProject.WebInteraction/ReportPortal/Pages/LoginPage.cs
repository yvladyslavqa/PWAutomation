using FluentAssertions;
using Microsoft.Playwright;
using PwProject.Core.Logger;
using PwProject.Core.Web.Component;
using PwProject.Core.Web.PageObject;
using PwProject.Domain;

namespace PwProject.WebInteraction.ReportPortal.Pages
{
    public class LoginPage : BasePageObject
    {
        public TextElement LoginInput;
        public TextElement PasswordInput;
        public UiElement LoginButton;
        public TextElement ErrorHint;
        public LoginPage(IPage page) : base(page)
        {
            LoginInput = new TextElement(page, "//input[@name='login']", this.annotationHelper);
            PasswordInput = new TextElement(page, "//input[@name='password']", this.annotationHelper);
            LoginButton = new UiElement(page, "//button[@type='submit']", this.annotationHelper);
            ErrorHint = new TextElement(page, "//div[contains(@class,'fieldErrorHint')]//span", this.annotationHelper);

        }

        public async Task GotoAsync(string url, AnnotationType annotationType = AnnotationType.Step) => await this.GotoPageAsync(url);

        public async Task EnterLoginField(string login) => await this.LoginInput.FillAsync(login);
        public async Task EnterPasswordField(string password) => await this.PasswordInput.FillAsync(password);
        public async Task ClickLoginButton() => await this.LoginButton.ClickAsync();

        public List<string> GetTextElements() => this.ErrorHint.GetAllTextContent();


        public async Task LoginAsync(User user)
        {
            await this.LoginInput.FillAsync(user.Email);
            await this.PasswordInput.FillAsync(user.Password);
            await this.LoginButton.ClickAsync();
        }

        public async Task VerifyErrorMessage(string expectedResult)
        {
            var actualResult = GetTextElements();
            actualResult.Should().Contain(expectedResult);
        }
    }
}
