using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Exam2Prep.View
{
    public class AVLView : View
    {
        public AVL<int> avl;

        public AVLView(AVL<int> avl = null) : base("AVL Tree")
        {
            if (avl == null)
            {
                this.avl = new AVL<int>();
            }
            else
            {
                this.avl = avl;
            }
        }
        private void error()
        {
            WriteLine($"***\n[ Returning to Main Menu... ]\n***");
        }

        private void emptyCheck()
        {
            if (avl.IsEmpty())
            {
                WriteLine($"[ The AVL Tree is empty... ]");
                error();
            }
        }

        protected override void add(int val)
        {
            try
            {
                WriteLine($"[ PRE INSERTION >> {val} ]\n");
                avl.Prints();
                WriteLine($"[ After Adding {val} to the {type}... ]");
                avl.Insert(val);
                avl.Prints();
                WriteLine();
                enterToContinue();
            }
            catch (ApplicationException)
            {
                WriteLine($"[ {val} already exists in the AVL Tree. AVL Trees are comprised of unique values ]");
                error();
            }
        }

        public override void Remove()
        {
            emptyCheck();

            int rmveVal = getIntput($"[-] Enter a value to Remove from the {type} or q to quit: ");

            if (rmveVal != -1) remove(rmveVal);
        }

        protected override void doClear()
        {
            avl.Clear();
        }

        protected override void remove(int val)
        {
            try
            {
                WriteLine($"[ PRE REMOVAL >> {val} ]\n");
                avl.Prints();
                WriteLine($"[ Removing {val} from the {type}... ]");
                avl.Remove(val);
                avl.Prints();
                enterToContinue();
            }
            catch (ApplicationException)
            {
                WriteLine($"[ {val} does not exist in the AVL Tree. ]");
                error();
            }
        }

        protected override void ViewADT()
        {
            emptyCheck();

            Clear();

            WriteLine($@"
                |==========================================|
                |      [ Select an option to view ]        |
                |                                          |
                |    [ t ] View Tree                       |
                |    [ q ] Go Back                         |
                |==========================================|
            ");
            char k = ReadKey().KeyChar;

            if (k != 'q') handleViewer(k);
        }


        private void handleViewer(char c)
        {
            Clear();

            if (c == 't') avl.Prints();

            else WriteLine("[ Select a valid menu option ]");

            enterToContinue();
            ViewADT();
        }

        public void Clear()
        {
            avl.Clear();
        }








    }
}
