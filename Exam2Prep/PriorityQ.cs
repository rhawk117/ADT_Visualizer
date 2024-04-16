using System;
using System.Text;
using System.Threading.Tasks;

namespace OurPriorityQueue
{
    /// <summary> A Priority Queue with a lower value having a higher priority /// </summary>

    public class PriorQ<TPriority, TValue> where TPriority : IComparable<TPriority>
    {
        /// <summary> Private class for each cell in the array /// </summary>
        private class Cell
        {
            public TValue Value { get; set; }

            public TPriority Priority { get; set; }

            public Cell(TPriority aPriority = default, TValue aValue = default)
            {
                Priority = aPriority;
                Value = aValue;
            }
            public override string ToString() => $"{Priority}:{Value}";
        }

        /// <summary> The array where each cell is initially null /// </summary>
        private Cell[] table;

        /// <summary> Keeps track of the number of items /// </summary>
        private int count = 0;
        public int Count { get { return count; } }

        public PriorQ(int size = 100)
        {
            if (size < 10)
                size = 10;

            table = new Cell[size];
        }

        public bool IsEmpty() => (count == 0);

        public bool IsFull() => (count == table.Length - 1);

        public void Clear() => count = 0;

        /// <summary>
        /// Peek:
        /// Returns the value stored by the root (does not remove) 
        /// </summary>
        /// <returns>root cell</returns>
        public TValue Peek()
        {
            if (IsEmpty() == true)
                throw new ApplicationException(
                    "Can't Peek an empty OurPriorityQueue");

            return table[1].Value;
        }

        public TValue PeekAt(int index)
        {
            if (index < 1 || index > count)
            {
                throw new ArgumentOutOfRangeException(nameof(index),
                          "Index is out of range.");
            }
            return table[index].Value;
        }

        /// <summary>
        /// Adds a new item using its priority to help place it in the array
        /// </summary>
        /// <param name="aPriority"></param>
        /// <param name="aValue"></param>
        public void Add(TPriority aPriority, TValue aValue)
        {
            if (IsFull()) return;

            // Percolate up
            int hole = ++count;
            for (; hole > 1 && aPriority.CompareTo(table[hole / 2].Priority) < 0; hole /= 2)
            {
                table[hole] = table[hole / 2];
            }
            table[hole] = new Cell(aPriority, aValue);
        }

        /// <summary>
        /// Remove and return the highest priority data
        /// </summary>
        /// <returns></returns>
        public TValue Remove()
        {
            if (IsEmpty())
                throw new ApplicationException(
                    "Can't Remove from an empty OurPriorityQueue");

            // save the data for later
            TValue valPtr = table[1].Value;

            // put the last item in the tree in the root
            table[1] = table[count--];

            // keep moving the lowest child up until we've found the right spot 
            // for the item moved from the last level to the root
            PercolateDown(1);

            return valPtr;
        }

        /// <summary> Percolate Down:
        /// Reposition the hole down until it is 
        /// in the right spot for its priority
        /// </summary>
        /// <param name="hole"></param>
        /// 

        /// <summary>
        // Steps:
        // a. save the hole's cell in a tmp spot
        // b. keep going down the tree until the last level 
        // c. check the right and left child and put lowest one in the child variable
        // d. put lowest child in hole
        /// </summary>
        private void PercolateDown(int hole = 1)
        {
            int child;
            // a. 
            Cell pTmp = table[hole];

            // b.
            for (; hole * 2 <= count; hole = child)
            {
                child = hole * 2;
                TPriority parent = table[child].Priority;
                // if right child is less than parent
                if (child != count && table[child + 1].Priority.CompareTo(parent) < 0)
                {
                    child++;
                }
                // d.
                if (table[child].Priority.CompareTo(pTmp.Priority) < 0)
                {
                    table[hole] = table[child];
                }
                else
                {
                    break;
                }
            }
            // found right spot of hole's original value, put it back into tree
            table[hole] = pTmp;
        }

        /// <summary>
        /// i'm slow and wanted to visualize each step 
        /// </summary>
        private void percolateVisualizer(int hole, int child, TPriority pTmp)
        {
            Cell ChildC = table[child], HoleC = table[hole];
            Console.WriteLine($"[ 'Child' index = {child}|'hole' = {hole} ]");
            Console.WriteLine($"[ Child Cell = {ChildC} | Hole Cell = {HoleC} ]");
            Console.WriteLine($"[ Incr Clause => {table[child + 1].Priority} < (child) {ChildC.Priority} (child++)]");
            Console.WriteLine($"[ Re-Assign => {ChildC.Priority} < {pTmp}]");
        }


