using System;

namespace Algorithms.Sort
{
    public class InsertionSort
    {
        public static void Sort(IComparable[] a)
        {
            Sort(a, 0, a.Length - 1);
        }

        public static void Sort(IComparable[] a, int low, int high)
        {
            int length = high - low + 1;
            for (int i = low + 1; i < length; i++)
            {
                int minIndex = i;
                for (int j = i; j > low; j--)
                {
                    if (SortUtils.Less(a[j], a[j - 1]))
                        SortUtils.Exchange(a, j, j - 1);
                }
            }
        }
    }
}
