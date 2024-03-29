using OurPriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Exam2Prep.View
{
    public class QueueView : View
    {
        private PriorQ<int, int> queue;

        public QueueView(PriorQ<int, int> q = null) : base("Priority Queue")
        {
            if (q == null)
            {
                queue = new PriorQ<int, int>(20);
            }
            else
            {
                queue = q;
            }
        }

        protected override void add(int val)
        {
            if (queue.IsFull())
            {
                WriteLine($"The {type} is full going back to the main menu...");
                Run();
            }
            queue.Add(val, val);
            viewResult();
        }

        public override void Remove()
        {
            WriteLine("[ Removing Cell with the greatest (smallest value) priority... ]");
            try
            {
                queue.Remove();
                viewResult();
            }
            catch (ApplicationException)
            {
                WriteLine("The Priority Queue is empty, cannot remove.");
                Run();
            }

        }

        private void viewResult()
        {
            WriteLine($"[ Table Array ]\n{queue}\n[ Heap View ]");
            queue.PrintTree();
            enterToContinue();
        }

        protected override void ViewADT()
        {
            WriteLine(@"
            |================================================|
            | [ Select a View for the Heap \ Priority Queue ]|
            |                                                |
            |    [ a ] Array View - Table Array              |
            |    [ h ] Heap View - Tree View                 |
            |    [ o ] In Order View - Dowells Method        |
            |    [ q ] Go Back                               |
            |                                                |
            |================================================|
            ");
            char key = ReadKey().KeyChar;

            if (key != 'q') handleViewer(key);
        }

        private void handleViewer(char c)
        {
            Clear();

            if (c == 'a') WriteLine(queue);

            else if (c == 'h') queue.PrintTree();

            else if (c == 'o') queue.Print();

            else WriteLine("[ Select a valid menu option ]");

            enterToContinue();
            ViewADT();
        }





    }
}
