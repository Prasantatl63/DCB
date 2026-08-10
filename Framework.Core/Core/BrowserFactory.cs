using Microsoft.Playwright;
using DCB.Framework.Utilities;

//namespace Framework.Core;

namespace DCB.Framework.Core
{
public class BrowserFactory
{
    public static async Task<IBrowser> CreateAndLaunchBrowser(IPlaywright playwright,string browserName,bool headless)
    {
        return browserName.ToLower() switch
        {
            "firefox" =>
                await playwright.Firefox.LaunchAsync(
                    new BrowserTypeLaunchOptions
                    {
                        Headless = ConfigReader.Headless
                    }),

            "webkit" =>
                await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = ConfigReader.Headless
                }),

            _ =>
                await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = ConfigReader.Headless
                }),
        };
    }
}
}