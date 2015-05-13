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
        private LogAnalyzer analyzer = null;
        
        // Using setup may harm readability!
        [SetUp]
        public void Setup()
        {
            analyzer = MakeAnalyzer();
        }

        [Test]
        public void IsValidFileName_BadExtension_ReturnsFalse()
        {
            bool result = analyzer.IsValidLogFileName("filewithbadextension.foo");

            Assert.False(result);
        }

        [TestCase("filewithgoodextension.slf")]
        [TestCase("filewithgoodextension.SLF")]
        public void IsValidLogFileName_ValidExtensions_ReturnsTrue(string file)
        {
            bool result = analyzer.IsValidLogFileName(file);

            Assert.True(result);
        }

        [TestCase("badfile.foo", false)]
        [TestCase("goodfile.slf", true)]
        public void IsValidFileName_WhenCalled_ChangesWasLastFileNameValid(string file, bool expected)
        {
            analyzer.IsValidLogFileName(file);

            Assert.AreEqual(expected, analyzer.WasLastFileNameValid);
        }

        // Requiring TearDown is a sign of integration testing!
        // TearDown is rarely used in reality.
        [TearDown]
        public void TearDown()
        {
            analyzer = null; // This is an antipattern! It is not necessary.
        }

        // Factory methods are a good idea!
        private static LogAnalyzer MakeAnalyzer()
        {
            return new LogAnalyzer();
        }
        
    }
}
