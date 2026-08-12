using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using DCB.Framework.Utilities;
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
            await new LogInPageWorkFlow(Page).Login(ConfigReader.UserName, ConfigReader.Password);
            Assert.That(Page.Url, 
                Does.Contain("logged-in-successfully"));
        }
    }
}