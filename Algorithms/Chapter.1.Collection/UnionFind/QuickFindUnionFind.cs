using System;

namespace Algorithms.Collection
{
    public class QuickFindUnionFind : UnionFindBase
    {
        public QuickFindUnionFind(int count) : base(count) { }

        public override int Find(int p)
        {
            return _Id[p];
        }

        public override void Union(int p, int q)
        {
            int pId = Find(p);
            int qId = Find(q);

            if (pId == qId)
                return;

            for (int i = 0; i < UnionCount; i++)
                if (_Id[i] == pId)
                    _Id[i] = qId;

            UnionCount--;
        }
    }
}
