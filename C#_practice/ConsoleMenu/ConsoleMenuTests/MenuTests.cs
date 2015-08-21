using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ConsoleMenuTDD;

namespace ConsoleMenuTests
{
    [TestFixture]
    public class MenuTests
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


        private MenuItem ArrangeItemAtLevel(int level, Type itemType)
        {
            // Level == 1: return main menu.
            // Level > 1: return submenu item of some type.

            if (level >= 1)
            {
                var type = typeof(Menu);
                var title = CreateTestItemTitle(FirstLevel, type);
                var currentItem = MenuItemFactory.Create(type, title);

                var i = FirstLevel + 1;
                while (i <= level)
                {
                    type = itemType;
                    title = CreateTestItemTitle(i, type);
                    var nextItem = MenuItemFactory.Create(type, title);
                    currentItem.AddChild(nextItem);
                    currentItem = nextItem;
                    i++;
                }
                return currentItem;
            }
            else
                throw new ArgumentException("Level must be 1 or greater.");
        }

        private MenuItem ArrangeItemAtLevelAndReturnRoot(int level, Type itemType)
        {
            var item = ArrangeItemAtLevel(level, itemType);
            return item.GetRoot();
        }

        private string CreateTestItemTitle(int level, Type type)
        {
            return "Level " + level + " " + type.Name;
        }


        [Test]
        public void CreateEmptyMenu()
        {
            var menu = (Menu)ArrangeItemAtLevel(FirstLevel, typeof(Menu));
            Assert.IsInstanceOf(typeof(Menu), menu);
        }

        [Test]
        public void CreateGenericLeaf()
        {
            var leaf = (Leaf)ArrangeItemAtLevel(2, typeof(Leaf));
            Assert.IsInstanceOf(typeof(Leaf), leaf);
        }

        [Test]
        public void MenuIsComponentSubtype()
        {
            var menu = (Menu)ArrangeItemAtLevel(FirstLevel, typeof(Menu));
            Assert.True(menu.GetType().IsSubclassOf(typeof(MenuItem)));
        }

        [Test]
        public void ReadMenuTitle()
        {
            var menu = (Menu)ArrangeItemAtLevel(FirstLevel, typeof(Menu));
            Assert.True(menu.Title == CreateTestItemTitle(FirstLevel, typeof(Menu)));
        }

        [Test]
        public void AddSubMenu_ToEmptyMenu_TitleIsCorrect()
        {
            var level = 2;
            var submenu = (Menu)ArrangeItemAtLevel(level, typeof(Menu));
            Assert.True(submenu.Title == CreateTestItemTitle(level, typeof(Menu)));
        }

        [Test]
        public void RemoveSubMenu_FromEmptyMenu_NothingHappens()
        {
            var menu = (Menu)ArrangeItemAtLevel(FirstLevel, typeof(Menu));
            menu.RemoveChild(0);
        }
        
        [Test]
        public void RemoveSubMenu_FromNonEmptyMenu_SubMenuIsGone()
        {
            var mainMenu = (Menu)ArrangeItemAtLevelAndReturnRoot(2, typeof(Menu));
            mainMenu.RemoveChild(0);

            Assert.True(mainMenu.ChildrenCount == 0);
        }

        [Test]
        public void GetChildrenTitles_FromEmptyMenu_ListIsEmpty()
        {
            var menu = (Menu)ArrangeItemAtLevel(FirstLevel, typeof(Menu));
            var titles = menu.GetChildrenTitles();
            Assert.IsEmpty(titles);
        }

        [Test]
        public void GetChildrenTitles_FromNonEmptyMenu_ListIsCorrect()
        {
            var mainMenu = (Menu)ArrangeItemAtLevel(FirstLevel, typeof(Menu));
            var title1 = "Submenu1";
            var title2 = "Submenu1";
            mainMenu.AddChild(MenuItemFactory.Create(typeof(Menu), title1));
            mainMenu.AddChild(MenuItemFactory.Create(typeof(Menu), title2));
            var subMenuTitles = new List<string> {title1, title2};
            var readTitles = mainMenu.GetChildrenTitles();
            Assert.True(subMenuTitles.SequenceEqual(readTitles));
        }

        [Test]
        public void ChildHasParent()
        {
            var subMenu = (Menu)ArrangeItemAtLevel(2, typeof(Menu));
            var mainMenu = subMenu.GetRoot();
            Assert.True(mainMenu.Equals(subMenu.Parent));
        }

        [Test]
        public void RootIsItsOwnParent()
        {
            var mainMenu = (Menu)ArrangeItemAtLevel(FirstLevel, typeof(Menu));
            Assert.True(mainMenu.Equals(mainMenu.Parent));
        }

        [Test]
        public void CheckIfRoot_IsNotRoot_ReturnsFalse()
        {
            var subMenu = (Menu)ArrangeItemAtLevel(2, typeof(Menu));
            Assert.False(subMenu.IsRoot());
        }

        [Test]
        public void CheckIfRoot_IsRoot_ReturnsTrue()
        {
            var mainMenu = (Menu)ArrangeItemAtLevelAndReturnRoot(2, typeof(Menu));
            Assert.True(mainMenu.IsRoot());
        }

        [Test]
        public void FindRoot_StartFromRoot_ReturnsSelf()
        {
            var mainMenu = (Menu)ArrangeItemAtLevelAndReturnRoot(3, typeof(Menu));
            Menu root = mainMenu.GetRoot();
            Assert.True(root.Equals(mainMenu));
        }

        public void FindRoot_StartFrom3rdLevelMenu_ReturnsRoot()
        {
            var grandchild = (Menu) ArrangeItemAtLevel(3, typeof (Menu));
            Menu root = grandchild.GetRoot();
            Assert.True(root.IsRoot());
        }

        public void FindRoot_FromLevel4Leaf_ReturnsRoot()
        {
            var leaf = (Leaf) ArrangeItemAtLevel(4, typeof(Leaf));
            Menu root = leaf.GetRoot();
            Assert.True(root.IsRoot());
        }

    }
}
