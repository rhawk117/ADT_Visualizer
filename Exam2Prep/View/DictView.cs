using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Exam2Prep.View
{
    public class DictView : View
    {
        public Dict<int, int> dict;

        public DictView(Dict<int, int> dict = null) : base("Dictionary")
        {
            if (dict == null)
            {
                this.dict = DictCustomizer.CreateCustomDict();
            }
            else
            {
                this.dict = dict;
            }
        }

        private void error() => WriteLine("[ Returning to Main Menu... ]");

        protected override void add(int key)
        {
            try
            {
                WriteLine($"[ Adding {key} to the {type}... ]");
                dict.Add(key, key);
                enterToContinue();
                WriteLine(dict);
            }
            catch (ArgumentException)
            {
                WriteLine($"[ {key} already exists in the {type}. Dictionaries are comprised of unique keys ]");
                error();
            }
            catch (ApplicationException)
            {
                WriteLine("[ Something went horribly Wrong ]");
                error();
            }
            catch (Exception e)
            {
                WriteLine($"Unhandled -> {e.Message} <- Occured in the Hash Map");
            }
            finally
            {
                enterToContinue();
            }
        }

        protected override void doClear()
        {
            WriteLine("[ NOTE: The collision strategy will remain the same ]");
            dict.Clear();
        }

        public override void Remove()
        {
            WriteLine("[ KEYS TO REMOVE ]");
            foreach (int k in dict.GetKeys())
            {
                Write($"{k}, ");
            }
            int keyToRmve = getIntput($"\n[-] Enter a key to Remove from the {type} or q to quit: ");

            if (keyToRmve != -1)
            {
                remove(keyToRmve);
            }

            else
            {
                Run();
            }
        }



        protected override void remove(int key)
        {
            try
            {
                WriteLine($"[ Removing {key} from the {type}... ]");
                dict.Remove(key);
            }
            catch (KeyNotFoundException)
            {
                WriteLine($"[ {key} does not exist in the {type}. ]");
                error();
            }
            catch (ApplicationException)
            {
                WriteLine($"[ Something went horribly Wrong ]");
                error();
            }
        }

        protected override void ViewADT()
        {
            WriteLine(dict);
            enterToContinue();
        }
    }
    // static coupled class to help visualize the different collision resolution strategies
    public static class DictCustomizer
    {
        public static Dict<int, int> CreateCustomDict()
        {
            int size = 31;
            if (confirmAction("size"))
            {
                size = getSize();
            }
            WriteLine($"[i] Size will be {size} (default is 31)\n[ Press ENTER ]");
            ReadLine();

            Dict<int, int>.CollisionRes strategy = Dict<int, int>.CollisionRes.Double;
            if (confirmAction("Collision Strategy"))
            {
                strategy = selectCollisonStrategy();
            }
            WriteLine("[i] Collision strategy has been set, (default is double).\n[ Press ENTER ]");
            ReadLine();

            return new Dict<int, int>(size, strategy);
        }

        private static bool confirmAction(string attr)
        {
            Write($@"
          ==================={attr.ToUpper()}=====================
                [ ? ] Select a {attr} for the Dictionary [ ? ]
                           [ y (yes) \ n (no) ]
                        
                        >> Type Here: ");
            string choice = ReadLine().ToLower();
            return choice[0] == 'y';
        }
        private static int getSize()
        {
            WriteLine("[ ? ] Type a positive integer for the size that is greater than 20: ");
            string size = ReadLine();
            if (int.TryParse(size, out int s) && s >= 20)
            {
                return s;
            }
            else
            {
                WriteLine("[ ! ] Dude come on... invalid size, try again...");
                return getSize();
            }
        }
        private static Dict<int, int>.CollisionRes selectCollisonStrategy()
        {
            WriteLine(@"
            =================================================================
                        
                [ Before Continuing, Select a Collision Strategy ]
                    [ The Table Size is always a Prime Number ]
                       ( hash(key) + f(i) ) % <table_size> )                

                [ l ] Linear Probing =>   where f(i) = i++
                [ q ] Quadratic Probing =>  where f(i) = (i * i)
                [ d ] Double Hashing => where f(i) = (hash(key) + i) % (<table_size> - 1) + 1

            ==================================================================
                
            ");
            char key = ReadKey().KeyChar;
            return choiceMap(key);
        }

        private static Dict<int, int>.CollisionRes choiceMap(char key)
        {
            switch (key)
            {
                case 'l':
                    return Dict<int, int>.CollisionRes.Linear;


                case 'q':
                    return Dict<int, int>.CollisionRes.Quad;


                case 'd':
                    return Dict<int, int>.CollisionRes.Double;

                default:
                    WriteLine("[!] Select a Valid Collision strategy or type q to quit");
                    return selectCollisonStrategy();
            }
        }












    }
}
