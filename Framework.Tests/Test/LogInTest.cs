using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using DCB.Framework.Base; 
using DCB.Framework.Utilities;
using DCB.Framework.Workflow;
using DCB.Pages;
using DCB.Framework.Configuration;

namespace DCB.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class LoginTests : BaseTest
    {
        private LoginPage _loginPage = null!;

        [SetUp]
        public void InitializePages()
        {
            _loginPage = new LoginPage(Page);
        }

        [Test]
        [AllureTag("smoke")]
        [AllureFeature("Login")]
        [AllureTag("smoke")]
        [AllureFeature("Login")]
        [AllureStory("Valid Login")]
        [AllureAfter]
        [AllureSeverity(SeverityLevel.critical)]

        public async Task VerifyLogin()
        {
            var settings =
           ConfigurationManager.Settings;

            await Page.GotoAsync
                (settings.TestSettings.BaseUrl);

            await new LogInPageWorkFlow(Page).Login
                (settings.TestSettings.UserName, 
                settings.TestSettings.Password);

            Assert.That(Page.Url, 
                Does.Contain("logged-in-successfully"));
        }
    }
}