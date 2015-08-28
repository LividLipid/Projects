using System;
using System.Collections.Generic;

namespace UserInterfaceBoundary
{
    public class UIDataNewItem : UIData
    {
        public List<Type> CreatableTypes;
        public List<string> Names; 

        public UIDataNewItem(string title, List<Type> creatableTypes) : base(title)
        {
            CreatableTypes = creatableTypes;

            // Very ugly implementation, can this be done with polymorphism?
            Names = new List<string>();
            foreach (var type in CreatableTypes)
            {
                var tmp = ItemFactory.Create(type, "");
                Names.Add(tmp.GetItemTypeName());
            }
        }
    }
}