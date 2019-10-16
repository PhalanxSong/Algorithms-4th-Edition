using System;

namespace Algorithms.Collection
{
    public class WeightedQuickUnionUnionFind : UnionFindBase
    {
        private int[] _Rank;

        public WeightedQuickUnionUnionFind(int count) : base(count)
        {
            _Rank = new int[count];
            for (int i = 0; i < count; i++)
                _Rank[i] = 1;
        }

        public override int Find(int p)
        {
            while (p != _Id[p])
            {
                // advance.在 find 过程中 压缩路径 (path compression by halving)
                _Id[p] = _Id[_Id[p]];

                p = _Id[p];
            }
            return p;
        }

        public override void Union(int p, int q)
        {
            int pRootId = Find(p);
            int qRootId = Find(q);

            if (pRootId == qRootId)
                return;

            int smallSizeRootId, bigSizeRootId = int.MinValue;
            if (_Rank[pRootId] < _Rank[qRootId])
            {
                smallSizeRootId = pRootId;
                bigSizeRootId = qRootId;
            }
            else
            {
                smallSizeRootId = qRootId;
                bigSizeRootId = pRootId;
            }

            _Id[smallSizeRootId] = bigSizeRootId;
            _Rank[bigSizeRootId] += _Rank[smallSizeRootId];

            UnionCount--;
        }
    }
}
