using System;
using System.Collections.Generic;

namespace UserInterfaceBoundary
{
    public class UIDataMenu : UIData
    {
        public List<string> ChildrenTitles;

        public List<Type> CreatableTypes;
        public List<string> CreatableTypeNames;

        public UIDataMenu(string title, bool isRoot, List<string> childrenTitles, List<Type> creatableTypes, List<string> creatableTypeNames) : base(title, isRoot)
        {
            ChildrenTitles = childrenTitles;
            CreatableTypes = creatableTypes;
            CreatableTypeNames = creatableTypeNames;
        }
    }
}