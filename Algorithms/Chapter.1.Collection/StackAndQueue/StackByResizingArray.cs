using System;

namespace Algorithms.Collection
{
    // 下压堆栈 (可扩容数组实现 LIFO)
    public class StackByResizingArray<Item> : IterableCollectionBase<Item>
    {
        protected Item[] _ItemArray = new Item[1];
        protected int _Count = 0;

        public int Size { get { return _Count; } }
        public int Capacity { get { return _ItemArray.Length; } }

        public bool IsEmpty() { return _Count == 0; }

        protected void Resize(int capacity)
        {
            if (capacity < _Count)
                throw new Exception("Unvalid resize, new capacity is less than current count.");

            Item[] newArray = new Item[capacity];
            for (int i = 0; i < _Count; i++)
                newArray[i] = _ItemArray[i];
            _ItemArray = newArray;
        }

        /*
         * 定容栈的扩容
         *  1.没有空间则加倍
         *  2.小于四分之一则减半
         * 实现保证栈不会溢出且使用率不小于 1/4
        */

        public StackByResizingArray<Item> Push(Item item)
        {
            if (_Count == Capacity)
                Resize(2 * Capacity);
            _ItemArray[_Count] = item;
            _Count++;
            return this;
        }

        public Item Pop()
        {
            Item item = _ItemArray[_Count];
            _ItemArray[_Count] = default(Item);
            _Count--;
            if (_Count > 0 && _Count == Capacity / 4)
                Resize(Capacity / 2);
            return item;
        }

        public override Iterator<Item> GetIterator()
        {
            return new ResizingArrayIterator(this, _Count);
        }

        protected class ResizingArrayIterator : Iterator<Item>
        {
            protected StackByResizingArray<Item> ResizingArray;
            protected int ItorCounter;

            public ResizingArrayIterator(StackByResizingArray<Item> resizingArrayStack, int count)
            {
                ResizingArray = resizingArrayStack;
                ItorCounter = count;
            }

            public bool HasNext()
            {
                return ItorCounter > 0;
            }

            public Item Next()
            {
                return ResizingArray._ItemArray[--ItorCounter];
            }

            public void Remove()
            {
            }
        }
    }
}
