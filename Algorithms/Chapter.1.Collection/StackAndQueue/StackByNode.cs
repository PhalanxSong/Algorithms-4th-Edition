using System;

namespace Algorithms.Collection
{
    // 下压堆栈 (链表实现)
    public class StackByNode<Item> : SingleSideNodeCollectionBase<Item>
    {
        internal SingleSideNode<Item> Head;
        protected int _Count;

        public bool IsEmpty()
        {
            return Head == null;
        }

        public int Size { get { return _Count; } }

        public StackByNode<Item> Push(Item item)
        {
            SingleSideNode<Item> oldHead = Head;
            Head = new SingleSideNode<Item>(item, oldHead);
            _Count++;
            return this;
        }

        public Item Pop()
        {
            Item item = Head.ItemInstance;
            Head = Head.Next;
            _Count--;
            return item;
        }

        internal override SingleSideNode<Item> GetFirstNode()
        {
            return Head;
        }
    }
}
