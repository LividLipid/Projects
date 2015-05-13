using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class LogAnalyzer
    {
        private IExtensionManager manager;

        // Constructor used to create seam where fake can be injected
        public LogAnalyzer(IExtensionManager mgr)
        {
            manager = mgr;
        }

        // Unit of work under test:
        public bool IsValidLogFileName(string fileName)
        {
            return manager.IsValid(fileName);
        }
    }

    // Extracting an interface to break dependency
    public interface IExtensionManager
    {
        bool IsValid(string fileName);
    }

    public class FileExtensionManager : IExtensionManager
    {
        public bool IsValid(string fileName)
        {
            return false; // placeholder
        }
    }
}