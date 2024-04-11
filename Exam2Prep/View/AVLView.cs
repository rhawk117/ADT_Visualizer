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
        public AVL<int> avl = new AVL<int>();

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
                Clear();
                WriteLine($"[ PRE INSERTION >> {val} ]\n");
                safePrint();
                enterToContinue();
                WriteLine($"[ After Adding {val} to the {type}... ]");

                avl.Insert(val);
                safePrint();
                WriteLine();
            }
            catch (ApplicationException)
            {
                WriteLine($"[ {val} already exists in the AVL Tree. AVL Trees are comprised of unique values ]");
                error();
            }
            catch (Exception e)
            {
                WriteLine($"[!] Unhandled -> {e.Message} <- occured while inserting in the AVL");

            }
            finally
            {
                enterToContinue();
            }
        }

        public override void Remove()
        {
            if (avl.IsEmpty())
            {
                WriteLine("[ Cannot Remove on an Empty AVL ]");
                enterToContinue();
                return;
            }
            Clear();
            char c;
            WriteLine(@"

            *===============================*
            |    [ Select an Option ]       |
            |                               |
            | [ r ] Remove Root             |
            | [ i ] Remove a Node           |
            | [ q ] Go Back                 |
            |                               |
            *===============================*

            ");
            c = char.ToLower(ReadKey().KeyChar);

            if (c != 'q')
            {
                handleRemove(c);
            }
        }

        private void handleRemove(char c)
        {
            switch (c)
            {
                case 'r':
                    rmveRoot();
                    break;
                case 'i':
                    rmveNode();
                    break;
                default:
                    WriteLine("[ Select a Valid Menu Option ]");
                    break;
            }
            enterToContinue();
            Remove();
        }
        private void rmveRoot()
        {
            int rVal = avl.rootData;

            if (rVal == default)
            {
                WriteLine("[ AVL returned 0 for root (i.e root is null).. ]\n [ Remove cannot be performed.. ]");
            }
            else
            {
                remove(rVal);
            }
        }

        private void rmveNode()
        {
            WriteLine("[-] NODES TO REMOVE [-]\n");
            avl.InOrder();
            int rmveVal = getIntput($"[-] Enter a value displayed above to Remove from the {type} or 'q' to quit: ");

            if (rmveVal != -1)
            {
                remove(rmveVal);
            }
        }

        protected void safePrint()
        {
            enterToContinue();
            Clear();
            avl.Prints();
        }

        protected override void doClear() => avl.Clear();
        protected override void remove(int val)
        {
            try
            {
                WriteLine($"[ PRE REMOVAL >> {val} ]\n");
                safePrint();
                enterToContinue();
                WriteLine($"[ Removing {val} from the {type}... ]");
                avl.Remove(val);
                safePrint();
            }
            catch (ApplicationException)
            {
                WriteLine($"[ {val} does not exist in the AVL Tree. ]");
            }
            catch (Exception e)
            {
                WriteLine($"[ ! ] Unhandled -> {e.Message} <- exception in the AVL Tree...");
            }
            finally
            {
                ResetColor();
            }

        }

        protected override void ViewADT()
        {
            emptyCheck();

            Clear();

            char k;

            WriteLine($@"
                |==========================================|
                |      [ Select an option to view ]        |
                |                                          |
                |     [ t ] View Tree                      |
                |     [ i ] In Order                       |
                |     [ b ] Pre Order                      |
                |     [ p ] Post Order                     |
                |     [ q ] Go Back                        |
                |==========================================|
            ");
            k = char.ToLower(ReadKey().KeyChar);
            if (k != 'q')
            {
                handleViewer(k);
            }
        }


        private void handleViewer(char c)
        {
            switch (c)
            {
                case 't':
                    avl.Prints();
                    break;

                case 'i':
                    avl.InOrder();
                    break;

                case 'b':
                    avl.PreOrder();
                    break;

                case 'p':
                    avl.PostOrder();
                    break;

                default:
                    WriteLine("[ ! ] Select a valid menu option [ ! ]");
                    break;
            }
            enterToContinue();
            ViewADT();
        }
    }
}
