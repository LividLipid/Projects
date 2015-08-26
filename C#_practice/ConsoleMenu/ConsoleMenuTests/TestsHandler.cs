using ConsoleMenu;
using ConsoleMenuTests;
using NUnit.Framework;

namespace ConsoleMenuTests
{
    [TestFixture]
    public class TestsHandler
    {
        public static string TestFolderPath = Handler.DefaultFolderPath + @"\TestFiles";
        public static string TestName = "Test Tree";
        public static string TestFilePath = TestFolderPath + @"\" + TestName;

        public static HandlerMenu CreateDefaultHandler()
        {
            var tree = new ExampleTree().Root;
            var handler = new HandlerMenu(TestName, tree);
            handler._folderPath = TestFolderPath;

            return handler;
        }

        public static HandlerMenu CreateIsolatedHandler()
        {
            var tree = new ExampleTree().Root;
            var handler = new HandlerMenu(TestName, tree, new StubUserInterface(), new StubSaver());
            handler._folderPath = TestFolderPath;

            return handler;
        }

        [Test]
        [ExpectedException]
        public void SaveHandler_HasNoSaver_ThrowsException()
        {
            var handler = new HandlerMenu();
            handler._saver = null;
            handler.SaveHandler();
        }

        [Test]
        public void SaveHandler_HasSaver_IsSaved()
        {
            var handler = new HandlerMenu();
            var stubSaver = new StubSaver();
            handler._saver = stubSaver;
            handler.SaveHandler();

            Assert.True(stubSaver.HasSaved);
        }
    }
}