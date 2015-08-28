using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using NUnit.Framework;
using Menu;

namespace MenuTests
{
    [TestFixture]
    public class TestsMenu
    {
        const int FirstLevel = 1;

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void CleanUp()
        {
        }


        private Item ArrangeItemAtLevel(int targetLevel, Type itemType)
        {
            // Arrange a nested item and return it.
            // All higher level items are of type Menu.
            if (targetLevel < FirstLevel)
                throw new ArgumentException("Level must be" + FirstLevel + " or greater.");
            else
                return GenerateArrangedItemAtLevel(targetLevel, itemType);
        }

        private Item GenerateArrangedItemAtLevel(int targetLevel, Type itemType)
        {
            // Most deeply nested item of selected type.
            var arrangedItem = CreateTestItem(targetLevel, itemType);

            // Go backwards and nest the item within submenus.
            var currentLevel = targetLevel - 1;
            var currentItem = arrangedItem;
            while (currentLevel >= FirstLevel)
            {
                var higherItem = CreateTestItem(currentLevel, typeof(ItemMenu));
                higherItem.AddChild(currentItem);
                currentItem = higherItem;
                currentLevel--;
            }
            return arrangedItem;
        }

        private Item ArrangeItemAtLevelAndReturnRoot(int level, Type itemType)
        {
            var item = ArrangeItemAtLevel(level, itemType);
            return item.GetRoot();
        }

        private Item CreateTestItem(int level, Type type)
        {
            var title = CreateTestItemTitle(level, type);
            return ItemFactory.Create(type, title);
        }

        private string CreateTestItemTitle(int level, Type type)
        {
            return "Level " + level + " " + type.Name;
        }

        private bool CompareItemListTitles(IEnumerable<Item> list1, IEnumerable<Item> list2)
        {
            var ordered1 = list1.OrderBy(t => t.Title);
            var ordered2 = list2.OrderBy(t => t.Title);
            return ordered1.SequenceEqual(ordered2); ;
        }

        private StubHandler CreateTreeWithStubHandler()
        {
            var root = new ExampleTree().Root;
            var stub = new StubHandler();
            stub.SetTreeRoot(root);
            return stub;
        }

        [Test]
        public void CreateEmptyMenu()
        {
            var menu = (ItemMenu)ArrangeItemAtLevel(FirstLevel, typeof(ItemMenu));
            Assert.IsInstanceOf(typeof(ItemMenu), menu);
        }

        [Test]
        public void CreateGenericLeaf()
        {
            var leaf = (ItemLeaf)ArrangeItemAtLevel(2, typeof(ItemLeaf));
            Assert.IsInstanceOf(typeof(ItemLeaf), leaf);
        }

        [Test]
        public void MenuIsComponentSubtype()
        {
            var menu = (ItemMenu)ArrangeItemAtLevel(FirstLevel, typeof(ItemMenu));
            Assert.True(menu.GetType().IsSubclassOf(typeof(Item)));
        }

        [Test]
        public void ReadMenuTitle()
        {
            var menu = (ItemMenu)ArrangeItemAtLevel(FirstLevel, typeof(ItemMenu));
            Assert.True(menu.Title == CreateTestItemTitle(FirstLevel, typeof(ItemMenu)));
        }

        [Test]
        public void AddSubMenu_ToEmptyMenu_TitleIsCorrect()
        {
            var level = 2;
            var submenu = (ItemMenu)ArrangeItemAtLevel(level, typeof(ItemMenu));
            Assert.True(submenu.Title == CreateTestItemTitle(level, typeof(ItemMenu)));
        }

        [Test]
        public void RemoveSubMenu_FromEmptyMenu_NothingHappens()
        {
            var menu = (ItemMenu)ArrangeItemAtLevel(FirstLevel, typeof(ItemMenu));
            menu.RemoveChild(0);
        }
        
        [Test]
        public void RemoveSubMenu_FromNonEmptyMenu_SubMenuIsGone()
        {
            var mainMenu = (ItemMenu)ArrangeItemAtLevelAndReturnRoot(2, typeof(ItemMenu));
            mainMenu.RemoveChild(0);

            Assert.True(mainMenu.ChildrenCount == 0);
        }

        [Test]
        public void GetChildrenTitles_FromEmptyMenu_ListIsEmpty()
        {
            var menu = (ItemMenu)ArrangeItemAtLevel(FirstLevel, typeof(ItemMenu));
            var titles = menu.GetChildrenTitles();
            Assert.IsEmpty(titles);
        }

        [Test]
        public void GetChildrenTitles_FromNonEmptyMenu_ListIsCorrect()
        {
            var mainMenu = (ItemMenu)ArrangeItemAtLevel(FirstLevel, typeof(ItemMenu));
            var title1 = "Submenu1";
            var title2 = "Submenu2";
            mainMenu.AddChild(new ItemMenu(title1));
            mainMenu.AddChild(new ItemMenu(title2));
            var subMenuTitles = new List<string> {title1, title2};
            var readTitles = mainMenu.GetChildrenTitles();
            Assert.True(subMenuTitles.SequenceEqual(readTitles));
        }

        [Test]
        public void ChildHasParent()
        {
            var subMenu = (ItemMenu)ArrangeItemAtLevel(2, typeof(ItemMenu));
            var mainMenu = subMenu.GetRoot();
            Assert.True(mainMenu.Equals(subMenu.Parent));
        }

        [Test]
        public void GetParent_OfRoot_IsSentinel()
        {
            var mainMenu = (ItemMenu)ArrangeItemAtLevel(FirstLevel, typeof(ItemMenu));
            Assert.True(mainMenu.Parent.IsSentinel());
        }

