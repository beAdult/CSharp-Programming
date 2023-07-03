// See https://aka.ms/new-console-template for more information

using System;

namespace Learn009
{
    class Program
    {
        abstract class Product
        {
            protected double Price { set; get; }

            // public vitual void ProductInfor()


            public abstract void ProductInfor();
            
            public void Test() => ProductInfor();
        }

        class Iphone : Product
        {
            public Iphone() => Price = 5000;
            //public override void ProductInfor()
            //{
            //   Console.WriteLine("Iphone is here");
            //   base.ProductInfor();
            //}

            public override void ProductInfor()
            {
                Console.WriteLine("Iphone is here");
                Console.WriteLine($"Iphone cost: {Price}");
            }
        }
        
        int tong(int a, int b) => a + b;

        static void Main(string[] args)
        {
            
            Console.WriteLine("Virtual Method, abstract, interface");
            // virtual method : định nghĩa lại, ở lớp con.

            Iphone pr = new Iphone();
            // Product prr = new Product(); // error
            // abstract khong tao ra doi tuong, hàm không có thân hàm

            HCN h = new HCN(4, 5);
            Console.WriteLine(h.TinhChuVi() + " " + h.TinhDienTich());
        }

        // interface

        interface IHinhHoc
        {
            public double TinhChuVi();
            public double TinhDienTich();
        }

        class HCN: IHinhHoc
        {
            private double a, b;
            public double A { set; get; }
            public double B { set; get; }
            public HCN(double _a, double _b)
            {
                a = _a;
                b = _b;
            }

            public double TinhChuVi()
            {
                return (a + b) * 2;
            }
            public double TinhDienTich()
            {
                return a * b;
            }
        }

    }
}