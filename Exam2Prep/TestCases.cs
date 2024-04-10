using OurPriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;



namespace Exam2Prep
{
    public static class TestCases
    {
        public static Dict<int, int> TwoMaxes1()
        {
            Dict<int, int> a = new Dict<int, int>();
            for (int i = 0; i < 10; i++)
            {
                a.Add(i, i);
            }
            return a;
        }

        public static Dict<int, int> TwoMaxes2()
        {
            Dict<int, int> a = new Dict<int, int>();
            for (int i = 20; i > 0; i--)
            {
                a.Add(i, i);
            }
            return a;
        }

        public static Dict<int, int> TwoMaxes3()
        {
            Dict<int, int> a = new Dict<int, int>();
            a.Add(1, 1);
            return a;
        }

        public static void TwoMaxes()
        {
            var curCase = TwoMaxes1();
            Console.WriteLine(curCase);
            Console.WriteLine("Expected output 8, 9");

            await();

            curCase.TwoMaxKeys();

            await();

            curCase = TwoMaxes2();
            Console.WriteLine(curCase);
            Console.WriteLine("Expected output: 19, 20");
            await();

            curCase.TwoMaxKeys();

            curCase = TwoMaxes3();
            Console.WriteLine(curCase);
            Console.WriteLine("Expected output: 1, 0");
            await();
            curCase.TwoMaxKeys();
        }

        private static void await() => Console.ReadLine();

        public static void CountLeafPQ()
        {
            var curCase = CountLeaf1();
            curCase.PrintTree();
            WriteLine($" Leaf Nodes: ({curCase.CountLeafNodes()}");
            await();

            curCase = CountLeaf2();
            curCase.PrintTree();
            await();
            WriteLine($"Leaf Nodes: ({curCase.CountLeafNodes()}");


        }

        public static PriorQ<int, int> MakeQ(uint bounds)
        {
            var pQ = new PriorQ<int, int>();
            for (int i = 1; i <= bounds; i++)
            {
                pQ.Add(i, i);
            }
            return pQ;
        }


        public static PriorQ<int, int> CountLeaf1()
        {
            var pq = new PriorQ<int, int>();

            for (int i = 1; i <= 7; i++) pq.Add(i, i);

            return pq;
        }
        public static PriorQ<int, int> CountLeaf2()
        {
            var pq = new PriorQ<int, int>();

            for (int i = 0; i <= 22; i++) pq.Add(i, i);

            return pq;
        }

        public static void DecrPriority()
        {
            var cur = CountLeaf1();
            cur.PrintTree();
            await();
            cur.DecreasePriority(1, 6);
            Console.WriteLine("POST Method Call");
            cur.PrintTree();

            cur = CountLeaf2();
            cur.PrintTree();
            await();
            cur.DecreasePriority(16, 42);
            Console.WriteLine("POST Method Call");
            cur.PrintTree();

        }


        public static void TestAreChildren()
        {
            int left, right;

            var curCase = MakeQ(7);
            curCase.PrintTree();
            ReadLine();
            left = 6; right = 7;
            WriteLine($"Are siblings => {left}(l) & {right}(r) {curCase.AreSiblings(left, right)}");

            curCase = MakeQ(10);
            curCase.PrintTree(); ReadLine();
            left = 8; right = 9;
            WriteLine($"Are siblings => {left}(l) & {right}(r) {curCase.AreSiblings(left, right)}");






        }






    }
}
