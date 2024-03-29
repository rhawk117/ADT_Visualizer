using System;
using System.Collections.Generic;

namespace SortAlgos
{
    class MySort<T> where T : IComparable<T>
    {
        // ================================================================================
        //
        //    Insertion Algorithm
        //
        // ================================================================================
        public static void InsertionSort(List<T> aList)
        {
            int count = 0;
            Console.WriteLine("----------- Insertion Sort -------");
            PrintHeader(aList, 0, aList.Count);

            T tmp;
            int hole;
            for (int bar = 1; bar < aList.Count; bar++)
            {
                tmp = aList[bar];
                for (hole = bar; hole > 0 && tmp.CompareTo(aList[hole - 1]) < 0; hole--)
                {
                    count++;
                    aList[hole] = aList[hole - 1];
                }
                aList[hole] = tmp;
            }

            print(aList);
            Console.WriteLine("Count = {0}", count);
        }

        // ================================================================================
        //
        //    Bubble Algorithm
        //
        // ================================================================================
        public static void BubbleSort(List<T> aList)
        {
            int count = 0;
            Console.WriteLine("----------- Bubble Sort --- Green - bubbled to correct spot starting at end");
            PrintHeader(aList, 0, aList.Count);
            print(aList);

            for (int i = aList.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < aList.Count - 1; j++)
                {
                    if (aList[j].CompareTo(aList[j + 1]) > 0)
                        Swap(aList, j, j + 1);
                    count++;
                }
                printOne(aList, i, 0, i + 1);
                //print(aList, 0, i + 1);
            }

            print(aList);
            Console.WriteLine("Count = {0}", count);
        }

        // ================================================================================
        //
        //    ShellSort Algorithm
        //
        // ================================================================================        public static void ShellSort(List<T> aList)
        public static void ShellSort(List<T> aList)
        {
            int count = 0;
            Console.WriteLine();
            Console.Write("      ");
            PrintHeader(aList, 0, aList.Count);

            int hole;
            T tmp;
            for (int gap = aList.Count / 3 + 1; gap > 0; gap /= 2)	// determines increment sequence
            {
                Console.Write("{0}     ", gap);
                Print2(aList, 0, aList.Count);
                for (int next = gap; next < aList.Count; next++) // goes thru array by steps
                {
                    tmp = aList[next];
                    for (hole = next; hole >= gap && tmp.CompareTo(aList[hole - gap]) < 0; hole -= gap) // slides tmp until in place
                    {
                        ++count;
                        aList[hole] = aList[hole - gap];

                    }
                    aList[hole] = tmp;
                }
            }
            Console.WriteLine("Count = {0}", count);
        }

        // gaps must be sorted larger to smaller
        public static void ShellSort(List<T> aList, int[] gaps)
        {
            Console.Write("---- ShellSort -----");
            T tmp;
            int hole;
            int count = 0;
            Console.WriteLine();
            Console.Write("      ");
            PrintHeader(aList, 0, aList.Count);
            Console.Write("      ");
            print(aList);
            foreach (int gap in gaps)                  // determines increment sequence
            {
                Console.Write("{0}     ", gap);
                for (int next = gap; next < aList.Count; next++)
                {  		       // goes thru array by steps
                    tmp = aList[next];
                    for (hole = next; hole >= gap && tmp.CompareTo(aList[hole - gap]) < 0; hole -= gap) // slides tmp until in place
                    {
                        ++count;
                        aList[hole] = aList[hole - gap];
                    }
                    aList[hole] = tmp;
                }
                printEvery(aList, 0, aList.Count, gap);
            }
            Console.WriteLine("Number of shifts = {0}", count);
        }

        public static void ShellSort(List<T> aList, List<int> IncrementList)
        {
            T tmp;
            int hole;
            int count = 0;

            Console.Write("      ");
            PrintHeader(aList, 0, aList.Count);
            Console.Write("      ");
            Print2(aList, 0, aList.Count);

            for (int incNumber = IncrementList.Count - 1; incNumber >= 0; incNumber--)
            {
                int gap = IncrementList[incNumber];

                for (int next = gap; next < aList.Count; next++)
                {  		            // goes thru array by steps
                    tmp = aList[next];
                    for (hole = next; hole >= gap &&
                         tmp.CompareTo(aList[hole - gap]) < 0;
                         hole -= gap) // slides tmp until in place 
                    {
                        ++count;
                        aList[hole] = aList[hole - gap];
                    }
                    aList[hole] = tmp;
                    Console.WriteLine("Count = {0}", count);
                }
                Console.Write("{0,3}   ", gap);
                Print2(aList, 0, aList.Count);
            }
            Console.WriteLine("Count = {0}", count);
        }

