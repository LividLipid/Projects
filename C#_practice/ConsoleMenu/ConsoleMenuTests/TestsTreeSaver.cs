using System.IO;
using ConsoleMenuTDD;
using NUnit.Framework;

namespace ConsoleMenuTests
{
    [TestFixture]
    public class TestsTreeSaver
    {

        private static string TestFolderPath = TestsHandler.TestFolderPath;
        private static string TestName = TestsHandler.TestName;
        private static string TestFilePath = TestsHandler.TestFilePath;

        [SetUp]
        public void Init()
        {
            ResetTestFolder();
        }

        [TearDown]
        public void Cleanup()
        {
            ResetTestFolder();
        }

        private void ResetTestFolder()
        {
            var deleteFiles = true;
            if (Directory.Exists(TestFolderPath))
                Directory.Delete(TestFolderPath, deleteFiles);
            Directory.CreateDirectory(TestFolderPath);
        }

        [Test]
        public void CreateBinarySerializer()
        {
            var saver = new SaverBinarySerializer();

            Assert.True(saver is SaverBinarySerializer);
        }

        [Test]
        public void SerializeTree_NonExistantFile_FileExists()
        {
            var testHandler = TestsHandler.CreateDefault();
            string path = testHandler.GetFilePath();

            bool wasExisting = File.Exists(path);
            testHandler.SaveHandler();
            bool isExisting = File.Exists(path);

            Assert.True(!wasExisting && isExisting);
        }

        [Test]
        [ExpectedException]
        public void LoadSerializedTree_NonExistingFile_ThrowsException()
        {
            var saver = new SaverBinarySerializer();
            saver.LoadHandler(TestFilePath);
        }

        [Test]
        public void LoadSerializedTree_FileExists_TreeIsLoaded()
        {
            var testHandler = TestsHandler.CreateDefault();
            testHandler.SaveHandler();
            var path = testHandler.GetFilePath();

            var loadedHandler = HandlerMenu.LoadHandler(new SaverBinarySerializer(), path);

            Assert.True(testHandler.GetFilePath() == loadedHandler.GetFilePath());
        }
    }
}