using System;

namespace Algorithms.Collection
{
    internal class SingleSideNode<Item>
    {
        public Item ItemInstance;
        public SingleSideNode<Item> Next;

        public SingleSideNode() { }

        public SingleSideNode(Item item, SingleSideNode<Item> next)
        {
            ItemInstance = item;
            Next = next;
        }

        internal class SingleSideNodeIterator : Iterator<Item>
        {
            private SingleSideNode<Item> ItorCurrentNode;

            public SingleSideNodeIterator(SingleSideNode<Item> head)
            {
                ItorCurrentNode = head;
            }

            public bool HasNext()
            {
                return ItorCurrentNode.Next != null;
            }

            public Item Next()
            {
                ItorCurrentNode = ItorCurrentNode.Next;
                Item nextItem = ItorCurrentNode.ItemInstance;
                return nextItem;
            }

            public void Remove()
            {
            }
        }
    }
}
