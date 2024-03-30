using Exam2Prep.View;
using System;
namespace Exam2Prep
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Utils.StartUp(); // you can comment this line after it runs for the first time

            // UI Master - Select an ADT to visualize in the menu itself
            ADTViewer adt = new ADTViewer();
            adt.RenderUI();

            // note you can pass queues, avls, and dicts to the views
            // however it's not required and is an optional parameter

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
