namespace DCB.Framework.Configuration;

public class FrameworkSettings
{
    public TestSettings TestSettings { get; set; } = new();

    public TimeoutSettings Timeouts { get; set; } = new();
}