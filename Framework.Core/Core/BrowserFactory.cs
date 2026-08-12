using Microsoft.Playwright;

namespace DCB.Framework.Core
{
public class BrowserFactory
{
    public static async Task<IBrowser> CreateAndLaunchBrowser(IPlaywright playwright,string browserName,bool headless)
    {
            var options = new BrowserTypeLaunchOptions
            {
                Headless = headless
            };

            return browserName.ToLower() switch
        {
            "chromium" => await playwright.Chromium.LaunchAsync(options),
            "firefox" => await playwright.Firefox.LaunchAsync(options),
            "webkit" => await playwright.Webkit.LaunchAsync(options),
           
        };
    }
}
}