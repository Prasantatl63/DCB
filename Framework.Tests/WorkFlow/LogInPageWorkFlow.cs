using DCB.Pages;
using Framework.Utilities.Logging;
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
            TestLogger.Info("Login started for {Username}", user);
            await loginPage.LogInAsync(user, pass);
            TestLogger.Info("Login Done : {Username}", user);

        }
    }
}