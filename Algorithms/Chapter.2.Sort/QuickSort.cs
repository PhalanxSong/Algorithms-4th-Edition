using System;

namespace Algorithms.Sort
{
    public class QuickSort
    {
        public static void Sort(IComparable[] a)
        {
            Sort(a, 0, a.Length - 1);
        }

        private static void Sort(IComparable[] a, int low, int high, bool use3Way = false, int insertionThreshold = 10)
        {
            //if (high <= low) return;

            // advance.对长度小于 threshold 的数组使用插入排序
            if ((high - low) < insertionThreshold)
            {
                InsertionSort.Sort(a, low, high);
                return;
            }

            // advance.三取样切分 ( 使用 a[low] a[low+1] a[low+2] 的中位数作为切分元素 )
            if (high - low >= 2)
            {
                InsertionSort.Sort(a, low, low + 2);
                SortUtils.Exchange(a, low, low + 1);
            }

            // advance.三向切分 ( 在重复元素较多的情况下的最优方式 )
            if (use3Way)
            {
                int left, right;
                Partition3Way(a, low, high, out left, out right);
                Sort(a, low, left - 1);
                Sort(a, right + 1, high);
            }
            // 基本模式
            else
            {
                int j = PartitionBasic(a, low, high);
                Sort(a, low, j - 1);
                Sort(a, j + 1, high);
            }
        }

        private static void Partition3Way(IComparable[] a, int low, int high, out int left, out int right)
        {
            IComparable v = a[low];
            left = low;
            right = high;
            int i = low + 1;

            while (i <= right)
            {
                int compare = a[i].CompareTo(v);
                if (compare < 0) SortUtils.Exchange(a, left++, i++);
                else if (compare > 0) SortUtils.Exchange(a, i, right--);
                else i++;
            }
        }

        private static int PartitionBasic(IComparable[] a, int low, int high)
        {
            // a[low] 为判断位
            IComparable v = a[low];

            // a[++left] 为左侧判断起始位
            int left = low;
            // a[--right] 为右侧判断起始位
            int right = high + 1;

            while (true)
            {
                // 从左侧扫描大于 v 的值
                while (SortUtils.Less(a[++left], v)) if (left == high) break;
                // 从右侧扫描小于 v 的值
                while (SortUtils.Less(v, a[--right])) if (right == low) break;

                if (left >= right) break;
                SortUtils.Exchange(a, left, right);
            }

            SortUtils.Exchange(a, low, right);
            return right;
        }
    }
}
