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
            WriteLine(curCase);
            WriteLine("Expected output 8, 9");

            await();

            curCase.TwoMaxKeys();

            await();

            curCase = TwoMaxes2();
            WriteLine(curCase);
            WriteLine("Expected output: 19, 20");
            await();

            curCase.TwoMaxKeys();

            curCase = TwoMaxes3();
            WriteLine(curCase);
            WriteLine("Expected output: 1, 0");
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

        public static void CountChildren()
        {
            var cur = MakeQ(8);
            cur.PrintTree();
            await();
            int of = 2;
            WriteLine($"Children Count of {of} = {cur.CountChildren(of)}");

        }

        public static void TryMerge()
        {
            var thisCur = MakeQ(8);
            Console.WriteLine("Merging into...");
            thisCur.PrintTree();
            WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Random random = new Random();
            var otherQ = new PriorQ<int, int>();
            for (int i = 0; i <= 10; i++)
            {
                int entry = random.Next(1, 28);
                otherQ.Add(entry, entry);
            }

            Console.WriteLine("Other Q");
            otherQ.PrintTree();
            WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++");

            thisCur.TryMerge(otherQ);
            thisCur.PrintTree();
            WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++");


        }
        public static void countLeaf()
        {
            for (uint i = 3; i <= 10; i++)
            {
                var cur = MakeQ(i);
                cur.PrintTree();
                WriteLine($"Leaves = {cur.CountLeaf()}");
                await();
                WriteLine("========================================");
            }
        }

        // unit test
        public static void GetKthRow()
        {
            var cases = new Dictionary<int, PriorQ<int, int>>();

            cases[0] = MakeQ(8); cases[1] = MakeQ(15); cases[2] = MakeQ(21);

            PriorQ<int, int> currentCase = new PriorQ<int, int>();
            for (int i = 0; i < 3; i++)
            {
                currentCase = cases[i];
                for (int k = 1; k <= 4; k++)
                {
                    try
                    {
                        WriteLine($"CASE {i} - ROW = {k} ");
                        currentCase.PrintTree();
                        await();

                        var output = currentCase.GetKthRow(k);
                        WriteLine("--OUTPUT--");
                        viewArr(output);
                        await();
                        WriteLine("=========================================");
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        WriteLine($"Method properly caught out of bounds ({k}) kth row");
                        WriteLine($"<< {ex.Message} >>");
                    }
                    catch (Exception ex)
                    {
                        WriteLine($"Unit test failed => {ex.Message}");
                        await();
                        WriteLine(ex.ToString());
                        await();
                        WriteLine(ex.StackTrace);
                    }
                    finally
                    {
                        await();
                        WriteLine("=========================================");
                    }

                }
            }
        }

        private static void viewArr(int[] a)
        {
            Write("{");
            foreach (int i in a)
            {
                Write($"{i}, ");
            }
            Write("}");
        }

        private static void spacing()
        {
            await();
            WriteLine("======================================================");
        }

        public static void testDeadZone()
        {
            var dict = new Dict<int, int>();
            for (int i = 0; i < 31; i++)
            {
                dict.Add(i, i);
            }
            WriteLine(dict);
            WriteLine($"\n\nLargest Deadzone = {dict.LargestDeadZone()}");
            spacing();


            dict = new Dict<int, int>(27);
            for (int i = 1; i < 10; i++)
            {
                dict.Add(i, i);
            }
            dict.Add(20, 20);
            WriteLine(dict);
            WriteLine($"\n\nLargest Deadzone = {dict.LargestDeadZone()}");
            spacing();




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

        public static void addBoth(Dict<int, int> a, Dict<int, int> b, int i)
        {
            a[i] = i;
            b[i] = i;
        }
        public static void areEq()
        {
            var a = new Dict<int, int>();
            var b = new Dict<int, int>();

            for (int i = 0; i < 10; i++)
            {
                addBoth(a, b, i);
            }
            b.Add(69, 69);
            WriteLine($"Are Equal => {a.AreEqual(b)}");

        }







    }

}
