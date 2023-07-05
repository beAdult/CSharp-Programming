// See https://aka.ms/new-console-template for more information

namespace Learn020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Parallel");
            TestFor();
            TestForAsync();
            TestForEach();
            TestInvoke();
        }

        public static void TestFor()
        {
            For.ParallelFor();
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }

        public static void TestForAsync()
        {
            ForAsync.ParallelFor();
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }

        public static void TestForEach()
        {
            ForEach.ParallelFor();
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }

        public static void TestInvoke()
        {
            Invoke.ParallelInvoke();
            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }
    }
}
