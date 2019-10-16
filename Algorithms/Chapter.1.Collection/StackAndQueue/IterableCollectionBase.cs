using System;

namespace Algorithms.Collection
{
    public abstract class IterableCollectionBase<Item> : Iterable<Item>
    {
        public virtual void Foreach(Action<Item> action)
        {
            Iterator<Item> itor = GetIterator();
            while (itor.HasNext())
                action(itor.Next());
        }

        public abstract Iterator<Item> GetIterator();
    }
}
