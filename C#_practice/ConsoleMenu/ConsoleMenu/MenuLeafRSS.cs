using System;
using System.ServiceModel.Syndication;
using System.Xml;

namespace ConsoleMenu
{
    public class MenuLeafRSS : MenuLeaf
    {
        public string URL;

        public MenuLeafRSS(string title, MenuNode parent, string url) : base(title, parent)
        {
            URL = url;
        }

        public override void Act()
        {
            bool returnChosen = false;
            do
            {
                XmlReader reader = XmlReader.Create(URL);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();

                PrintRSS(feed);
                returnChosen = RequestReturnInput();

            } while (!returnChosen);
            
            Parent.Act();
        }

        private void PrintRSS(SyndicationFeed feed)
        {
            Console.Clear();
            foreach (SyndicationItem item in feed.Items)
            {
                Console.WriteLine("- " + item.Title.Text + ":");
                Console.WriteLine(item.Summary.Text);
                Console.WriteLine();
            }
        }

        private bool RequestReturnInput()
        {
            Console.WriteLine();
            Console.WriteLine("Press Escape or Backspace to return.");
            Console.WriteLine("Press any other key to refresh.");

            ConsoleKey keypress = Console.ReadKey().Key;
            bool returnChosen = (keypress == ConsoleKey.Escape) || (keypress == ConsoleKey.Backspace);
            return returnChosen;
        }
    }
}