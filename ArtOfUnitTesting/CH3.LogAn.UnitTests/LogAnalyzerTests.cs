using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        // Injecting the fake in the test:
        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            FakeExtensionManager myFakeManager = new FakeExtensionManager();
            myFakeManager.WillBeValid = true;

            LogAnalyzer log = new LogAnalyzer(myFakeManager);

            bool result = log.IsValidLogFileName("short.ext");
            Assert.True(result);
        }
    }
    // The fake should be in the test class if it is only used here
    internal class FakeExtensionManager : IExtensionManager
    {
        public bool WillBeValid = false;

        public bool IsValid(string fileName)
        {
            return WillBeValid;
        }
    }
}
