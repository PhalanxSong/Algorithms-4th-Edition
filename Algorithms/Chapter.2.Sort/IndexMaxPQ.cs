using System;
using Algorithms.Collection;

namespace Algorithms.Sort
{
    // example
    //               0  1  2  3  4  5  6  7  8  9 10
    // pq            /  3 10  6  1  4  8
    // inversed_qp   /  4     1  5     3     6     2
    // item          /  k     a  n     c     h     b
    //              a
    //            b   c
    //           k n h  

    /// <summary> 
    /// 索引优先队列 https://www.cnblogs.com/nullzx/p/6624731.html
    /// </summary>
    public class IndexMaxPQ<Item> : Iterable<int> where Item : class, IComparable
    {
        // number of elements on PQ
        public int Count { get; protected set; }

        // _Items[i] = priority of i
        private Item[] _Items;
        // binary heap using 1-based indexing
        private int[] _Pq;
        // inverse of _PQ - _QP[_PQ[i]] = _PQ[_QP[i]] = i
        private int[] _InversedPq;

        public IndexMaxPQ(int capacity)
        {
            if (capacity <= 0)
                throw new Exception();

            Count = 0;
            _Items = new Item[capacity + 1];
            _Pq = new int[capacity + 1];
            _InversedPq = new int[capacity + 1];
            for (int i = 0; i <= capacity; i++)
                _InversedPq[i] = -1;
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public bool Contains(int index)
        {
            return _InversedPq[index] != -1;
        }

        public void Insert(int itemIndex, Item item)
        {
            if (itemIndex <= 0)
                throw new Exception("index unvalid");

            if (Contains(itemIndex))
                throw new Exception("index is already in the priority queue");

            Count++;
            _InversedPq[itemIndex] = Count;
            _Pq[Count] = itemIndex;
            _Items[itemIndex] = item;
            Swim(Count);
        }

        public int MaxIndex()
        {
            if (Count == 0)
                throw new Exception("Priority queue underflow");

            return _Pq[1];
        }

        public Item MaxItem()
        {
            if (Count == 0)
                throw new Exception("Priority queue underflow");

            return _Items[_Pq[1]];
        }

        public int DeleteMax()
        {
            if (Count == 0)
                throw new Exception("Priority queue underflow");

            int maxItemIndex = _Pq[1];
            Exchange(1, Count--);
            Sink(1);

            // delete
            _InversedPq[maxItemIndex] = -1;
            // to help with garbage collection
            _Items[maxItemIndex] = null;
            // not needed
            _Pq[Count + 1] = -1;

            return maxItemIndex;
        }

        public Item ItemOf(int i)
        {
            if (!Contains(i))
                throw new Exception("index is not in the priority queue");

            return _Items[i];
        }

        public void ChangeItem(int i, Item key)
        {
            if (!Contains(i))
                throw new Exception("index is not in the priority queue");

            _Items[i] = key;
            Swim(_InversedPq[i]);
            Sink(_InversedPq[i]);
        }

        public void IncreaseItem(int i, Item key)
        {
            if (!Contains(i))
                throw new Exception("index is not in the priority queue");
            if (_Items[i].CompareTo(key) >= 0)
                throw new Exception("Calling increaseKey() with given argument would not strictly increase the key");

            _Items[i] = key;
            Swim(_InversedPq[i]);
        }

        public void DecreaseItem(int i, Item key)
        {
            if (!Contains(i))
                throw new Exception("index is not in the priority queue");
            if (_Items[i].CompareTo(key) <= 0)
                throw new Exception("Calling decreaseKey() with given argument would not strictly decrease the key");

            _Items[i] = key;
            Sink(_InversedPq[i]);
        }

        public void DeleteItem(int i)
        {
            if (!Contains(i)) throw new Exception("index is not in the priority queue");
            int index = _InversedPq[i];
            Exchange(index, Count--);
            Swim(index);
            Sink(index);
            _Items[i] = null;
            _InversedPq[i] = -1;
        }

        private bool Less(int i, int j)
        {
            return _Items[_Pq[i]].CompareTo(_Items[_Pq[j]]) < 0;
        }

        private void Exchange(int i, int j)
        {
            int swap = _Pq[i];
            _Pq[i] = _Pq[j];
            _Pq[j] = swap;
            _InversedPq[_Pq[i]] = i;
            _InversedPq[_Pq[j]] = j;
        }

        private void Swim(int k)
        {
            while (k > 1 && Less(k / 2, k))
            {
                Exchange(k, k / 2);
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= Count)
            {
                int j = 2 * k;
                if (j < Count && Less(j, j + 1)) j++;
                if (!Less(k, j)) break;
                Exchange(k, j);
                k = j;
            }
        }

        public Iterator<int> GetIterator()
        {
            return new HeapIterator(this);
        }

        public void Foreach(Action<int> action)
        {
            throw new NotImplementedException();
        }

        private class HeapIterator : Iterator<int>
        {
            // create a new pq
            private IndexMaxPQ<Item> _Copy;

            // add all elements to copy of heap
            // takes linear time since already in heap order so no keys move
            public HeapIterator(IndexMaxPQ<Item> origin)
            {
                _Copy = new IndexMaxPQ<Item>(origin._Pq.Length - 1);
                for (int i = 1; i <= origin.Count; i++)
                    _Copy.Insert(origin._Pq[i], origin._Items[origin._Pq[i]]);
            }

            public bool HasNext()
            {
                return !_Copy.IsEmpty();
            }

            public int Next()
            {
                if (!HasNext()) throw new Exception();
                return _Copy.DeleteMax();
            }

            public void Remove()
            {
                throw new NotImplementedException();
            }
        }
    }
}