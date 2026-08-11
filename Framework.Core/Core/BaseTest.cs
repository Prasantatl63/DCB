using Microsoft.Playwright;
using NUnit.Framework;
using DCB.Framework.Utilities;
using Allure.Net.Commons;
using NUnit.Framework.Interfaces;
using Allure.NUnit;

namespace DCB.Framework.Core
{
    [Parallelizable(ParallelScope.All)]
    public abstract class BaseTest
    {
        // Shared at fixture/assembly level
        protected static IPlaywright Playwright { get; private set; } = null!;
        protected static IBrowser Browser { get; private set; } = null!;

        // Unique for every test
        protected IBrowserContext Context { get; private set; } = null!;
        protected IPage Page { get; private set; } = null!;

        private readonly List<string> _consoleMessages = new();

        private string _artifactDirectory = string.Empty;

        // ============================================================
        // ONE TIME SETUP
        // ============================================================

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            Playwright =
                await Microsoft.Playwright.Playwright.CreateAsync();

            Browser =
                await BrowserFactory.CreateAndLaunchBrowser(
                    Playwright,
                    ConfigReader.Browser,
                    ConfigReader.Headless);
        }

        // ============================================================
        // TEST SETUP
        // ============================================================

        [SetUp]
        public async Task SetUp()
        {
            // Create unique artifact folder for this test
            CreateArtifactDirectory();

            // Create isolated browser context
            Context = await Browser.NewContextAsync(
                new BrowserNewContextOptions
                {
                    BaseURL = ConfigReader.BaseUrl
                });

            // Create page
            Page = await Context.NewPageAsync();

            // Default timeout
            Page.SetDefaultTimeout(
                ConfigReader.DefaultTimeout);

            // Browser console capture
            Page.Console += (_, message) =>
            {
                _consoleMessages.Add(
                    $"[{DateTime.Now:HH:mm:ss}] " +
                    $"[{message.Type}] " +
                    $"{message.Text}");
            };

            // Start Playwright tracing
            await Context.Tracing.StartAsync(
                new TracingStartOptions
                {
                    Screenshots = true,
                    Snapshots = true,
                    Sources = true
                });
        }

        // ============================================================
        // TEST TEARDOWN
        // ============================================================

        [TearDown]
        public async Task TearDown()
        {
            var testStatus =
                TestContext.CurrentContext.Result.Outcome.Status;

            if (testStatus == TestStatus.Failed)
            {
                await CaptureFailureArtifacts();
            }
            else
            {
                // Stop tracing without saving it
                if (Context != null)
                {
                    await Context.Tracing.StopAsync();
                }
            }

            // Always close context
            if (Context != null)
            {
                await Context.CloseAsync();
            }
        }

        // ============================================================
        // ONE TIME TEARDOWN
        // ============================================================

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            if (Browser != null)
            {
                await Browser.CloseAsync();
            }

            Playwright?.Dispose();
        }

        // ============================================================
        // FAILURE ARTIFACTS
        // ============================================================

        private async Task CaptureFailureArtifacts()
        {
            try
            {
                await CaptureScreenshot();

                CapturePageUrl();

                CaptureConsoleLogs();

                await CaptureTrace();
            }
            catch (Exception ex)
            {
                TestContext.WriteLine(
                    $"Failed to capture failure artifacts: {ex}");
            }
        }

        // ============================================================
        // CREATE ARTIFACT DIRECTORY
        // ============================================================

        private void CreateArtifactDirectory()
        {
            var testName =
                TestContext.CurrentContext.Test.Name;

            var timestamp =
                DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");

            var safeTestName =
                SanitizeFileName(testName);

            _artifactDirectory =
                Path.Combine(
                    TestContext.CurrentContext.WorkDirectory,
                    "TestArtifacts",
                    safeTestName,
                    timestamp);

            Directory.CreateDirectory(
                _artifactDirectory);
        }

        // ============================================================
        // SCREENSHOT
        // ============================================================

        private async Task CaptureScreenshot()
        {
            if (Page == null)
                return;

            var screenshotPath =
                Path.Combine(
                    _artifactDirectory,
                    "failure-screenshot.png");

            await Page.ScreenshotAsync(
                new PageScreenshotOptions
                {
                    Path = screenshotPath,
                    FullPage = true
                });

            // NUnit attachment
            TestContext.AddTestAttachment(
                screenshotPath,
                "Failure Screenshot");

            // Allure attachment
            AllureApi.AddAttachment(
                "Failure Screenshot",
                "image/png",
                screenshotPath);
        }

        // ============================================================
        // CURRENT URL
        // ============================================================

        private void CapturePageUrl()
        {
            if (Page == null)
                return;

            var urlPath =
                Path.Combine(
                    _artifactDirectory,
                    "page-url.txt");

            File.WriteAllText(
                urlPath,
                Page.Url);

            TestContext.WriteLine(
                $"Failure URL: {Page.Url}");

            // Allure
            AllureApi.AddAttachment(
                "Failure URL",
                "text/plain",
                urlPath);
        }

        // ============================================================
        // BROWSER CONSOLE
        // ============================================================

        private void CaptureConsoleLogs()
        {
            if (_consoleMessages.Count == 0)
            {
                TestContext.WriteLine(
                    "No browser console messages captured.");

                return;
            }

            var consolePath =
                Path.Combine(
                    _artifactDirectory,
                    "browser-console.log");

            File.WriteAllLines(
                consolePath,
                _consoleMessages);

            TestContext.AddTestAttachment(
                consolePath,
                "Browser Console");

            AllureApi.AddAttachment(
                "Browser Console",
                "text/plain",
                consolePath);
        }

        // ============================================================
        // PLAYWRIGHT TRACE
        // ============================================================

        private async Task CaptureTrace()
        {
            if (Context == null)
                return;

            var tracePath =
                Path.Combine(
                    _artifactDirectory,
                    "playwright-trace.zip");

            await Context.Tracing.StopAsync(
                new TracingStopOptions
                {
                    Path = tracePath
                });

            TestContext.AddTestAttachment(
                tracePath,
                "Playwright Trace");

            AllureApi.AddAttachment(
                "Playwright Trace",
                "application/zip",
                tracePath);
        }

        // ============================================================
        // SANITIZE FILE NAME
        // ============================================================

        private static string SanitizeFileName(
            string fileName)
        {
            foreach (var invalidChar
                     in Path.GetInvalidFileNameChars())
            {
                fileName =
                    fileName.Replace(
                        invalidChar,
                        '_');
            }

            return fileName;
        }
    }
}