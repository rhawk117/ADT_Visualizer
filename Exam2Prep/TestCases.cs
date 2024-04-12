using OurPriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using NUnit.Framework;


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
            WriteLine("POST Method Call");
            cur.PrintTree();

            cur = CountLeaf2();
            cur.PrintTree();
            await();
            cur.DecreasePriority(16, 42);
            WriteLine("POST Method Call");
            cur.PrintTree();
        }


        public static void TestAreChildren()
        {
            int left, right;

            PriorQ<int, int> curCase = MakeQ(7);
            curCase.PrintTree();
            ReadLine();
            left = 6; right = 7;
            WriteLine($"Are siblings => {left}(l) & {right}(r) {curCase.AreSiblings(left, right)}");

            curCase = MakeQ(10);
            curCase.PrintTree(); ReadLine();
            left = 8; right = 9;
            WriteLine($"Are siblings => {left}(l) & {right}(r) {curCase.AreSiblings(left, right)}");


            curCase = MakeQ(11);
            left = 1; right = 11;
            curCase.PrintTree(); ReadLine();
            WriteLine($"Are siblings => {left}(l) & {right}(r) {curCase.AreSiblings(left, right)}");
        }

        public static void areEq()
        {
            AVL<int> a = new AVL<int>();
            AVL<int> b = new AVL<int>();
            addBoth(a, b, 100);
            addBoth(a, b, 75);
            addBoth(a, b, 200);
            addBoth(a, b, 45);
            //a.Insert(26);

            Write($"Are Equal => {a.areEqual(b)}");
        }

        private static void addBoth(AVL<int> a, AVL<int> b, int val)
        {
            a.Insert(val);
            b.Insert(val);
        }




    }
    public static class Dict
    {
        [Test]
        public static void TestHashWithMostCollisions_EmptyTable()
        {
            var dict = new Dict<int, string>(10);
            Assert.Throws<KeyNotFoundException>(() => dict.MostCollisions());
        }

        [Test]
        public static void TestHashWithMostCollisions_SingleItem()
        {
            var dict = new Dict<int, string>(10);
            dict.Add(1, "one");
            Assert.Equals(1, dict.MostCollisions());
        }

        [Test]
        public static void TestHashWithMostCollisions_MultipleItems()
        {
            var dict = new Dict<int, string>(10);
            dict.Add(1, "one");
            dict.Add(11, "eleven");
            dict.Add(21, "twenty-one");
            dict.Add(31, "thirty-one");
            dict.Add(41, "forty-one");
            dict.Add(51, "fifty-one");

            // All of these items should hash to the same value
        }


        public static void TestHashWithMostCollisions_DifferentCollisionResolution()
        {
            var dict = new Dict<int, string>(10, Dict<int, string>.CollisionRes.Linear);
            dict.Add(1, "one");
            dict.Add(11, "eleven");
            dict.Add(21, "twenty-one");
            dict.Add(31, "thirty-one");
            dict.Add(41, "forty-one");
            dict.Add(51, "fifty-one");

        }
    }

}
