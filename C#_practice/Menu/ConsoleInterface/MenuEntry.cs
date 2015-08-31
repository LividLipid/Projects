using System;
using System.Collections.Generic;

namespace ConsoleInterface
{
    [Serializable]
    public class MenuEntry
    {
        public string Text; // String displayed in menu.
        public string Data; // Underlying string data (default operations do not have data).
        public string Operation; // Underlying string coded operation.
        public bool IsDeletable;
        public int DataIndex; // Maps entry to selectable data.

        public MenuEntry(string text, string data, string operation, bool isDeletable, int dataIndex)
        {
            Text = text;
            Data = data;
            Operation = operation;
            IsDeletable = isDeletable;
            DataIndex = dataIndex;
        }
    }
}