        [Test]
        public void CheckIfRoot_IsNotRoot_ReturnsFalse()
        {
            var subMenu = (ItemMenu)ArrangeItemAtLevel(2, typeof(ItemMenu));
            Assert.False(subMenu.IsRoot());
        }

        [Test]
        public void CheckIfRoot_IsRoot_ReturnsTrue()
        {
            var mainMenu = (ItemMenu)ArrangeItemAtLevelAndReturnRoot(2, typeof(ItemMenu));
            Assert.True(mainMenu.IsRoot());
        }

        [Test]
        public void FindRoot_FromRoot_ReturnsSelf()
        {
            var mainMenu = (ItemMenu)ArrangeItemAtLevelAndReturnRoot(3, typeof(ItemMenu));
            var root = mainMenu.GetRoot();
            Assert.True(root.Equals(mainMenu));
        }

        [Test]
        public void FindRoot_FromNestedMenu_ReturnsRoot()
        {
            var grandchild = (ItemMenu) ArrangeItemAtLevel(FirstLevel + 2, typeof (ItemMenu));
            var root = grandchild.GetRoot();
            Assert.True(root.IsRoot());
        }

        [Test]
        public void FindRoot_FromNestedLeaf_ReturnsRoot()
        {
            var leaf = (ItemLeaf) ArrangeItemAtLevel(FirstLevel + 3, typeof(ItemLeaf));
            var root = leaf.GetRoot();
            Assert.True(root.IsRoot());
        }

        [Test]
        [ExpectedException]
        public void AddChild_ToLeaf_ThrowsException()
        {
            var leaf1 = (ItemLeaf)ArrangeItemAtLevel(FirstLevel, typeof(ItemLeaf));
            var leaf2 = (ItemLeaf)ArrangeItemAtLevel(FirstLevel, typeof(ItemLeaf));

            leaf1.AddChild(leaf2);
        }

        [Test]
        public void LevelOrderTraversal_FromRoot_CorrectWalk()
        {
            var testTree = new ExampleTree();
            var i = new IteratorLevelOrderWalk(testTree.Root);
            
            var walkList = new List<string>();
            while (!i.IsDone())
            {
                walkList.Add(i.CurrentItem().Title);
                i.Next();
            }
            var isCorrectWalk = walkList.SequenceEqual(testTree.CorrectLevelOrder);
            Assert.True(isCorrectWalk);
        }

        [Test]
        public void SearchTree_TargetExists_ReturnsTarget()
        {
            var testTree = new ExampleTree();
            var i = new IteratorLevelOrderWalk(testTree.Root);
            var targetTitle = testTree.CorrectLevelOrder[testTree.CorrectLevelOrder.Count-1];
            var target = i.SearchForTitle(targetTitle);

            Assert.True(target.Title.Equals(targetTitle));
        }

        [Test]
        public void SearchTree_TargetDoesNotExist_ReturnsSentinel()
        {
            var testTree = new ExampleTree();
            var i = new IteratorLevelOrderWalk(testTree.Root);
            var targetTitle = "Nonexisting title";
            var target = i.SearchForTitle(targetTitle);

            Assert.True(target.GetType() == typeof(ItemSentinel));
        }

        [Test]
        [ExpectedException]
        public void AddChild_IsAlreadyInTree_ThrowsException()
        {
            var mainMenu = CreateTestItem(FirstLevel, typeof (ItemMenu));
            var subMenu = CreateTestItem(FirstLevel + 1, typeof(ItemMenu));

            mainMenu.AddChild(subMenu);
            mainMenu.AddChild(subMenu);
        }

        [Test]
        public void CheckIfItemIsInTree_IsInTree_ReturnsTrue()
        {
            var testTree = new ExampleTree();
            var targetNode = testTree.ListOfNodes.First();
            var originNode = testTree.ListOfNodes.Last();

            Assert.True(originNode.HasInTree(targetNode));
        }

        [Test]
        public void CheckIfItemIsInTree_IsNotInTree_ReturnsFalse()
        {
            var testTree = new ExampleTree();
            var targetNode = CreateTestItem(1, typeof (ItemMenu));
            var originNode = testTree.ListOfNodes.Last();

            Assert.False(originNode.HasInTree(targetNode));
        }

        [Test]
        public void GetSubTreeLeaves_FromRoot_ReturnsLeaves()
        {
            var testTree = new ExampleTree();
            var knownLeaves = testTree.ListOfLeaves;
            var foundLeaves = testTree.Root.GetSubTreeLeaves();
            var areEqual = CompareItemListTitles(knownLeaves, foundLeaves);

            Assert.True(areEqual);
        }

        [Test]
        public void GetSubTreeLeaves_FromLeaf_ReturnsSelf()
        {
            var menu = new ItemMenu("Mainmenu");
            var leaf = new ItemLeaf("Leaf");
            menu.AddChild(leaf);
            var leaves = leaf.GetSubTreeLeaves();
            bool returnedSelf = (leaf == leaves.First()) && (leaf == leaves.Last());

            Assert.True(returnedSelf);
        }

        [Test]
        public void SaveTree_FromRoot_IsSaved()
        {
            var stub = CreateTreeWithStubHandler();
            var root = stub.GetTreeRoot();
            root.SaveTree();

            Assert.True(stub.HasBeenSaved);
        }

        [Test]
        public void SaveTree_FromNestedItem_IsSaved()
        {
            var stub = CreateTreeWithStubHandler();
            var root = stub.GetTreeRoot();
            var leaves = root.GetSubTreeLeaves();
            leaves[0].SaveTree();

            Assert.True(stub.HasBeenSaved);
        }
    }
}
