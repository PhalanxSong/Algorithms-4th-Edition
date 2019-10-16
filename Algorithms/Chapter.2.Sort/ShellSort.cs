using System;

namespace Algorithms.Sort
{
    public class ShellSort
    {
        public static void Sort(IComparable[] a)
        {
            int length = a.Length;
            int h = 1;

            /*
             关于希尔排序 increment (增量) 的取法。
             增量increment的取法有各种方案。
             最初shell提出取increment=n/2向下取整，increment=increment/2向下取整，直到increment=1。
             但由于直到最后一步，在奇数位置的元素才会与偶数位置的元素进行比较，这样使用这个序列的效率会很低。
             后来Knuth提出取increment=n/3向下取整+1。
             还有人提出都取奇数为好，也有人提出increment互质为好。应用不同的序列会使希尔排序算法的性能有很大的差异。
             */

            // 希尔排序 选取 increment 序列 1 4 13 40 121 364 1093
            while (h < length / 3)
                h = 3 * h + 1;

            while (h > 1)
            {
                for (int i = h; i < length; i++)
                {
                    for (int j = i; j >= h; j -= h)
                    {
                        if (SortUtils.Less(a[j], a[j - h]))
                            SortUtils.Exchange(a, j, j - h);
                    }
                }

                // h = (h - 1) / 3;
                h = h / 3;
            }
        }
    }
}
