// See https://aka.ms/new-console-template for more information
using System;

namespace Learn005
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Math library");
            Console.WriteLine(Math.PI);
            Console.WriteLine(Math.E);

            Console.WriteLine(Math.Max(4, 5));
            Console.WriteLine(Math.Min(4, 10));
            Console.WriteLine(Math.Sign(3));
            Console.WriteLine(Math.Abs(-3));

            Console.WriteLine(Math.Sin(Math.PI / 4));

            Console.WriteLine(Math.Sqrt(99));
            Console.WriteLine(Math.Pow(99, 2));
            Console.WriteLine(Math.Log(33));

            // Round, Floor, Ceiling
            // Math.Round() 5.6 -> 6, 5.4 -> 5
            // Math.Floor() 5.1, 5.5, 5.7 -> 5
            // Math.Ceiling() 5.1, 5.5, 5.7 -> 6

            Console.WriteLine(Math.Truncate(5.6666));
        }

    }
}