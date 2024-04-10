using Exam2Prep.View;
using System;
namespace Exam2Prep
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // you can comment this line after it runs for the first time (lol)
            //Utils.StartUp();

            // UI Master - Select an ADT to visualize in the menu itself

            //Console.WriteLine(107 % 101);

            //for (int i = 0; i < 105; i++)
            //{
            //    Console.WriteLine($"{i} % 101 = {i % 101}");

            //}

            RunUI();



        }

        static void RunUI()
        {
            ADTViewer adt = new ADTViewer();
            adt.RenderUI();
        }

        static void runIndividual()
        {
            // Priority Queue / Binary Heap
            // QueueView qv = new QueueView();
            // qv.Run();

            // AVL Tree
            // AVLView avl = new AVLView();
            // avl.Run();

            // Dictionary / Hash Table
            // DictView dv = new DictView();
            // dv.Run();
        }


    }
}
