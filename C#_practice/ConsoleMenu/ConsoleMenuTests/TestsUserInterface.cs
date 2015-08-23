using NUnit.Framework;
using ConsoleMenuTDD;

namespace ConsoleMenuTests
{
    [TestFixture]
    public class TestsUserInterface
    {
        [Test]
        public void IssueReturnCommand_ToNonRoot_ShowsParentMenuItem()
        {
            var mainMenu = new StubMenu("mainmenu");
            var subMenu = new StubMenu("submenu");
            mainMenu.AddChild(subMenu);
            var cmd = new CommandReturn(subMenu);

            cmd.Execute();
            Assert.True(mainMenu.HasBeenShown);
        }

        [Test]
        public void IssueReturnCommand_ToRoot_QuitsMenu()
        {

        }
    }
}