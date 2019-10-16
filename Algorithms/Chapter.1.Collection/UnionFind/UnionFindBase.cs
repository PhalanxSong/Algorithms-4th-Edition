using System;

namespace Algorithms.Collection
{
    public abstract class UnionFindBase
    {
        protected int[] _Id;
        public int UnionCount { get; protected set; }

        public UnionFindBase(int count)
        {
            UnionCount = count;
            _Id = new int[count];
            for (int i = 0; i < count; i++)
                _Id[i] = i;
        }

        public bool IsConnected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        public abstract int Find(int p);

        public abstract void Union(int p, int q);
    }
}
