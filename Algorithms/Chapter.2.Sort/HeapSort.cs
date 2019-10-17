using System;

namespace Algorithms.Sort
{
    public class HeapSort
    {
        public static void Sort(IComparable[] a)
        {
            int count = a.Length;

            for (int i = count / 2; i >= 1; i--)
            {
                Sink(a, i, count);
            }

            while (count > 1)
            {
                Exchange(a, 1, count--);
                Sink(a, 1, count);
            }
        }

        private static void Sink(IComparable[] a, int k, int n)
        {
            while (2 * k <= n)
            {
                int j = 2 * k;
                if (j < n && Less(a, j, j + 1)) j++;
                if (!Less(a, k, j)) break;
                Exchange(a, k, j);
                k = j;
            }
        }

        private static bool Less(IComparable[] a, int i, int j)
        {
            return a[i - 1].CompareTo(a[j - 1]) < 0;
        }

        private static void Exchange(IComparable[] a, int i, int j)
        {
            IComparable swap = a[i - 1];
            a[i - 1] = a[j - 1];
            a[j - 1] = swap;
        }
    }
}


