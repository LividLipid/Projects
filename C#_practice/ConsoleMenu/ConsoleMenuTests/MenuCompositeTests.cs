using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ConsoleMenuTDD;

namespace ConsoleMenuTests
{
    [TestFixture]
    public class MenuCompositeTests
    {
        private MenuComposite _mainMenu;
        private string _mainMenuTitle = "Main Menu";
        private MenuComposite _subMenu1;
        private string _subMenu1Title = "Submenu 1";
        private MenuComposite _subMenu2;
        private string _subMenu2Title = "Submenu 2";


        [SetUp]
        public void Init()
        {
            
            _mainMenu = new MenuComposite(_mainMenuTitle);
            _subMenu1 = new MenuComposite(_subMenu1Title);
            _subMenu2 = new MenuComposite(_subMenu2Title);
        }

        [TearDown]
        public void CleanUp()
        {
        }

        [Test]
        public void CreateEmptyMenu()
        {
            Assert.IsInstanceOf(typeof(MenuComposite), _mainMenu);
        }

        [Test]
        public void MenuIsComponentSubtype()
        {
            Assert.True(_mainMenu.GetType().IsSubclassOf(typeof(MenuComponent)));
        }

        [Test]
        public void ReadMenuTitle()
        {
            Assert.True(_mainMenu.Title == _mainMenuTitle);
        }

        [Test]
        public void AddSubMenu_ToEmptyMenu_TitleIsCorrect()
        {
            _mainMenu.AddChild(_subMenu1);
            MenuComposite nestedMenu = (MenuComposite) _mainMenu.GetChild(0);
            Assert.True(nestedMenu.Title == _subMenu1Title);
        }

        [Test]
        public void AddSubMenu_ToNonEmptyMenu_TitleIsCorrect()
        {
            _mainMenu.AddChild(_subMenu1);
            _mainMenu.AddChild(_subMenu2);
            MenuComposite nestedMenu = (MenuComposite)_mainMenu.GetChild(1);
            Assert.True(nestedMenu.Title == _subMenu2Title);
        }

        [Test]
        public void RemoveSubMenu_FromEmptyMenu_NothingHappens()
        {
            _mainMenu.RemoveChild(0);
        }
        
        [Test]
        public void RemoveSubMenu_FromNonEmptyMenu_SubMenuIsGone()
        {
            _mainMenu.AddChild(_subMenu1);
            _mainMenu.AddChild(_subMenu2);
            var countBefore = _mainMenu.ChildrenCount;
            _mainMenu.RemoveChild(0);
            var countAfter = _mainMenu.ChildrenCount;

            Assert.True((countBefore == 2) && (countAfter==1));
        }

        [Test]
        public void GetChildrenTitles_FromEmptyMenu_ListIsEmpty()
        {
            var readTitles = _mainMenu.GetChildrenTitles();
            Assert.IsEmpty(readTitles);
        }

        [Test]
        public void GetChildrenTitles_FromNonEmptyMenu_ListIsCorrect()
        {
            _mainMenu.AddChild(_subMenu1);
            _mainMenu.AddChild(_subMenu2);
            var subMenuTitles = new List<string> { _subMenu1.Title, _subMenu2Title};
            var readTitles = _mainMenu.GetChildrenTitles();
            Assert.True(subMenuTitles.SequenceEqual(readTitles));
        }

        [Test]
        public void ChildHasParent()
        {
            _mainMenu.AddChild(_subMenu1);
            var gottenChild = _mainMenu.GetChild(0);
            Assert.True(_mainMenu.Equals(gottenChild.Parent));
        }

        [Test]
        public void RootIsItsOwnParent()
        {
            Assert.True(_mainMenu.Equals(_mainMenu.Parent));
        }
    }
}
