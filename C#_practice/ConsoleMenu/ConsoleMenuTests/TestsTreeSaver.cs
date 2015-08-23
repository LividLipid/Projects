using System.IO;
using ConsoleMenuTDD;
using NUnit.Framework;

namespace ConsoleMenuTests
{
    [TestFixture]
    public class TestsTreeSaver
    {
        public static string TestFolderPath = @"C:\Projects\C#_practice\ConsoleMenu\SavedMenus\TestFiles";
        public static string TestFileName = "TestFile";
        public static string TestFilePath = TestFolderPath + @"\" + TestFileName;

        [SetUp]
        public void Init()
        {
            var deleteFiles = true;
            if (Directory.Exists(TestFolderPath))
                Directory.Delete(TestFolderPath, deleteFiles);
            Directory.CreateDirectory(TestFolderPath);
        }

        [Test]
        public void CreateBinarySerializer_DoesNotExist_IsCreated()
        {
            var treeSaver = BinarySerializer.Instance;

            Assert.True(treeSaver is BinarySerializer);
        }

        [Test]
        public void CreateBinarySerializer_AlreadyExists_AreTheSame()
        {
            var treeSaver1 = BinarySerializer.Instance;
            var treeSaver2 = BinarySerializer.Instance;

            Assert.True(treeSaver1 == treeSaver2);
        }

        [Test]
        public void SerializeTree_NonExistantFile_FileExists()
        {
            var saver = BinarySerializer.Instance;
            var tree = new TreeExample().Root;
            saver.SaveTree(tree, TestFilePath);

            Assert.True(File.Exists(TestFilePath));
        }

        [Test]
        [ExpectedException]
        public void LoadSerializedTree_NonExistingFile_ThrowsException()
        {
            var saver = BinarySerializer.Instance;
            var tree = saver.LoadTree(TestFilePath);
        }

        [Test]
        public void LoadSerializedTree_ExistantFile_TreeIsLoaded()
        {
            var saver = BinarySerializer.Instance;
            var tree = new TreeExample().Root;
            saver.SaveTree(tree, TestFilePath);

            var loadedTree = saver.LoadTree(TestFilePath);

            Assert.True(loadedTree != null);
        }
    }
}