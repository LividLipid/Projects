using ConsoleMenuTDD;
using ConsoleMenuTests;
using NUnit.Framework;

namespace ConsoleMenuTests
{
    [TestFixture]
    public class TestsHandler
    {
        public static string TestFolderPath = @"C:\Projects\C#_practice\ConsoleMenu\SavedMenus\TestFiles";
        public static string TestName = "Test Tree";
        public static string TestFilePath = TestFolderPath + @"\" + TestName;

        public static HandlerMenu CreateDefault()
        {
            var tree = new ExampleTree().Root;
            var handler = new HandlerMenu(TestName, tree);
            handler._folderPath = TestFolderPath;

            return handler;
        }

        public static HandlerMenu CreateIsolated()
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
    }
}