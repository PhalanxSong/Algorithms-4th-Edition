using System;

namespace Algorithms.Sort
{
    public class SelectionSort
    {
        public static void Sort(IComparable[] a)
        {
            int length = a.Length;
            for (int i = 0; i < length; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < length; j++)
                {
                    if (SortUtils.Less(a[j], a[minIndex]))
                        minIndex = j;
                }
                SortUtils.Exchange(a, i, minIndex);
            }
        }
    }
}
