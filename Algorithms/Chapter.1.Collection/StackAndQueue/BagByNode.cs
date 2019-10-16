using System;

namespace Algorithms.Collection
{
    public class BagByNode<Item> : SingleSideNodeCollectionBase<Item>
    {
        internal SingleSideNode<Item> First;

        public BagByNode<Item> Add(Item item)
        {
            SingleSideNode<Item> oldFirst = First;
            First = new SingleSideNode<Item>(item, oldFirst);
            return this;
        }

        internal override SingleSideNode<Item> GetFirstNode()
        {
            return First;
        }
    }
}
