using System;
using Algorithms.Collection;

namespace Algorithms.Sort
{
    public class IndexMaxPQ<Item> : Iterable<int> where Item : class, IComparable
    {
        // number of elements on PQ
        public int Count { get; protected set; }

        // binary heap using 1-based indexing
        private int[] _Pq;
        // inverse of pq - qp[pq[i]] = pq[qp[i]] = i
        private int[] _Qp;
        // keys[i] = priority of i
        private Item[] _Items;

        public IndexMaxPQ(int maxN)
        {
            if (maxN < 0)
                throw new Exception();

            Count = 0;
            _Items = new Item[maxN + 1];
            _Pq = new int[maxN + 1];
            _Qp = new int[maxN + 1];
            for (int i = 0; i <= maxN; i++)
                _Qp[i] = -1;
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public bool Contains(int i)
        {
            return _Qp[i] != -1;
        }

        public void Insert(int i, Item key)
        {
            if (Contains(i))
                throw new Exception("index is already in the priority queue");

            Count++;
            _Qp[i] = Count;
            _Pq[Count] = i;
            _Items[i] = key;
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

            int max = _Pq[1];
            Exchange(1, Count--);
            Sink(1);

            _Qp[max] = -1;        // delete
            _Items[max] = null;    // to help with garbage collection
            _Pq[Count + 1] = -1;        // not needed
            return max;
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
            Swim(_Qp[i]);
            Sink(_Qp[i]);
        }

        public void IncreaseKey(int i, Item key)
        {
            if (!Contains(i)) throw new Exception("index is not in the priority queue");
            if (_Items[i].CompareTo(key) >= 0)
                throw new Exception("Calling increaseKey() with given argument would not strictly increase the key");

            _Items[i] = key;
            Swim(_Qp[i]);
        }

        public void DecreaseKey(int i, Item key)
        {
            if (!Contains(i)) throw new Exception("index is not in the priority queue");
            if (_Items[i].CompareTo(key) <= 0)
                throw new Exception("Calling decreaseKey() with given argument would not strictly decrease the key");

            _Items[i] = key;
            Sink(_Qp[i]);
        }

        public void Delete(int i)
        {
            if (!Contains(i)) throw new Exception("index is not in the priority queue");
            int index = _Qp[i];
            Exchange(index, Count--);
            Swim(index);
            Sink(index);
            _Items[i] = null;
            _Qp[i] = -1;
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
            _Qp[_Pq[i]] = i;
            _Qp[_Pq[j]] = j;
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