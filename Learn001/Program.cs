// See https://aka.ms/new-console-template for more information
using System;

namespace Learn001
{
    // Data type: int, float, double, string, char, array, matrix, ...
    // IO: Input - Output: ReadLine(), WriteLine()
    // Operator: +, - , *, /, ++, --, >, <, >=, <=, ==, &&, ||, ...
    // Bit Operator: ^, ?, |, &, !, ~, ...
    // if, else, switch case, goto, continue, break, while, for, do-while, ...
    // Array methods: Length, getLength, Max, Min, Sum, Sort, new []
    // function: void(): parameter & reference
    class Program
    {
        static void Main(string [] args)
        {
            Console.WriteLine("Hello, World!");
            string name = Console.ReadLine();
            Console.WriteLine("Your name is: " + name);

            int a = int.Parse(Console.ReadLine());
            Console.WriteLine("Your Age is: " + a);

            float b = Convert.ToSingle(Console.ReadLine());
            Console.WriteLine("Your GPA is: " + b);


            int[] ds = new int[10];
            int len = ds.Length;
            Array.Sort(ds);
            int maxVal = ds.Max();
            int minVal = ds.Min();
            int sumVal = ds.Sum();
            string[] ds_ten = new string[2] { "NQ Dat", "Dat NQ" };

            int[,] matrix = new int[2, 3];
            int[,] matrix_1 = new int[2, 2] { { 1, 1 }, { 2, 2 } };

            int r = matrix_1.GetLength(0);
            int c = matrix_1.GetLength(1);
            int rank = matrix_1.Rank;
            Console.WriteLine("The Rank of Matrix is" + rank);

            foreach (string s in ds_ten) Console.WriteLine("All Your name is: " + s);


            // function with ref
            xinchao("Dat", "Bui");
            xinchao(ho: "Pham", ten: "Huyen Chang");
            xinchao("Dat");


            int aa = 10;
            power(aa);// tham tri, tao ra ban sao, dua vao lam tham so, khong thay doi aa ban dau
            Console.WriteLine(aa);

            power(ref aa); // tham chieu, thay doi luon aa
            Console.WriteLine(aa);

            Count cnt = new Count();
            Console.WriteLine(cnt.getC());

            cnt.increase();
            Console.WriteLine(cnt.getC());

            Dem(cnt);
            Console.WriteLine(cnt.getC());

        }

        static void xinchao(string ten, string ho = "Nguyen")
        {
            Console.WriteLine(ho + " " + ten);
        }

        static void power(int a)
        {
            a = a * a;
            Console.WriteLine(a);
        }
        static void power(ref int a)
        {  
            a = a * a;
            Console.WriteLine(a);
        }

        static void Dem(Count count)
        {
            // thao tac truc tiep tren count duoc khai bao o tren.
            count.increase();
        }
    }

    class Count
    {
        private int c = 0;

        public int getC()
        {
            return c;
        }

        public void increase()
        {
            c += 1;
        }
    }
}
