using Allure.Net.Commons;
//using PlaywrightFramework.Core;
//using PlaywrightFramework.Pages;
using Allure.NUnit;
using Allure.NUnit.Attributes;
//using DCBBank.Core;
//using DCBBank.Pages;
using Microsoft.Playwright;
using NUnit.Framework;
//using PlayWrightHelloWorld.Utilities;
using Microsoft.Playwright;
using DCB.Framework.Utilities;

using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using DCB.Framework.Core;
using DCB.Framework.Workflow;



namespace DCB.Tests


{
    [TestFixture]
    [AllureNUnit]
    public class LoginTests : BaseTest
    {
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
            await Page.GotoAsync(ConfigReader.BaseUrl);
            await new LogInPageWorkFlow(Page).Login("student","Password123");
            Assert.IsTrue(Page.Url.ToString().Contains("login"));
        }

        
    }
}