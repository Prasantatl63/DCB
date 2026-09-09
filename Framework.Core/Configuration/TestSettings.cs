namespace DCB.Framework.Configuration;

public class TestSettings
{
    public string Browser { get; set; } = "chromium";

    public bool Headless { get; set; } = true;

    public string BaseUrl { get; set; } = string.Empty;

    public string HerokuAppUrl { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
