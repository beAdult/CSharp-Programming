// See https://aka.ms/new-console-template for more information

using System;
namespace Learn012
{
    public delegate void SuKienNhapSo(int i);
    class Program
    {
        static void Main(string[] args)
        {
            // Publisher -> Class phat sk
            // Subsriber -> Class nhan sk
            //ucomment form here
            Console.WriteLine("event action method with delegate");
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine();
                Console.WriteLine("exit");
            };
            UserInput us = new UserInput();
            //us.skns += (x) =>
            //{
            //    console.writeline($"so vua nhap la : {x}");
            //};
            //us.input();

            SquareRoot sqr = new SquareRoot();
            sqr.Sub(us);

            Square sqrr = new Square();
            sqrr.Sub(us);
            // if 2 is using for us -> destroy first, get last
            // else add event in class userinput of delagate
            us.Input();

            int[] array = {1, 2, 3, 4, 5, 6, 7};
            int tmx = array.Max();
            Console.WriteLine(tmx);
            // extendsion method
            string s = "hello dat";
            Print(s, ConsoleColor.Red);

            "Hello Dat".Print(ConsoleColor.Green);

            double a = 2.5;
            Console.WriteLine(a);
            Console.WriteLine(a.Can2());
            Console.WriteLine(Xyz.Bp(a));
        }
        // extendsion method
        public static void Print(string s, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(s);
        }

    }
    // Static class:
    static class ABC
    {
        //private string s;
        public static void Print(this string s, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(s);
        }
    }

    class DataInput : EventArgs
    {
        public int data { set; get; }

        public DataInput(int x)
        {
            data = x;
        }
    }

    class UserInput
    {
        // public SuKienNhapSo skns { set; get; }
        // public event SuKienNhapSo Skns; // huong su kien, khong co phep gan, ma phai la phep += -=

        public event EventHandler Skns; // ~ Delegate void Kieu(object? event, EventArgs args)
        public void Input()
        {
            do
            {
                Console.WriteLine("Nhap vao so nguen");
                string s = Console.ReadLine();
                int i = Int32.Parse(s);
                Skns?.Invoke(this, new DataInput(i));

            } while (true);
        }
    }

    class SquareRoot
    {
        public void Sub(UserInput usip)
        {
            // usip.skns = FindSQR;
            usip.Skns += FindSQR;
        }
        public void FindSQR(object sender, EventArgs args) {
            DataInput di = (DataInput)args;
            int n = di.data;
            Console.WriteLine($"Sqrt of {n} is {Math.Sqrt(n)}");
        }
    }


    class Square
    {
        public void Sub(UserInput usip)
        {
            usip.Skns += FindSquare; 
        }
        public void FindSquare(int n)
        {
            Console.WriteLine($"Square of {n} is {Math.Pow(n, 2)}");
        }
        public void FindSquare(object sender, EventArgs args)
        {
            DataInput di = (DataInput)args;
            int n = di.data;
            Console.WriteLine($"Square of {n} is {Math.Pow(n, 2)}");
        }
    }


}
