using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exam2Prep
{
    /// <summary>
    /// A Dictionary or Associative Array class
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public class Dict<TKey, TVal> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// StatusType: Initially, a cell will be empty. If a so as to act as a placeholder
        /// If a Cell is in Use it is Active
        /// </summary>

        public enum StatusType
        {
            Empty, // Cell is Empty and has not been used yet
            Active, // Cell is Actively being used
            Deleted // When a cell has been used but is now "empty", then it is Deleted 
        };

        /// <summary>
        /// Private array cell that holds the status, data, and key value of the data
        /// </summary>
        /// <remarks>The key value is seperate from the data</remarks>
        private class Cell
        {
            public StatusType Status { get; set; } // Active, Empty, Deleted
            public TVal Value { get; set; }
            public TKey Key { get; set; }

            public Cell(TKey aKey = default(TKey), TVal aValue = default(TVal), StatusType aStatus = StatusType.Empty)
            {
                this.Key = aKey;
                this.Value = aValue;
                this.Status = aStatus;
            }

            public override string ToString() => $"| {Key} : {Value} |";

        }
        /// <summary>
        /// The user of the class in the constructor can pick the 
        /// "Collision Resolution" strategy. 
        /// The choices are Linear, Quad, Double (e.g. Double Hashing)
        /// </summary>
        public enum CollisionRes
        {
            Linear,
            Quad,
            Double
        };

        private Cell[] table; /// <summary> The hash table is an array of Cells (see above).</summary>

        private readonly CollisionRes _Strategy;
        public override string ToString()
        {
            StringBuilder hashString = new StringBuilder();
            int i = 0;
            hashString.Append("\t\t   [KEY]-[VAL]\n");
            foreach (Cell pos in table)
            {
                string original = $"[ POSITION #{i} {pos} ] STATUS = ";

                if (pos == null)
                {
                    original += "< empty >";
                }
                else if (pos.Status == StatusType.Deleted)
                {
                    original += "< deleted >";
                }
                else
                {
                    original += "< active >";
                }
                hashString.AppendLine(original);
                i++;
            }
            return hashString.ToString();
        }

        /// <summary>
        /// The constructor to create an empty dictionary
        /// </summary>
        /// <param name="size"></param>
        /// <param name="aCollisionStrategy">
        public Dict(int size = 31, CollisionRes aCollisionStrategy = CollisionRes.Double)
        {
            table = new Cell[NextPrime(size)];
            _Strategy = aCollisionStrategy;
        }

        /// <summary> Empties the table </summary>
        public void Clear()
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    table[i].Status = StatusType.Deleted; // mark as deleted
                }
            }
        }


        /// <summary>
        /// Given a key and a counter, it returns the 
        /// associated index of the key in the table
        /// </summary>
        /// <param name="count"></param>
        /// <param name="aKey"></param>

        private int KeyToIndex(int count, TKey aKey)
        {
            return (
                Math.Abs(aKey.GetHashCode()) + f(count, aKey)
                ) % table.Length;
            // hash = hash(key) + f(i) % T_Size
        }

        /// <summary> Allows the use of [] like an array on class instance </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public TVal this[TKey aKey]
        {
            get => Find(aKey);
            set
            {
                // find empty cell (e.g. cell is null, status empty or deleted)
                int count = 0;
                int index = KeyToIndex(count, aKey);
                while (table[index] != null && table[index].Status.Equals(StatusType.Deleted) == false)
                {
                    count++;
                    if (count == table.Length) // in case table is full, kicks out of inf loop
                    {
                        throw new ApplicationException("[ ERROR ]: Table is full");
                    }
                    index = KeyToIndex(count, aKey);
                    Console.WriteLine($"[ Collision Occured trying Index => {index} ]");
                }
                Cell Item = table[index];
                // table slot is empty (e.g. never been used), make new cell
                if (Item == null)
                {
                    table[index] = new Cell(aKey, value, StatusType.Active);
                }

                // duplicate key found, re assign 
                else if (Item.Key.Equals(aKey) && Item.Status == StatusType.Active)
                {
                    table[index].Value = value;
                }

                // previously used item, reuse it
                else if (Item.Status == StatusType.Deleted)
                {
                    table[index].Value = value;
                    table[index].Key = aKey;
                    table[index].Status = StatusType.Active;
                }
                else
                {
                    throw new ApplicationException("[ ERROR ]: Something went wrong in Dictionary's [] set");
                }
            }
        }

        /// <summary>
        /// Returns the data associated with the key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns>data item</returns>
        public TVal Find(TKey aKey)
        {
            // search until found or empty 
            int count = 0;
            int index = KeyToIndex(count, aKey);
            while (table[index] != null &&
                   table[index].Status != StatusType.Deleted &&
                   table[index].Key.Equals(aKey) == false
            )
            {
                count++;
                if (count == table.Length) // in case the table is full, kicks out of inf loop
                {
                    throw new ApplicationException("[ ERROR ]: Table is full");
                }
                index = KeyToIndex(count, aKey);
            }

            // We can't find the key
            if (table[index] == null)
            {
                throw new KeyNotFoundException($"[ ERROR ]: The key {aKey} was not found");
            }
            // We found the key
            else if (table[index].Status == StatusType.Active && table[index].Key.Equals(aKey) == true)
            {
                return table[index].Value;
            }
            // Something went [horribly] wrong
            else
            {
                throw new ApplicationException("[ ERROR ]: Something went horribly wrong in Find method ???");
            }
        }

        /// <summary>
        /// Adds a new key value and data pair using the key value 
        /// </summary>
        /// <param name="aKey"></param>
        /// <param name="aValue"></param>
        public void Add(TKey aKey, TVal aValue)
        {
            // find empty cell (e.g. cell is null, status empty or deleted)
            int count = 0;
            int index = KeyToIndex(count, aKey);

            while (table[index] != null &&
                   table[index].Status.Equals(StatusType.Deleted) == false)
            {
                count++;
                if (count == table.Length) // in case table is full, kicks out of inf loop
                {
                    throw new ApplicationException("Table is full");
                }
                index = KeyToIndex(count, aKey);
                Console.WriteLine($"[ Collision Occured trying Index => {index} ]");

            }

            // table slot is empty (e.g. never been used)
            if (table[index] == null)
            {
                table[index] = new Cell(aKey, aValue, StatusType.Active);
            }

            // duplicate key found
            else if (table[index].Key.Equals(aKey) && table[index].Status == StatusType.Active)
            {
                throw new ArgumentException("Dictionary Error: Don't add duplicate keys: " + aKey.ToString());
            }

            // previously used item, reuse the cell
            else if (table[index].Status == StatusType.Deleted)
            {
                table[index].Value = aValue;
                table[index].Key = aKey;
                table[index].Status = StatusType.Active;
            }
            else
            {
                throw new ApplicationException("Something went wrong in HashTable Add method ???");
            }
        }

        /// <summary>
        /// Renamed from f to CollisionFactor
        /// Calculates the offset for handling collisions for 
        /// the various 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="aKey"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        private int f(int i, TKey aKey)
        {
            if (i == 0)
            {
                return 0;
            }
            else
            {
                if (_Strategy == CollisionRes.Linear)
                {
                    return i++;
                }

                else if (_Strategy == CollisionRes.Quad)
                {
                    return i * i;
                }

                else if (_Strategy == CollisionRes.Double)
                {
                    return (Math.Abs(aKey.GetHashCode()) + i) % (table.Length - 1) + 1;
                }

                else
                {
                    throw new ApplicationException("Unknown Collision Startegy in OurHashTable)");
                }
            }
        }

        public void Remove(TKey aKey)
        {
            //int index = Search(aKey, IsDeletedOrFound);
            int count = 0;
            int index = KeyToIndex(count, aKey);

            while (table[index] != null &&
                   table[index].Status != StatusType.Deleted &&
                   table[index].Key.Equals(aKey) == false)
            {
                count++;
                if (count == table.Length) // in case table is full, kicks out of inf loop
                {
                    throw new ApplicationException("Table is full");
                }
                index = KeyToIndex(count, aKey);
            }
            // Search will keep looking until found or empty cell
            if (table[index] == null)
            {
                throw new KeyNotFoundException($"Can't delete missing key: {aKey}");
            }

            else if (table[index].Status == StatusType.Active &&
                     table[index].Key.Equals(aKey) == true)
            {
                table[index].Status = StatusType.Deleted; // found it, make the cell deleted
            }

            else
            {
                throw new ApplicationException("Something went horribly wrong in HashTableO Find method ???");
            }
        }
        private static bool IsPrime(int n)
        {
            if (n == 2 || n == 3)
            {
                return true;
            }

            if (n == 1 || n % 2 == 0)
            {
                return false;
            }

            for (int i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static int NextPrime(int n)
        {
            if (n % 2 == 0) n++;


            for (; !IsPrime(n); n += 2)
                ;

            return n;
        }

        // Add a method to the OurDictionary class that returns the length of biggest cluster 
        // of active cells in the table.A cluster may wrap around from the end of the table to 
        // the beginning of the table --> this would be considered one cluster.Make sure your 
        // method accounts for a cluster that may wrap from the end of the array to the beginning.

        // circular logic 
        private void Increment(ref int n)
        {
            if (++n == table.Length)
            {
                n = 0;
            }
        }

        // recursive helper method
        private int getClusterSize(int start, int index)
        {
            if (table[index] == null || table[index].Status != StatusType.Active)
            {
                return 0;
            }

            Increment(ref index); // wrap around 

            if (index == start) // dictionary is full (infinite loop clause)
            {
                return 1;
            }

            return 1 + getClusterSize(start, index);
        }
        public int biggestCluster()
        {
            int Best = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == null || table[i].Status != StatusType.Active)
                    continue;

                int clsterSize = getClusterSize(i, i);
                Best = Math.Max(Best, clsterSize); // update largest cluster
                i += clsterSize - 1; // -1 for the increment in loop, ensures i stays on track
            }
            return Best;
        }

        // (recursive version)
        public int R_biggestCluster() => R_biggestCluster(0, 0);
        private int R_biggestCluster(int index, int biggest)
        {
            if (index >= table.Length) // base case
            {
                return biggest;
            }
            else if (table[index] == null || table[index].Status != StatusType.Active)
            {
                return R_biggestCluster(index + 1, biggest);
            }
            else
            {
                int size = getClusterSize(index, index); // cluster size
                return R_biggestCluster(index + size, Math.Max(biggest, size));
            }
        }


        public void TwoMaxKeys()
        {
            TKey absMax = default, scndMax = default;
            bool haveItem = false;

            foreach (var item in table)
            {
                if (item == null || item.Status != StatusType.Active)
                {
                    continue;
                }
                else if (!haveItem)
                {
                    haveItem = true;
                    absMax = item.Key;
                }
                else if (item.Key.CompareTo(absMax) > 0)
                {
                    scndMax = absMax;
                    absMax = item.Key;
                }
                else if (item.Key.CompareTo(scndMax) > 0)
                {
                    scndMax = item.Key;
                }
            }
            if (haveItem)
            {
                Console.WriteLine($"First: {absMax} Second: {scndMax}");
            }
            else Console.WriteLine("No items were found...");
        }

        // Add a method called 'TryGetKey' to the OurDictionary class that accepts an item and, 
        // if the item is in the table, returns the item’s key value thru the second argument
        // with the 'out' key word.

        // Keep in mind that the key value is not necessarily the position in the array.

        public bool TryGetKey(TVal search, out TKey item)
        {
            item = default;
            foreach (var cell in table)
            {
                if (cell == null || cell.Status != StatusType.Active)
                {
                    continue;
                }
                else if (cell.Value.Equals(search))
                {
                    item = cell.Key;
                    return true;
                }
            }
            return false;
        }

        // Add a method to the OurDictionary class that returns the total
        // number of hash collisions that have occured when
        // the hash function is hash(i) = hash(key) + f(i) % < size >
        // Keep in mind collisions occur when more than one key share a hash
        // code, ensure your method operates in linear time or O(n).

        public int CountCollisions()
        {
            int total = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == null || table[i].Status != StatusType.Active)
                {
                    continue;
                }

                int hash = Math.Abs(table[i].Key.GetHashCode() + f(0, table[i].Key)) % table.Length;

                if (hash != i)
                {
                    total++;
                }
            }
            return total;
        }
        // Write a method for the OurDictionary class that returns
        // the hash with the most collisions in the table.
        public TKey MostCollisions()
        {
            int[] collisions = new int[table.Length];
            int max = 0;
            TKey maxCollisionKey = default;

            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == null || table[i].Status != StatusType.Active)
                {
                    continue;
                }

                int hash = Math.Abs(table[i].Key.GetHashCode() + f(0, table[i].Key)) % table.Length;
                collisions[hash]++;

                if (collisions[hash] > max)
                {
                    max = collisions[hash];
                    maxCollisionKey = table[i].Key;
                }
            }

            return maxCollisionKey;
        }
        private int countCollisions(TKey aKey, int hash)
        {
            int count = 0;
            while (table[hash] != null && table[hash].Status == StatusType.Active)
            {
                if (count == table.Length)
                {
                    return count;
                }
                hash = Math.Abs(aKey.GetHashCode() + f(count, aKey)) % table.Length;
                count++;
            }
            return count;
        }

        public List<TKey> GetKeys()
        {
            List<TKey> keys = new List<TKey>();
            foreach (var cell in table)
            {
                if (cell == null || cell.Status != StatusType.Active)
                {
                    continue;
                }
                keys.Add(cell.Key);
            }
            return keys;
        }

        // Write a method for the OurDictionary Class that takes the value of
        // a key a returns true if another cell in the Dictionary has a key that
        // hashes to the same value as the parameter.
        // Keep in mind the key may not be in the table and to exclude 

        public bool HasMultipleHashes(TKey key)
        {
            int hashCode = origHash(key);
            bool keyExists = false;
            int count = 0;
            foreach (Cell kv in table)
            {
                if (kv == null || kv.Status != StatusType.Active)
                {
                    continue;
                }

                int kvHash = origHash(kv.Key);

                if (kv.Key.Equals(key))
                {
                    keyExists = true;
                }

                if (kvHash == hashCode)
                {
                    count++;
                }

                if (keyExists && count > 1)
                {
                    return true;
                }
            }
            return true;
        }
        private int origHash(TKey key)
        {
            return Math.Abs(key.GetHashCode() + f(0, key)) % table.Length;
        }




    }
}

