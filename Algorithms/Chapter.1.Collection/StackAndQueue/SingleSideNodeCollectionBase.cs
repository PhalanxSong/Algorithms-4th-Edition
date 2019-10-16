using System;

namespace Algorithms.Collection
{
    public abstract class SingleSideNodeCollectionBase<Item> : IterableCollectionBase<Item>
    {
        internal abstract SingleSideNode<Item> GetFirstNode();

        public override Iterator<Item> GetIterator()
        {
            return new SingleSideNode<Item>.SingleSideNodeIterator(GetFirstNode());
        }
    }
}