        // ================================================================================
        //
        //    Heapsort Algorithm
        //
        // ================================================================================
        public static void Heapsort(List<T> aList)
        {
            Console.WriteLine(" --- Starting HeapSort ----");
            Heapsort(aList, aList.Count);
        }

        private static int LeftChild(int i)
        {
            return 2 * i + 1;
        }

        private static void PercDown(List<T> aList, int i, int size)
        {
            int Child;
            T Tmp;

            for (Tmp = aList[i]; LeftChild(i) < size; i = Child)
            {
                Child = LeftChild(i);
                if (Child != size - 1 && aList[Child].CompareTo(aList[Child + 1]) < 0)
                {
                    Child++;
                }
                if (Tmp.CompareTo(aList[Child]) < 0)
                {
                    aList[i] = aList[Child];
                }
                else
                {
                    break;
                }
            }
            aList[i] = Tmp;
        }

        private static void Heapsort(List<T> aList, int N)
        {
            PrintHeader(aList, 0, aList.Count);
            print(aList);

            for (int i = N / 2; i >= 0; i--) /* BuildHeap */
                PercDown(aList, i, N);
            Console.WriteLine("-- Max Heap is built --");
            print(aList);
            for (int i = N - 1; i > 0; i--)
            {
                Swap(aList, 0, i); /* DeleteMax */
                PercDown(aList, 0, i);
                print(aList);
            }
        }

        // ================================================================================
        //
        //    QuickSort Algorithm
        //
        // ================================================================================
        public static void QuickSort(List<T> aList, int stopOn = 3)
        {
            Console.WriteLine("-------- QuickSort ---------");

            QuickSort(aList, 0, aList.Count - 1, stopOn);
            Console.WriteLine("After QuickSort but before it calls InsertionSort");
            print(aList);
            Console.WriteLine();
            InsertionSort(aList);
        }

        private static T median3(List<T> aList, int left, int right)
        {
            Console.Write("\tIn median3: {0}  {1}  {2}", aList[left], aList[(left + right) / 2], aList[right]);
            int center = (left + right) / 2;
            if (aList[center].CompareTo(aList[left]) < 0)
                Swap(aList, left, center);
            if (aList[right].CompareTo(aList[left]) < 0)
                Swap(aList, left, right);
            if (aList[right].CompareTo(aList[center]) < 0)
                Swap(aList, center, right);

            Swap(aList, center, right);
            return aList[right];
        }
        private static void Swap(List<T> aList, int lhs, int rhs)
        {
            T temp = aList[lhs];
            aList[lhs] = aList[rhs];
            aList[rhs] = temp;
        }

        public static void QuickSort(List<T> aList, int left, int right, int stopOn)
        {
            if (Math.Abs(left - right) < stopOn)
                return;
            else
            {
                Console.Write("QuickSort({");
                for (int j = left; j <= right; j++)
                {
                    Console.Write("{0}", aList[j]);
                    if (j != right)
                        Console.Write(", ");
                }
                Console.Write("}");
                Console.WriteLine(", {0}, {1})", left, right);


                Console.WriteLine();
                T pivot = median3(aList, left, right);
                Console.Write("{0}", new String(' ', left * 4));
                Print2(aList, left, right + 1);
                int i = left; //, j = right;
                for (int j = right; i < j;)
                {
                    while (aList[++i].CompareTo(pivot) < 0) ;
                    while (pivot.CompareTo(aList[--j]) < 0) ;
                    if (i < j)
                        Swap(aList, i, j);
                    else
                        break;
                }
                Swap(aList, i, right);	// Move pivot back
                Console.Write("{0}", new String(' ', left * 4));
                Print3(aList, left, right + 1, i);

                QuickSort(aList, left, i - 1, stopOn);	// sort small partition
                QuickSort(aList, i + 1, right, stopOn);	// sort large partition
            }
        }

        // ================================================================================
        //
        //    Selection Sort Algorithm
        //
        // ================================================================================
        public static void SelectionSort(List<T> aList)
        {
            SelectionSort(aList, 0, aList.Count - 1);
        }

