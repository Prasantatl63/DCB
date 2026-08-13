using Microsoft.Playwright;

namespace DCB.Framework.Browser;

public class BrowserManager : IAsyncDisposable
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;

    public IBrowser Browser =>
        _browser
        ?? throw new InvalidOperationException(
            "Browser has not been initialized.");

    public async Task InitializeAsync(
        string browserName,
        bool headless)
    {
        _playwright = await Playwright.CreateAsync();

        _browser = browserName.ToLowerInvariant() switch
        {
            "chromium" =>
                await _playwright.Chromium.LaunchAsync(
                    new BrowserTypeLaunchOptions
                    {
                        Headless = headless
                    }),

            "firefox" =>
                await _playwright.Firefox.LaunchAsync(
                    new BrowserTypeLaunchOptions
                    {
                        Headless = headless
                    }),

            "webkit" =>
                await _playwright.Webkit.LaunchAsync(
                    new BrowserTypeLaunchOptions
                    {
                        Headless = headless
                    }),

            _ => throw new ArgumentException(
                $"Unsupported browser: {browserName}")
        };
    }

    public async Task<IBrowserContext> CreateContextAsync(
        string? baseUrl = null)
    {
        if (_browser == null)
            throw new InvalidOperationException(
                "Browser is not initialized.");

        return await _browser.NewContextAsync(
            new BrowserNewContextOptions
            {
                BaseURL = baseUrl
            });
    }

    public async ValueTask DisposeAsync()
    {
        if (_browser != null)
            await _browser.CloseAsync();

        _playwright?.Dispose();
    }
}