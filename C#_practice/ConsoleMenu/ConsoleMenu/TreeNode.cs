using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConsoleMenu
{
    [Serializable]
    class TreeNode<T>
    {
        private T Data;
        private LinkedList<TreeNode<T>> children;
        public int ChildrenCount = 0;

        public TreeNode(T data)
        {
            this.Data = data;
            children = new LinkedList<TreeNode<T>>();
        }

        public void AddChild(T data)
        {
            children.AddLast(new TreeNode<T>(data));
            ChildrenCount++;
        }

        public void RemoveChild(int i)
        {
            foreach (TreeNode<T> n in children)
                if (--i == 0)
                {
                    children.Remove(n);
                }
        }

        public TreeNode<T> GetChild(int i)
        {
            foreach (TreeNode<T> n in children)
                if (--i == 0)
                    return n;
            return null;
        }

        public void Traverse(TreeNode<T> node, Action<T> visitor)
        {
            visitor(node.Data);
            foreach (TreeNode<T> kid in node.children)
                Traverse(kid, visitor);
        }

        public T GetData()
        {
            return Data;
        }

        public List<T> GetChildrenData()
        {
            var childDataList = new List<T>(ChildrenCount);
            var i = 0;
            foreach (var n in children)
            {
                childDataList[i] = (T) n.Data;
                i++;
            }
            return childDataList;
        } 
    }
}