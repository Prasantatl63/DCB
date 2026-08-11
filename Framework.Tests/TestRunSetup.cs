
using Framework.Utilities.Logging;
using NUnit.Framework;

namespace Framework.Tests
{
    [SetUpFixture]
    public class TestRunSetup
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            LoggerManager.Initialize();
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            LoggerManager.Close();
        }
    }
}

