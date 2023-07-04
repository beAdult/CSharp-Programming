// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;

namespace Learn007
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Generric class");
            int a = 56, b = 12;
            Swap(ref a, ref b);
            Console.WriteLine(a);
            Console.WriteLine(b);

            Pair<int, string> pr = new Pair<int, string> ( 1, "Dat" );
            Console.WriteLine(pr.ToString());


            Stack<int> st = new Stack<int>();
            List<string> list = new List<string>();
            Queue<string> queue = new Queue<string>();
        }

        static void Swap(ref int x, ref int y)
        {
            int z = x;
            x = y;
            y = z;
        }

        static void Swap<T> (ref T x, ref T y)
        {
            T z = x;
            x = y;
            y = z;
        }
    }

    class Pair<K, V>
    {
        private K x;
        private V y;

        public Pair (K x, V y)
        {

            this.x = x;
            this.y = y;
        }

        public Pair<K, V> getPair()
        {
            return new Pair<K, V>(x, y);
        }

        public override string ToString()
        {
            return $"Key is {x} Value is {y}";
        }
    }
}