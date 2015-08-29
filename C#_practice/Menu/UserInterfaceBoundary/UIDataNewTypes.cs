using System;
using System.Collections.Generic;

namespace UserInterfaceBoundary
{
    public class UIDataNewTypes : UIData
    {
        public List<Type> CreatableTypes;
        public List<string> Names; 

        public UIDataNewTypes(string title, List<Type> creatableTypes, List<string> typeNames) : base(title)
        {
            CreatableTypes = creatableTypes;
            Names = typeNames;
        }
    }
}