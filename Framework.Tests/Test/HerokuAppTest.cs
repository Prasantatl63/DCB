using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using DCB.Framework.Base;
using DCB.Framework.Configuration;
using DCB.Framework.Workflow;
using DCB.Pages;

namespace DCB.Tests
{
    [TestFixture]
    public class HerokuAppTest :BaseTest
    {
        private FrameworkSettings _settings = ConfigurationManager.Settings;
        private HerokuAppWorkFlow _herokuApp;

        [SetUp]
        public async Task InitialSetUp()
        {
            await Page.GotoAsync(_settings.TestSettings.HerokuAppUrl);
            _herokuApp = new HerokuAppWorkFlow(Page);
        }

        [Test]
        [Category("Shard1")]
        [AllureTag("smoke")]
        [AllureFeature("Login")]
        [AllureTag("smoke")]
        [AllureFeature("Login")]
        [AllureStory("Valid Login")]
        [AllureAfter]
        [AllureSeverity(SeverityLevel.critical)]

        public async Task VerifyCheckBoxCheck()
        {
            await _herokuApp.NavigateToCheckBoxes();
            await _herokuApp.CheckCheckBoxes();

            Assert.That(Page.Url,Does.Contain("check"));
        }
        [Test]
        public async Task VerifyDisAppearingElement()
        {
            await _herokuApp.NavigateAndClickGallery();
            Assert.That(Page.Url,Does.Contain("gallery"));
        }

        [Test]
        public async Task VerifyDragAndDrop()
        {
            await _herokuApp.DragAndDrop();
            Assert.That(Page.Url, Does.Contain("drag_and_drop"));
        }

    }
}
