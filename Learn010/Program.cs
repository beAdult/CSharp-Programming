// See https://aka.ms/new-console-template for more information
using System;

namespace Learn010
{
    public delegate void ShowLog(string message);
    class Program
    {
        static void Infor(string s)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(s);
            Console.ResetColor();
        }

        static void Warning(string s)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(s);
            Console.ResetColor();
        }

        static int Tong(int a, int b) => a + b;
        static int Hieu(int a, int b) => a - b;

        static void Sum(int a, int b, ShowLog log)
        {
            int res = a + b;
            log?.Invoke($"Tong Can tinh la {res}");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Delegate in C#");
            ShowLog log = null;
            // c1 
            log = Infor;
            // if(log != null)
            //      log("Xin Chao Dat NQ");
            // c2
            log?.Invoke("XinChao Dat Dat");

            log = Warning;
            log?.Invoke("Hoc ve Delegate");

            ShowLog log2 = null;
            log2 += Infor;
            log2 += Warning;
            log2 += Infor;
            log2 += Warning;
            log2 += Infor;
            log2 += Warning;
            log2?.Invoke("XinChao Dat Dat Dat Dat");

        // Action, Func: delegate - generic: 
            Action action; // delegate void Kieu();
            Action<string, int> action1; // delegate void Kieu(string s, int i)
            Action<string> action2;
            action2 = Warning;
            action2 += Infor;
            action2?.Invoke("Lai Chao Dat");

            // Kieu tra ve liệt kê cuối cùng
            Func<int> f1; // delegate int Kieu();
            Func<string, double, int> f2; // delegate int Kieu(string s, double);

            Func<int, int, int> f3 = Tong;

            int a = 10, b = 5;
            Console.WriteLine($"Ket qua la{f3?.Invoke(a, b)}");


            Sum(4, 5, Infor); // log is Infor
            Sum(4, 5, null);// not print because log is null
            Sum(4, 5, Warning); // log is Warning
        }
    }
}
