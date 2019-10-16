using System;

namespace Algorithms.Collection
{
    // 队列 (链表实现 FIFO)
    public class QueueByNode<Item> : SingleSideNodeCollectionBase<Item>
    {
        internal SingleSideNode<Item> First;
        internal SingleSideNode<Item> Last;
        protected int Count;

        public bool IsEmpty()
        {
            return First == null;
        }

        public int Size { get { return Count; } }

        public QueueByNode<Item> Enqueue(Item item)
        {
            SingleSideNode<Item> oldLast = Last;
            Last = new SingleSideNode<Item>(item, null);
            if (IsEmpty())
                First = Last;
            else
                oldLast.Next = Last;
            Count++;
            return this;
        }

        public Item Dequeue()
        {
            Item item = First.ItemInstance;
            First = First.Next;
            if (IsEmpty())
                Last = null;
            Count--;
            return item;
        }

        internal override SingleSideNode<Item> GetFirstNode()
        {
            return First;
        }
    }
}