        /// <summary>
        /// BuildHeap:
        /// Assumes all but last item in array is in correct order
        /// Shifts last item in array into correct location based on priority
        /// </summary>
        public void BuildHeap()
        {
            for (int i = count / 2; i > 0; i--)
            {
                PercolateDown(i);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= count; i++)
            {
                sb.Append($"[{i}] {table[i].Priority}  | ");
            }
            return sb.ToString();
        }

        // return string with contents of array in order (e.g. left child, parent, right child)
        public StringBuilder InOrder() => inOrder();
        private StringBuilder inOrder(int position = 1)
        {
            StringBuilder str = new StringBuilder();
            if (position <= count)
            {
                str.Append(inOrder(position * 2) + "\t");
                str.Append(table[position].Value.ToString() + "\n ");
                str.Append(inOrder(position * 2 + 1) + "\t");
            }
            return str;
        }

        public void Print()
        {
            string msg = InOrder().ToString();
            Console.WriteLine(msg);
        }
        /// <summary>
        /// Add a method to the OurPriorityQueue class that decreases an entry’s
        /// priority to a new, higher priority. This method accepts two priorities 
        /// and does not accept values. Return true if it works, false otherwise. 
        /// Return false if the new priority is greater than the old priority. 
        /// Don’t dequeue and enqueue the value since this may place the new value
        /// in the wrong spot. 
        /// Hint: decreasing a priority value means it becomes
        /// higher priority and may move up in the tree. 
        /// 
        ///            
        ///                  8
        ///            ┌─────┴─────┐
        ///           31          10
        ///         ┌──┴──┐     ┌──┴──┐
        ///        40     45   26     17
        ///        
        ///     >> Make 31's priority become 4 
        ///
        ///                  4 (formally 31)
        ///            ┌─────┴─────┐
        ///            8          10
        ///         ┌──┴──┐     ┌──┴──┐
        ///        40     45   26    17
        /// </summary>

        // considerations 

        // -empty clause, greater than or eq priority clause 

        // -find item => false if not in then check if the new assigned priority would 
        // move it's position the queue => false if it wouldn't

        // re-assign moving upward 

        // test cases (given example), 1 item, not found, parent still having higher priority
        public bool IncreasePriority(TPriority search, TPriority newPriority)
        {
            // wouldn't really make sense to give a value the same priority
            if (IsEmpty() || newPriority.CompareTo(search) >= 0)
            {
                return false;
            }

            int index = findIndex(search);

            if (index == -1) // didn't find it
            {
                return false;
            }

            table[index].Priority = newPriority;
            Cell ptr = table[index];

            for (; index > 1 && newPriority.CompareTo(table[index / 2].Priority) < 0; index /= 2)
            {
                table[index] = table[index / 2];
            }
            table[index] = ptr;
            return true;
        }

        // helper method to find / determine whether or not the value in the tree
        // is in it 
        private int findIndex(TPriority search)
        {
            for (int i = 1; i <= count; i++)
            {
                if (table[i].Priority.CompareTo(search) == 0)
                {
                    return i;
                }
            }
            return -1;
        }
        // Add a method to the OurPriorityQueue class that returns
        // true if it is a valid Min Binary Heap. This means that the
        // root's priority has the smallest value in the heap and the 
        // nodes with greater values are at the bottom
        public bool IsValid()
        {
            for (int i = 1; i <= count; i++)
            {
                int left = 2 * i;
                int right = left + 1;
                if (left <= count && table[i].Priority.CompareTo(table[left].Priority) > 0)
                {
                    return false;
                }
                if (right <= count && table[i].Priority.CompareTo(table[right].Priority) > 0)
                {
                    return false;
                }
            }
            return true;
        }


        // dowell solution

