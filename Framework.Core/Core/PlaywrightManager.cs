using Microsoft.Playwright;

namespace DCB.Core;

public static class PlaywrightManager
{
    private static IPlaywright _playwright = null!;

    [ThreadStatic]
    private static IBrowser _browser = null!;

    [ThreadStatic]
    private static IBrowserContext _context = null!;

    [ThreadStatic]
    private static IPage _page = null! ;

    public static IPage Page => _page;

    public static async Task Initialize()
    {
        _playwright = await Playwright.CreateAsync();

        _browser = await _playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = false
            });

        _context = await _browser.NewContextAsync();

        _page = await _context.NewPageAsync();
    }

    public static async Task Close()
    {
        await _context.CloseAsync();
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}