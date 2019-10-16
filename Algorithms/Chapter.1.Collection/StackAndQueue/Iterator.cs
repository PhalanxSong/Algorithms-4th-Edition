using System;

namespace Algorithms.Collection
{
    public interface Iterable<Item>
    {
        Iterator<Item> GetIterator();
        void Foreach(Action<Item> action);
    }

    public interface Iterator<Item>
    {
        bool HasNext();
        Item Next();
        void Remove();
    }
}