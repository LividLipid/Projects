using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MenuSystem
{
    [Serializable]
    public class SaverBinarySerializer : Saver
    {
        public override void SaveHandler(MenuHandler menuHandler, string filePath)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, menuHandler);
            stream.Close();
        }

        public override MenuHandler LoadHandler(string filePath)
        {
            if (!File.Exists(filePath))
                throw new Exception("File does not exist.");
            else
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                MenuHandler loadedMenuHandler = (MenuHandler) formatter.Deserialize(stream);
                stream.Close();

                return loadedMenuHandler;
            }

        }
    }
}