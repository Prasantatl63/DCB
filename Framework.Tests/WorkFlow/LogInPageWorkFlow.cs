using Microsoft.Playwright;
using DCB.Pages;

namespace DCB.Framework.Workflow
{
    public class LogInPageWorkFlow
    {
        private readonly IPage _page;
        private  LoginPage loginPage;

        public LogInPageWorkFlow(IPage _page)
        {
            loginPage = new LoginPage(_page);
        }

        public async Task Login(string user, string pass)
        {
            await loginPage.EnterLogIn(user);
            await loginPage.EnterPassword(user);
            await loginPage.SubmitLogIn();
        }
    }
}