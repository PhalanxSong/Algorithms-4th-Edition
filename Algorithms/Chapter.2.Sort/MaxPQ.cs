using System;
using Algorithms.Collection;

namespace Algorithms.Sort
{
    /// <summary> 优先队列 </summary>
    public class MaxPQ<Item> : IterableCollectionBase<Item> where Item : class, IComparable
    {
        private Item[] _HeapArr;

        public int Count { get; protected set; }
        public int Capacity { get; protected set; }

        public MaxPQ()
        {
            Capacity = 1;
            Count = 0;
            _HeapArr = new Item[Capacity + 1];
        }

        public MaxPQ(int initCapacity)
        {
            Capacity = initCapacity;
            Count = 0;
            _HeapArr = new Item[initCapacity + 1];
        }

        public MaxPQ(Item[] keys)
        {
            Capacity = keys.Length;
            Count = keys.Length;
            _HeapArr = new Item[keys.Length + 1];
            for (int i = 0; i < Count; i++)
                _HeapArr[i + 1] = keys[i];
            for (int k = Count / 2; k >= 1; k--)
                Sink(k);
            if (!IsMaxHeap())
                throw new Exception("Ctor not max heap.");
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public Item Max()
        {
            if (IsEmpty())
                throw new Exception("Priority queue underflow.");
            return _HeapArr[1];
        }

        private void Resize(int capacity)
        {
            if (capacity <= Count)
                throw new Exception("New capacity smaller than n.");

            Capacity = capacity;
            Item[] temp = new Item[capacity + 1];
            for (int i = 1; i <= Count; i++)
                temp[i] = _HeapArr[i];
            _HeapArr = temp;
        }

        public void Insert(Item item)
        {
            // double size of array if necessary
            if (Count == Capacity)
                Resize(2 * Capacity);

            // add x, and percolate it up to maintain heap invariant
            _HeapArr[++Count] = item;
            Swim(Count);
        }

        public Item DeleteMax()
        {
            if (IsEmpty())
                throw new Exception("Priority queue underflow");

            Item max = _HeapArr[1];
            SortUtils.Exchange(_HeapArr, 1, Count);
            _HeapArr[Count--] = default(Item);

            Sink(1);

            if ((Count > 0) && (Count == Capacity / 4))
                Resize(Capacity / 2);
            return max;
        }

        // 由下至上的堆有序化
        private void Swim(int index)
        {
            while (index > 1)
            {
                if (!SortUtils.Less(_HeapArr[index / 2], _HeapArr[index]))
                    break;
                SortUtils.Exchange(_HeapArr, index, index / 2);
                index /= 2;
            }
        }

        // 由上至下的堆有序化
        private void Sink(int index)
        {
            while (2 * index <= Count)
            {
                int j = 2 * index;
                // 比较 left right 的大小
                if (j < Count && SortUtils.Less(_HeapArr[j], _HeapArr[j + 1]))
                    j++;
                // 比较 root 与 较大子节点 的大小
                if (!SortUtils.Less(_HeapArr[index], _HeapArr[j]))
                    break;
                SortUtils.Exchange(_HeapArr, index, j);
                index = j;
            }
        }

        private bool IsMaxHeap()
        {
            for (int i = 1; i <= Count; i++)
                if (_HeapArr[i] == null)
                    return false;
            for (int i = Count + 1; i <= _HeapArr.Length; i++)
                if (_HeapArr[i] != null)
                    return false;
            if (_HeapArr[0] != null)
                return false;
            return IsMaxHeapOrdered(1);
        }

        private bool IsMaxHeapOrdered(int k)
        {
            if (k > Count)
                return true;

            int left = 2 * k;
            int right = 2 * k + 1;
            if (left <= Count && SortUtils.Less(_HeapArr[k], _HeapArr[left]))
                return false;
            if (right <= Count && SortUtils.Less(_HeapArr[k], _HeapArr[right]))
                return false;

            return IsMaxHeapOrdered(left) && IsMaxHeapOrdered(right);
        }

        public override Iterator<Item> GetIterator()
        {
            return new HeapIterator(this);
        }

        private class HeapIterator : Iterator<Item>
        {
            // create a new pq
            private MaxPQ<Item> _Copy;

            // add all items to copy of heap
            // takes linear time since already in heap order so no keys move
            public HeapIterator(MaxPQ<Item> origin)
            {
                _Copy = new MaxPQ<Item>(origin.Count);
                for (int i = 1; i <= origin.Count; i++)
                    _Copy.Insert(origin._HeapArr[i]);
            }

            public bool HasNext()
            {
                return !_Copy.IsEmpty();
            }

            public Item Next()
            {
                if (!HasNext())
                    throw new Exception("No next");
                return _Copy.DeleteMax();
            }

            public void Remove()
            {
                throw new NotImplementedException();
            }
        }
    }
}