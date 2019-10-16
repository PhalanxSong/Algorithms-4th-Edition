using System;

namespace Algorithms.Sort
{
    public class MergeSort
    {
        // O O O O O O O O
        // O O
        //     O O
        // O O O O
        //         O O
        //             O O
        //         O O O O
        // O O O O O O O O   
        public static void SortFromTopToBottom(IComparable[] a)
        {
            IComparable[] aux = new IComparable[a.Length];
            SortFromTopToBottomRecursive(a, aux, 0, a.Length - 1);
        }

        private static void SortFromTopToBottomRecursive(IComparable[] a, IComparable[] aux, int low, int high)
        {
            if (low >= high) return;
            int mid = low + (high - low) / 2;

            // advance.1.对长度小于 15 的子序列直接插入排序
            if (high - low < 15)
            {
                InsertionSort.Sort(a, low, high);
            }
            else
            {
                SortFromTopToBottomRecursive(a, aux, low, mid);
                SortFromTopToBottomRecursive(a, aux, mid + 1, high);

                // advance.2.判断是否需要执行 Merge
                if (SortUtils.Less(a[mid + 1], a[mid]))
                    Merge(a, aux, low, mid, high);
            }
        }

        // O O O O O O O O
        // O O
        //     O O
        //         O O
        //             O O
        // O O O O
        //         O O O O
        // O O O O O O O O    
        public static void SortFromBottomToTop(IComparable[] a)
        {
            int length = a.Length;
            IComparable[] aux = new IComparable[length];

            for (int size = 1; size < length; size *= 2) // 1 2 4 8 16
                for (int low = 0; low < length - size; low += size * 2)
                    Merge(a, aux, low, low + size - 1, Math.Min(low + 2 * size - 1, length - 1));
        }

        private static void Merge(IComparable[] a, IComparable[] aux, int low, int mid, int high)
        {
            int i = low;
            int j = mid + 1;

            // 将 a[low...high] 复制到 aux[low...high]
            for (int k = low; k <= high; k++)
                aux[k] = a[k];

            // 归并到 a[low...high]
            for (int k = low; k <= high; k++)
            {
                if (i > mid) a[k] = aux[j++];
                else if (j > high) a[k] = aux[i++];
                else if (SortUtils.Less(aux[j], aux[i])) a[k] = aux[j++];
                else a[k] = aux[i++];
            }
        }
    }
}
