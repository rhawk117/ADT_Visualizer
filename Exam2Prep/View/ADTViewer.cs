using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2Prep.View
{
    // made by ryan hawkins - enjoy =)
    public class ADTViewer
    {
        // Cool ASCII Art from - https://edukits.co/text-art/
        private const string MENUTEXT = @"
    ==================================================================================
    |       __  _____ _  _    ___   ___   __                                         |
    |      / / |___ /| || |  / _ \ / _ \  \ \                                        |
    |     / /    |_ \| || |_| | | | | | |  \ \                                       |
    |     \ \   ___) |__   _| |_| | |_| |  / /                                       |
    |      \_\ |____/   |_|  \___/ \___/  /_/                                        |
    |                                                                                |
    |     ____        _          ____  _                   _                         |
    |    |  _ \  __ _| |_ __ _  / ___|| |_ _ __ _   _  ___| |_ _   _ _ __ ___  ___   |
    |    | | | |/ _` | __/ _` | \___ \| __| '__| | | |/ __| __| | | | '__/ _ \/ __|  |
    |    | |_| | (_| | || (_| |  ___) | |_| |  | |_| | (__| |_| |_| | | |  __/\__ \  |
    |    |____/ \__,_|\__\__,_| |____/ \__|_|   \__,_|\___|\__|\__,_|_|  \___||___/  |
    |                                                                                |
    |               [ Exam 2 Prep (ADT Visualizer) ]                                 |
    |           (made by ryan hawkins while procrastinating)                         |
    |                                                                                |
    |               [ Select an ADT to Visualize ]                                   |
    |                                                                                |
    |                                                                                |
    |           [ a ] Min Priority Queue / Binary Heap                               |
    |           [ b ] AVL Tree                                                       |
    |           [ c ] Dictionary / Hash Table                                        |
    |           [ q ] Quit                                                           |
    |                                                                                |
    ==================================================================================";

        private View currentUI; // which UI is currently active

        public ADTViewer() => currentUI = null;


        // call this to run the UI
        public void RenderUI()
        {
            Console.Clear();
            Console.WriteLine(MENUTEXT);

            char key = Console.ReadKey().KeyChar;

            if (key == 'q') Console.WriteLine("[ Exiting UI... ]");

            else handleKeys(key);
        }
        private void handleKeys(char c)
        {
            switch (c)
            {
                case 'a':
                    switchWindow(new QueueView());
                    break;
                case 'b':
                    switchWindow(new AVLView());
                    break;
                case 'c':
                    switchWindow(new DictView());
                    break;
                default:
                    Console.WriteLine("[ Select a valid menu option (a, b, c, q) ]");
                    break;
            }

            this.currentUI = null; // dispose
            RenderUI(); // loop UI till 'q'
        }

        // maybe can expand this later to allow for user to save the ADT while running
        // but that would require a lot of memory 
        private void switchWindow(View view)
        {
            this.currentUI = view;
            currentUI.Run();
        }


    }
}