        public bool inClass(TPriority oldP, TPriority newP)
        {
            if (oldP.CompareTo(newP) < 0)
            {
                return false;
            }
            int pos = -1;
            for (int i = 1; i <= count && pos == -1; i++)
            {
                if (table[i].Equals(oldP))
                {
                    pos = i;
                }
            }

            if (pos == -1) return false;

            TValue tmpValue = table[pos].Value;

            while (pos > 1 && table[pos / 2].Priority.CompareTo(newP) > 0)
            {
                table[pos].Priority = table[pos / 2].Priority;
                table[pos].Value = table[pos / 2].Value;
                pos /= 2;
            }
            table[pos].Priority = newP;
            table[pos].Value = tmpValue;
            return true;
        }

        // CountLeafNodes()
        // write test cases 
        public int CountLeafNodes()
        {
            if (count == 0)
            {
                return 0;
            }

            return countLeafNodes();
        }

        private int countLeafNodes(int pos = 1)
        {
            if (pos > count + 1)
            {
                return 0;
            }
            else if (pos * 2 > count && pos * 2 + 1 > count)
            {
                return 1;
            }
            return countLeafNodes(pos * 2) + countLeafNodes(pos * 2 + 1);
        }
        //31.	Write a method for the OurPriorityQueue class that
        //returns the kth priority in a priority queue.
        //The method is passed the kth (an int) element.

        public TPriority kthPriority(uint kth)
        {
            if (kth > count)
            {
                throw new ArgumentException();
            }
            return kthPriority(kth, 1);
        }

        private TPriority kthPriority(uint kth, int cur)
        {
            if (cur == kth)
            {
                return table[cur].Priority;
            }
            return kthPriority(kth, cur + 1);
        }

        // Write a method for the OurPriorityQueue class that
        // returns the parent of a priority that is passed
        // to the method. 

        public TPriority GetParentPriority(TPriority ChildP)
        {
            if (count == 0)
            {
                throw new Exception();
            }
            int pos = -1;
            for (int i = 2; i <= count && pos == -1; i++)
            {
                if (table[i].Priority.Equals(ChildP))
                {
                    pos = i;
                }
            }
            if (pos == -1)
            {
                throw new ArgumentOutOfRangeException();
            }
            return table[pos / 2].Priority;
        }


        // Add a method to the OurPriorityQueue class that
        // returns the Priorities of the kth row in the binary heap.
        //            (e.g)
        //             1
        //           /    \
        //          2      3
        //         / \    / \
        //        4   5  6   7
        //       / \
        //      8   9
        //
        // GetKthRow(1) => { 1 }
        // GetKthRow(2) => { 2, 3 }
        // GetKthRow(3) => { 4, 5, 6, 7 }
        // GetKthRow(4) => { 8, 9 }

        public TPriority[] GetKthRow(int kthRow)
        {
            if (kthRow <= 0)
            {
                throw new ArgumentException("kthRow must be at least 1.");
            }

            int rowIter = (int)Math.Pow(2, kthRow - 1);  // 2^(k-1)
            int bounds = (int)Math.Pow(2, kthRow) - 1;    // (2^k)    - 1

            if (rowIter > count)
            {
                throw new ArgumentOutOfRangeException(
                    $"Start of Row {kthRow} is > count"
                );
            }

            int size = Math.Min(count - rowIter + 1, rowIter);
            TPriority[] rowItems = new TPriority[size];

            int iter = 0;
            for (int i = rowIter; i <= bounds && i <= count; i++)
            {
                rowItems[iter++] = table[i].Priority;
            }

            return rowItems;
        }









        // Add a method to the OurPriorityQueue class that 
        // Decreases the Priority of a Cell in the Queue.
        // It should return true if the operation is successful

        public bool DecreasePriority(TPriority search, TPriority update)
        {
            if (count == 0 || update.CompareTo(search) < 0)
            {
                return false;
            }
            int hole = -1, child;
            for (int i = 1; i <= count && hole == -1; i++)
            {
                if (table[i].Priority.Equals(search))
                {
                    hole = i;
                }
            }

            if (hole == -1) return false;

            table[hole].Priority = update;
            Cell ptr = table[hole];
            for (; hole * 2 <= count; hole = child)
            {
                child = hole * 2;
                if (child != count && table[child + 1].Priority.CompareTo(table[child].Priority) < 0)
                {
                    child++;
                }
                else if (table[child].Priority.CompareTo(update) < 0)
                {
                    table[hole] = table[child];
                }
                else
                {
                    break;
                }
            }
            table[hole] = ptr;
            return true;
        }

