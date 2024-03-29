using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2Prep.View
{
    public class ADTViewer
    {
        private const string titleText = @"
    ===================================================================================
            [ Exam 2 Prep (ADT Visualizer) ]
        (made by ryan hawkins while procrastinating)
            
            [ Select an ADT to Visualize ]
            

            [ a ] Min Priority Queue / Binary Heap
            [ b ] AVL Tree
            [ c ] Dictionary / Hash Table
            [ q ] Quit  

    ===================================================================================";

        private View activeWindows;


        public ADTViewer()
        {
            activeWindows = null;
        }

        // call this to run the UI
        public void Render()
        {
            Console.Clear();
            Console.WriteLine(titleText);

            char key = Console.ReadKey().KeyChar;

            if (key == 'q') Console.WriteLine("[ Exiting UI... ]");

            else handleKeys(key);
        }

        private void handleKeys(char c)
        {
            switch (c)
            {
                case 'a':
                    setActiveWindow(new QueueView());
                    break;
                case 'b':
                    setActiveWindow(new AVLView());
                    break;
                case 'c':
                    setActiveWindow(new DictView());
                    break;
                default:
                    Console.WriteLine("[ Select a valid menu option (a, b, c, q) ]");
                    break;
            }
            Render();
        }
        private void setActiveWindow(View view)
        {
            this.activeWindows = view;
            activeWindows.Run();
        }


    }
}
