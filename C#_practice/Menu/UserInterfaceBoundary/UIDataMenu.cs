using System;
using System.Collections.Generic;

namespace UserInterfaceBoundary
{
    public class UIDataMenu : UIData
    {
        public List<string> ChildrenTitles;

        public List<Type> CreatableTypes;
        public List<string> TypeNames;
        public List<string> TextRequests;

        public UIDataMenu(string title, bool isRoot, List<string> childrenTitles, List<Type> creatableTypes, List<string> typeNames, List<string> textRequests) : base(title, isRoot)
        {
            ChildrenTitles = childrenTitles;
            CreatableTypes = creatableTypes;
            TypeNames = typeNames;
            TextRequests = textRequests;
        }
    }
}