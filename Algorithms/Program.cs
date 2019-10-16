using System;

namespace Algorithms
{
    public class IntItem
    {
        public int IntValue;
        public IntItem(int value) { IntValue = value; }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            TestResizingArrayStack();
        }

        private static void TestResizingArrayStack()
        {
            Collection.StackByResizingArray<IntItem> resizingArrayStack = new Collection.StackByResizingArray<IntItem>();
            resizingArrayStack
                .Push(new IntItem(1))
                .Push(new IntItem(2))
                .Push(new IntItem(3))
                .Push(new IntItem(4))
                .Push(new IntItem(5))
                .Push(new IntItem(6));
            string s = "";
            resizingArrayStack.Foreach((i) => { s += i.IntValue; });
            Console.WriteLine(s);
        }
    }
}
