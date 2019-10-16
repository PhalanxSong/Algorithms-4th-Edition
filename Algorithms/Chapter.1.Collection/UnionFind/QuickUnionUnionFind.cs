using System;

namespace Algorithms.Collection
{
    public class QuickUnionUnionFind : UnionFindBase
    {
        public QuickUnionUnionFind(int count) : base(count) { }

        public override int Find(int p)
        {
            while (p != _Id[p]) p = _Id[p];
            return p;
        }

        public override void Union(int p, int q)
        {
            int pRootId = Find(p);
            int qRootId = Find(q);

            if (pRootId == qRootId)
                return;

            _Id[pRootId] = qRootId;

            UnionCount--;
        }
    }
}
