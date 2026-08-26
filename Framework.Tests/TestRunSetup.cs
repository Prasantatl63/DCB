
//using Framework.Utilities.Logging;

namespace DCB.Tests
{
    [SetUpFixture]
    public class TestRunSetup
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            //LoggerManager2.Initialize();
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
           // LoggerManager2.Close();
        }
    }
}

