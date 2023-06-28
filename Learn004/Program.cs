// See https://aka.ms/new-console-template for more information
using System;

namespace Learn004
{
    class Program
    {
        struct Product
        {
            public string id, name;
            public int price;

            public Product(string _id, string _name, int _price)
            {
                name = _name;
                id = _id;
                price = _price;
            }

            public string Infor
            {
                get
                {
                    return $"Name of Product has id: {id} is {name} cost {price}";
                }
            }
            
            public string GetInfor()
            {
                return $"Name of Product has id: {id} is {name} cost {price}";
            }
        }
        struct Product_claas
        {
            public string id, name;
            public int price;

            public Product_claas(string _id, string _name, int _price)
            {
                name = _name;
                id = _id;
                price = _price;
            }

            public string Infor
            {
                get
                {
                    return $"Name of Product has id: {id} is {name} cost {price}";
                }
            }

            public string GetInfor()
            {
                return $"Name of Product has id: {id} is {name} cost {price}";
            }
        }

        enum Space{
            Sea = 8, Ground= 9, Sky = 10
        };
        static void Main(string[] args)
        {
            Console.WriteLine("Structure in C#");

            Product product1;
            // struct is preferences
            product1.id = "SP001";
            product1.name = "Quan";
            product1.price = 100;

            Product product2 = new Product("SP003", "Ao", 23);
            product2 = product1;
            product2.name = "QUan Jean";
            Console.WriteLine(product1.GetInfor());
            Console.WriteLine(product2.GetInfor());
            Console.WriteLine(product2.Infor);
            // two struct is dependency with each other



            // class is references
            Product_claas product3 = new Product_claas("", "", 0);
            product3.id = "SP0021";
            product3.name = "Quan Kaki";
            product3.price = 2000;

            Product_claas product4 = new Product_claas("SP9932", "Quan Au", 565);
            product4 = product3;
            product4.name = "Quan AU";
            Console.WriteLine(product3.GetInfor());
            Console.WriteLine(product4.GetInfor());
            Console.WriteLine(product4.Infor);
            // two of them is the same, in one object, and change to eachother

            Space sp;
            sp = Space.Sea;
            switch (sp)
            {
                case Space.Sea:
                    Console.WriteLine("Học lực kém");
                    break;
                case Space.Ground:
                    Console.WriteLine("Học lực Kha");
                    break;
                default:
                    Console.WriteLine("Học lực TB");
                    break;

            }
        }
    }
}