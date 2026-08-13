using DCB.Framework.Artifacts;
using DCB.Framework.Browser;
using DCB.Framework.Configuration;
using DCB.Framework.Logging;
using Microsoft.Playwright;
using NUnit.Framework;

namespace DCB.Framework.Base;

public abstract class BaseTest
{
    protected IPage Page { get; private set; } = null!;

    protected IBrowserContext Context { get; private set; } = null!;

    protected BrowserManager BrowserManager { get; private set; } = null!;

    protected ArtifactManager ArtifactManager { get; private set; } = null!;

    private string _artifactDirectory = string.Empty;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        TestLogger.Initialize();

        TestLogger.Info(
            "Starting test fixture: {Fixture}",
            TestContext.CurrentContext.Test.ClassName);

        BrowserManager = new BrowserManager();

        var settings =
              ConfigurationManager.Settings;

        await BrowserManager.InitializeAsync(
            settings.TestSettings.Browser,
            settings.TestSettings.Headless);
    }

    [SetUp]
    public async Task SetUp()
    {
        var settings =
            ConfigurationManager.Settings;

        ArtifactManager = new ArtifactManager();

        _artifactDirectory = ArtifactManager.CreateTestDirectory();

        Context =
            await BrowserManager.CreateContextAsync(
                settings.TestSettings.BaseUrl);

        Page =
            await Context.NewPageAsync();

        await Context.Tracing.StartAsync(
            new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

        TestLogger.Info(
            "Test started: {TestName}",
            TestContext.CurrentContext.Test.Name);
    }

    [TearDown]
    public async Task TearDown()
    {
        var testStatus =
            TestContext.CurrentContext.Result.Outcome.Status;

        TestLogger.Info(
            "Test completed: {TestName} - {Status}",
            TestContext.CurrentContext.Test.Name,
            testStatus);

        if (testStatus == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            await ArtifactManager.CaptureFailureArtifactsAsync(
                Page,
                Context,
                _artifactDirectory);
        }

        if (Context != null)
            await Context.CloseAsync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        if (BrowserManager != null)
            await BrowserManager.DisposeAsync();

        TestLogger.Close();
    }
}