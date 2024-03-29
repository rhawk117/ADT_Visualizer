using Exam2Prep.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2Prep
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // note you can pass queues, avls, and dicts to the views
            // however it's not required and is an optional parameter

            //QueueView qv = new QueueView();
            //qv.Run();

            //AVLView avl = new AVLView();
            //avl.Run();

            //DictView dv = new DictView();
            //dv.Run();

            // UI Master - Select an ADT to visualize
            ADTViewer adt = new ADTViewer();
            adt.Render();

        }
    }
}