        // 33.	Write a method for the OurPriorityQueue class
        // that is passed two priorities. The method returns
        // true if the two priorities are siblings (i.e., they
        // have the same parent) and false otherwise.
        // Assume there are no duplicate priorities

        public bool AreSiblings(TPriority left, TPriority right)
        {
            int r = -1, l = -1;
            for (int i = 2; i <= count && (r == -1 || l == -1); i++)
            {
                var prio = table[i].Priority;
                if (prio.Equals(right))
                {
                    r = i;
                }
                else if (prio.Equals(left))
                {
                    l = i;
                }
            }
            if (r == -1 || l == -1) return false;

            return l / 2 == r / 2;
        }

        // Write a method for the OurPriorityQueue class 
        // that takes a priority as the parameter and
        // returns the number of children the Cell with this has. 
        // It should return -1 if the priority does not exist 

        public int CountChildren(TPriority parent)
        {
            int pos = -1;
            for (int i = 1; i <= count && pos == -1; i++)
            {
                if (table[i].Priority.Equals(parent))
                {
                    pos = i;
                }
            }
            if (pos == -1)
            {
                return -1;
            }
            return countChildren(pos) - 1;
        }
        private int countChildren(int index)
        {
            if (index > count)
            {
                return 0;
            }
            return 1 + countChildren(index * 2) + countChildren(index * 2 + 1);
        }
        // Write a method for the OurPriorityQueue Class that takes the parameter of another
        // PriorityQueue and returns true if they are equal to one another. Two Priority Queues
        // are considered equal if the priorities at each index in both queues are identical to 
        // one another

        public bool areEqual(PriorQ<TPriority, TValue> other)
        {
            if (count != other.count)
            {
                return false;
            }

            int cur = 1;
            for (; cur <= count; cur++)
            {
                if (table[cur].Priority.Equals(other.table[cur].Priority) == false)
                {
                    return false;
                }
            }
            return true;
        }

        // Add a Method to the OurPriorityQueue Class that takes in the
        // value of another Priority Queue and merges it with the Priority
        // Queue calling the method. Your method must maintain the order of
        // the Queue and return True if the operation is successful.

        public bool TryMerge(PriorQ<TPriority, TValue> other)
        {
            if (other == null || other.IsEmpty() || other.Count + count > table.Length - 1)
            {
                return false;
            }

            int otherCur = 1;
            while (otherCur <= other.count)
            {
                int hole = ++count;
                TPriority newP = other.table[otherCur].Priority;
                for (; hole > 1 && newP.CompareTo(table[hole / 2].Priority) < 0; hole /= 2)
                {
                    table[hole] = table[hole / 2];
                }
                table[hole] = other.table[otherCur];
                otherCur++;
            }
            return true;
        }

        // Add a method to the OurPriorityQueue class that accepts a priority
        // and removes it from the Queue. Return true if the operation is successful
        // otherwise return false


        public int CountLeaf()
        {
            if (count == 0)
            {
                return 0;
            }
            int leaves = 0;
            for (int i = (count / 2) + 1; i <= count; i++)
                leaves++;
            return leaves;
        }






        // tree views

        public void PrintTree()
        {
            if (IsEmpty())
            {
                Console.WriteLine("The priority queue is empty.");
                return;
            }

            printAssist();
        }

        /// <summary>
        /// Recursively prints the tree.
        /// </summary>
        private void printAssist(int curIndex = 1, int indent = 0)
        {
            if (curIndex > count) return;

            int indentAmm = 4;

            string indentText = new String(' ', indent * indentAmm);

            printAssist(2 * curIndex + 1, indent + 1); // Right child

            Console.WriteLine($"{indentText}{table[curIndex]}");

            printAssist(2 * curIndex, indent + 1); // Left child
        }

        public int countChild(TPriority parent)
        {
            if (count <= 1) return 0;

            return countChild(parent, 1, false);
        }
        private int countChild(TPriority p, int cur, bool found)
        {
            if (cur > count)
            {
                return 0;
            }
            else if (!found)
            {
                return countChild(p, cur + 1, table[cur].Priority.Equals(p));
            }
            return 1 + countChild(p, cur * 2, true) + countChild(p, cur * 2 + 1, true);
        }




    }

}





