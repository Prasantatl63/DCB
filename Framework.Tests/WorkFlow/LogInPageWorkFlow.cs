using DCB.Pages;
using Framework.Utilities.Logging;
using Microsoft.Playwright;
using DCB.Framework.Configuration;

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

        public async Task Login(string user, string password)
        {
            var settings =
           ConfigurationManager.Settings;

            TestLogger2.Info("Login started for {Username}", user);
            await loginPage.LogInAsync(user, password);
            TestLogger2.Info("Login Done : {Username}", user);

        }
    }
}