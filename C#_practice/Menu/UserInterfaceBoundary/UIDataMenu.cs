using System.Collections.Generic;

namespace UserInterfaceBoundary
{
    public class UIDataMenu : UIData
    {
        public List<string> ChildrenTitles;

        public UIDataMenu(string title, List<string> childrenTitles) : base(title)
        {
            ChildrenTitles = childrenTitles;
        }
    }
}