// See https://aka.ms/new-console-template for more information
using System;
using System.Data.Common;

namespace Learn013
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Static Method, Read only Data, Overloading Operator, Indexer");
            CounterNumber.Infor();
            Console.WriteLine(CounterNumber.number);

            CounterNumber c1 = new CounterNumber(), c2 = new CounterNumber();
            c1.Count();
            c1.Count();

            Console.WriteLine(CounterNumber.number);

            Student st = new Student("NV A");
            //st.name = "ad"; // Error read only
            Console.WriteLine(st.name);

            // overloading operator
            Vector v1 = new Vector(2, 3);
            v1.Print();

            Vector v2 = new Vector(1, 1);
            v2.Print();

            Vector v3 = v1 + v2;
            v3.Print();

            Console.WriteLine("Exception in C#");
            // Exception 
            ExceptionErr();
        }

        static void ExceptionErr()
        {
            int a = 5, b = 0;
            try
            {
                var c = a / b;  // phai sinh doi tuong lop Exception
                Console.WriteLine(c);
            }catch (Exception e){
                Console.WriteLine("Err divice for zero");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.GetType().Name);
            }
            finally
            {

                Console.WriteLine("ENd");
            }

            //Phát sinh ngoại lệ với throw

            try
            {
                double z = Devide(100, 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // Tạo Exeption riêng
            try
            {
                UserInput("Đây là một chuỗi rất dài ...");
            }
            catch (DataTooLongExeption e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception otherExeption)
            {
                Console.WriteLine(otherExeption.Message);
            }

        }

        static double Devide(double x, double y)
        {

            if (y == 0)
            {
                // Khởi tạo ngoại lệ, tham số là thông báo lỗi
                Exception myexception = new Exception("Số chia không được bằng 0");

                // phát sinh ngoại lệ, code phía sau throw không được thực thi
                throw myexception;
            }
            return x / y;
        }

        public class DataTooLongExeption : Exception
        {
            const string erroMessage = "Dữ liệu quá dài";
            public DataTooLongExeption() : base(erroMessage) {
            }
        }

        public static void UserInput(string s)
        {
            if (s.Length > 10)
            {
                Exception e = new DataTooLongExeption();
                throw e;    // lỗi văng ra
            }
            //Other code - no exeption
        }
    }

    class CounterNumber
    {
        public static int number = 0;
        public static void Infor()
        {
            Console.WriteLine($"Change is: {number}");
        }
        public void Count()
        {
            // number += 1;
            CounterNumber.number += 1;
        }
    }

    class Student
    {
        public readonly string name; // chi doc.

        public Student(string _name)
        {
            this.name = _name;
        }
    }

    class Vector
    {
        double x, y;

        public Vector(double _x, double _y)
        {
            x = _x;
            y = _y;
        }

        public void Print()
        {
            Console.WriteLine($"x = {x}, y = {y}");
        }

        public static Vector operator+(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y +  b.y);

        }
        // indexer [chi so]
        public double this[int index]
        {
            get { 
                switch (index)
                {
                    case 0: return x;
                        break;
                    case 1: return y;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                        break;
                }
            }
            set {
                switch (index)
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                        break;
                }
            }
        }
    }
}
