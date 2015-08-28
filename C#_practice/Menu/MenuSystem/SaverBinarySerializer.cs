using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MenuSystem
{
    [Serializable]
    public class SaverBinarySerializer : Saver
    {
        public override void SaveHandler(Handler handler, string filePath)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, handler);
            stream.Close();
        }

        public override Handler LoadHandler(string filePath)
        {
            if (!File.Exists(filePath))
                throw new Exception("File does not exist.");
            else
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                Handler loadedHandler = (Handler) formatter.Deserialize(stream);
                stream.Close();

                return loadedHandler;
            }

        }
    }
}