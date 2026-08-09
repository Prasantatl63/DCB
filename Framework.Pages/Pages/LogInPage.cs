using Microsoft.Playwright;

namespace DCB.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
        }
        private ILocator UserName => _page.Locator("#username");

        private ILocator Password => _page.Locator("#password");

        private ILocator LoginButton => _page.Locator("#login");

        public async Task EnterLogIn(string user)
        {
            await UserName.FillAsync(user);
        }
        public async Task EnterPassword(string password)
        {
            await Password.FillAsync(password);
        }
        public async Task SubmitLogIn()
        {
            await LoginButton.ClickAsync();
        }
    }
}