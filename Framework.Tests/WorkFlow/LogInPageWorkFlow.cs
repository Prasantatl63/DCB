using DCB.Pages;
using Microsoft.Playwright;
using Serilog;

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
            Log.Information("Starting login for user: {Username}", user);
            await loginPage.LogInAsync(user, pass);
            Log.Information("Login Done : {Username}", user);

        }
    }
}