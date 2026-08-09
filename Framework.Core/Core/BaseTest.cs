//using Framework.Core;
using Microsoft.Playwright;
using NUnit.Framework;
//using PlayWrightHelloWorld.Utilities;
using Serilog;
using Serilog.Context;
using DCB.Framework.Utilities;

namespace DCB.Framework.Core
{
    public class BaseTest
    {
        protected IPlaywright Playwright;
        protected IBrowser Browser;
        protected IBrowserContext Context;
        protected IPage Page;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            LoggerManager.ConfigureLogger();
            Log.Information("Test execution started");
        }

        [SetUp]
        public async Task Setup()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            Browser = await BrowserFactory.CreateAndLaunchBrowser(
                Playwright,
                ConfigReader.Browser,
                ConfigReader.Headless);

            Context = await Browser.NewContextAsync();
            Page = await Context.NewPageAsync();


            //Logging the details
            var BrowserName = Browser.BrowserType?.Name ?? "Unknown";
            var BrowserVersion = Browser.Version?? "Unknown";

            using (LogContext.PushProperty("BrowserName", BrowserName))
            using (LogContext.PushProperty("BrowserVersion", BrowserVersion))
            {
                Log.Information(
                    "Starting test: {TestName} (Browser={BrowserName}, Version={BrowserVersion})",
                    TestContext.CurrentContext.Test.Name);
            }
        }

        [TearDown]
        public async Task TearDown()
        {
            Log.Information(
                "Finished test: {TestName}",
                TestContext.CurrentContext.Test.Name);

            await Context.CloseAsync();
            await Browser.CloseAsync();
            Playwright.Dispose();
        }

        [OneTimeTearDown]
        public void ExecutionEnd()
        {
            Log.Information("Test execution Completed");
        }
    }
}