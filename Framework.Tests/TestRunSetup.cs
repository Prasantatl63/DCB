
using Framework.Utilities.Logging;

namespace DCB.Tests
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

