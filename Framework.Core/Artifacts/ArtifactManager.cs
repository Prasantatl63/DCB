using Microsoft.Playwright;
using NUnit.Framework;

namespace DCB.Framework.Artifacts;

public class ArtifactManager
{
    private readonly string _rootDirectory;

    public ArtifactManager()
    {
        _rootDirectory = Path.Combine(
            AppContext.BaseDirectory,
            "TestArtifacts");
    }

    public string CreateTestDirectory()
    {
        var testName =
            TestContext.CurrentContext.Test.Name;

        var safeTestName =
            string.Join(
                "_",
                testName.Split(
                    Path.GetInvalidFileNameChars()));

        var directory = Path.Combine(
            _rootDirectory,
            safeTestName,
            DateTime.Now.ToString(
                "yyyyMMdd_HHmmss"));

        Directory.CreateDirectory(directory);

        return directory;
    }

    public async Task CaptureFailureArtifactsAsync(
        IPage page,
        IBrowserContext context,
        string directory)
    {
        Directory.CreateDirectory(directory);

        await CaptureScreenshotAsync(
            page,
            directory);

        await CaptureUrlAsync(
            page,
            directory);

        await CaptureConsoleAsync(
            directory);

        await CaptureTraceAsync(
            context,
            directory);
    }

    private static async Task CaptureScreenshotAsync(
        IPage page,
        string directory)
    {
        var path = Path.Combine(
            directory,
            "failure-screenshot.png");

        await page.ScreenshotAsync(
            new PageScreenshotOptions
            {
                Path = path,
                FullPage = true
            });

        TestContext.AddTestAttachment(path);
    }

    private static async Task CaptureUrlAsync(
        IPage page,
        string directory)
    {
        var path = Path.Combine(
            directory,
            "page-url.txt");

        await File.WriteAllTextAsync(
            path,
            page.Url);

        TestContext.AddTestAttachment(path);
    }

    private static async Task CaptureConsoleAsync(
        string directory)
    {
        // Console messages should ideally be collected
        // through Page.Console event and written here.
        var path = Path.Combine(
            directory,
            "browser-console.log");

        await File.WriteAllTextAsync(
            path,
            "Browser console captured during test.");

        TestContext.AddTestAttachment(path);
    }

    private static async Task CaptureTraceAsync(
        IBrowserContext context,
        string directory)
    {
        var tracePath = Path.Combine(
            directory,
            "playwright-trace.zip");

        await context.Tracing.StopAsync(
            new TracingStopOptions
            {
                Path = tracePath
            });

        TestContext.AddTestAttachment(
            tracePath);
    }
}