        private static void SelectionSort(List<T> aList, int low, int high)
        {
            T temp;
            for (int i = low; i <= high; ++i)
            {
                T min = aList[i];		// smallest element so far
                int min_index = i;		// index of smallest

                for (int j = i + 1; j <= high; ++j)
                {
                    if (aList[j].CompareTo(min) < 0)
                    {
                        min = aList[j];
                        min_index = j;
                    }
                }

                if (i != min_index)
                {
                    temp = aList[i];
                    aList[i] = min;
                    aList[min_index] = temp;
                }
            }
        }



        // ================================================================================
        //
        //    Merge Sort Algorithm
        //
        // ================================================================================
        public static void RecMergeSort(List<T> aList)
        {
            RecMergeSort(aList, 0, aList.Count);
        }

        private static void RecMergeSort(List<T> array, int start, int end)
        {
            if (end - start <= 1) return;
            int middle = start + (end - start) / 2;

            RecMergeSort(array, start, middle);
            RecMergeSort(array, middle, end);
            Merge(array, start, middle, end);
        }

        public static void MergeSort(List<T> aList)
        {
            print(aList);
            for (int i = 1; i <= aList.Count / 2 + 1; i *= 2)
                for (int j = i; j < aList.Count; j += 2 * i)
                    Merge(aList, j - i, j, Math.Min(j + i, aList.Count));
        }

        private static void Merge(List<T> aList, int start, int middle, int end)
        {
            List<T> merge = new List<T>(end - start); //T[end-start];

            for (int j = 0; j < end - start; ++j)
            {
                merge.Add(default(T));
            }

            int lft = 0, rght = 0, i = 0;

            while (lft < (middle - start) && rght < (end - middle))
            {
                if (aList[start + lft].CompareTo(aList[middle + rght]) < 0)
                {
                    merge[i++] = aList[start + lft++];
                }
                else
                {
                    merge[i++] = aList[middle + rght++];
                }
            }

            while (rght < end - middle)
            {
                merge[i++] = aList[middle + rght++];
            }

            while (lft < middle - start)
            {
                merge[i++] = aList[start + lft++];
            }

            for (int k = 0; k < merge.Count; k++)
            {
                aList[start++] = merge[k];
            }
        }


        // ================================================================================
        //
        //    Printing methods
        //
        // ================================================================================
        public static void PrintHeader(List<T> aList, int start, int stop)
        {
            for (int i = start; i < stop; ++i)
                Console.Write("[{0,2}]", i);
            Console.WriteLine();
        }

        public static void Print2(List<T> aList, int start, int stop)
        {
            for (int i = start; i < stop; ++i)
            {
                if (i == start || i == (start - 1 + stop) / 2)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("{0,4}", aList[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (i == (stop - 1))
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("{0,4}<==", aList[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                    Console.Write("{0,4}", aList[i]);
            }

            Console.WriteLine();
        }
        public static void Print3(List<T> aList, int start, int stop, int pivotPos)
        {
            for (int i = start; i < stop; ++i)
            {
                if (i < pivotPos)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("{0,4}", aList[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (i == pivotPos)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("{0,4}", aList[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (i > pivotPos)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("{0,4}", aList[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            Console.WriteLine();
        }

        /*
         * Black 0, DarkBlue 1, DarkGreen 2, DarkCyan 3, DarkRed 4, DarkMagenta 5, DarkYellow 6, Gray 7, DarkGray 8, 
         * Blue 9, Green 10, Cyan 11, Red 12, Magenta 13, Yellow 14, White 15  	
         */
        public static void printEvery(List<T> aList, int start, int stop, int ith)
        {
            for (int i = start; i < stop; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                int colorValue = 10 + (i % ith);
                if (colorValue == 9 || colorValue == 4 || colorValue == 5)
                    Console.ForegroundColor = ConsoleColor.White;

                Console.BackgroundColor = (ConsoleColor)colorValue;
                Console.Write("{0,4}", aList[i]);

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;

            }
            Console.WriteLine();
        }
        public static void print(List<T> aList, int start = 0, int end = -1)
        {
            if (end == -1)
                end = aList.Count;

            for (int i = start; i < end; i++)
                Console.Write("{0,4}", aList[i]);
            Console.WriteLine();
        }
        public static void printOne(List<T> aList, int pos, int start = 0, int end = -1)
        {
            if (end == -1)
                end = aList.Count;

            for (int i = start; i < end; i++)
            {
                if (pos != i)
                    Console.Write("{0,4}", aList[i]);
                else
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("{0,4}", aList[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            Console.WriteLine();
        }
    }
}


