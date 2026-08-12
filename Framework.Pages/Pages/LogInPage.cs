using Microsoft.Playwright;
using Serilog;

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

        private ILocator LoginButton => _page.Locator("#submit");


        public async Task LogInAsync(string user,string password)
        {
            await UserName.FillAsync(user);
            Log.Debug("Username entered");
            await Password.FillAsync(password);
            await LoginButton.ClickAsync();
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