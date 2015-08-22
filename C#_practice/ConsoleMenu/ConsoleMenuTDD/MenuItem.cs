using System;

namespace ConsoleMenuTDD
{
    [Serializable]
    public abstract class MenuItem
    {


        // The title uniquely identifies a menuitem,
        // and the same item cannot exist twice in the same tree.
        public string Title { get; } 
        public Menu Parent { get; set; }
        public int ChildrenCount { get; set; }
        private string _filePath;
        private TreeSaver _saver;

        protected MenuItem(string title)
        {
            Title = title;
            ChildrenCount = 0;
        }

        public abstract void AddChild(MenuItem child);
        public abstract MenuItem GetChild(int i);
        public abstract void RemoveChild(int i);
        //public abstract MenuItem DisplayAndReturnNextMenu();
        public abstract bool IsRoot();

        public virtual bool IsSentinel()
        {
            return false;
        }

        public Menu GetRoot()
        {
            var i = new IteratorParentWalk(this);
            return (Menu) i.GetFinal();
        }

        public bool HasInTree(MenuItem item)
        {
            var root = GetRoot();
            var isInTree = root.HasInSubTree(item);

            return isInTree;
        }

        public bool HasInSubTree(MenuItem item)
        {
            var i = new IteratorLevelOrderWalk(this);
            var foundItem = i.SearchForTitle(item.Title);
            var isInSubTree = !foundItem.IsSentinel();

            return isInSubTree;
        }

        public bool SaveTree()
        {
            var outcome = IsRoot() ? ExecuteSaveOperation() : GetRoot().ExecuteSaveOperation();
            return outcome;
        }

        public void SetFilePath(string filePath)
        {
            if (IsRoot())
                _filePath = filePath;
            else
                GetRoot().SetFilePath(filePath);
        }

        private bool ExecuteSaveOperation()
        {
            if (_filePath == null)
                throw new Exception("Filepath has not been set.");
            if (_saver == null)
                throw new Exception("Treesaver has not been set.");
            var outcome = _saver.SaveTree(this, _filePath);
            return outcome;
        }

        public void SetSaver(TreeSaver saver)
        {
            if (IsRoot())
                _saver = saver;
            else
                GetRoot().SetSaver(saver);
        }

    }
}