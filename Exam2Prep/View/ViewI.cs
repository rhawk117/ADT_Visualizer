using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Exam2Prep.View
{
    public class View
    {
        protected string type;

        protected string title;
        public View(string type)
        {
            title = $@"
        ================================================
                [ {type} Visualizer ]

                [ a ] Add
                [ r ] Remove
                [ c ] Clear / Reset
                [ v ] View {type}
                [ q ] Quit
          =============================================            
            ";
            this.type = type;
        }

        public void Run()
        {
            Clear();
            WriteLine(title);

            char key = ReadKey().KeyChar;
            key = char.ToLower(key);

            if (key == 'q') WriteLine("\t\t[ Exiting UI... ]");

            else handleKeys(key);
        }

        public void handleKeys(char c)
        {
            switch (c)
            {
                case 'a':
                    Add();
                    break;

                case 'r':
                    Remove();
                    break;

                case 'v':
                    ViewADT();
                    break;

                case 'c':
                    ClearADT();
                    break;

                default:
                    WriteLine("[ Select a valid menu option (a, r, v, q) ]");
                    break;
            }

            Run();
        }

        /// <summary>
        /// In theory you'd just call ToStr() on the data structure
        /// however I created various ways of viewing the data structure
        /// so maybe this makes more sense to be virtual 
        /// </summary>

        protected virtual void ViewADT() { }

        protected void ClearADT()
        {
            char c;
            Write($@"
            [ ? ] Clear the {type} [ ? ]
            [ ! ] Warning this cannot be undone [ ! ]
            
            >> Type Y to proceed or anything else to back out: 
            ");
            c = char.ToLower(ReadKey(true).KeyChar);
            if (c != 'y')
            {
                WriteLine($"[ ... ] Cancelling clearing the {type}");
                enterToContinue();
                return;
            }
            WriteLine($"[ X ] Clearing the {type} [ X ]");
            doClear();
            enterToContinue();
        }

        protected virtual void doClear() { }

        // peek DRY (don't repeat yourself) principle
        protected void enterToContinue()
        {
            WriteLine("\n[ Press Enter to Continue... ]\n");
            ReadKey();
        }

        protected virtual void add(int val) { }

        protected virtual void remove(int val) { }

        // adding should be the same for all types of data structures
        public void Add()
        {
            int addVal = getIntput($"[ Enter a value to add to the {type} or q to go back: ");

            if (addVal != -1) add(addVal);

            else Run();
        }
        // can't really generalize this one, children classes implement own
        public virtual void Remove() { }

        // funny pun
        public int getIntput(string prompt)
        {
            Clear();
            WriteLine(prompt);
            int val;
            string input = ReadLine();

            if (input.ToLower() == "q") return -1;

            else if (!int.TryParse(input, out val))
            {
                WriteLine("[ Input an integer value type q if you want to exit ]");
                return getIntput(prompt);
            }
            return val;
        }





    }







}
