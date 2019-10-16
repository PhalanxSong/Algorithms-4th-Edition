using System;

namespace Algorithms.Sort
{
    public class SortUtils
    {
        public static bool Less(IComparable a, IComparable b) { return a.CompareTo(b) < 0; }
        public static bool Less(int a, int b) { return a < b; }
        public static bool More(IComparable a, IComparable b) { return a.CompareTo(b) > 0; }
        public static bool More(int a, int b) { return a > b; }
        public static bool Equal(IComparable a, IComparable b) { return a.CompareTo(b) == 0; }
        public static bool Equal(int a, int b) { return a == b; }

        public static void Exchange(IComparable[] arr, int i, int j) { IComparable tmp = arr[i]; arr[i] = arr[j]; arr[j] = tmp; }
        public static void Exchange(int[] arr, int i, int j) { int tmp = arr[i]; arr[i] = arr[j]; arr[j] = tmp; }

        public static void Show(IComparable[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }
        public static void Show(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }

        public static bool IsSorted(IComparable[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
                if (Less(arr[i], arr[i - 1]))
                    return false;
            return true;
        }
        public static bool IsSorted(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
                if (Less(arr[i], arr[i - 1]))
                    return false;
            return true;
        }
    }
}
