using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// namespace, partical to build in more file in one namespace.
// namespace in namespace, class in class
namespace Products
{
    internal class Product
    {
        private string name;
        private decimal price;
        public Product(string _name, decimal _price)
        {
            name = _name;
            price = _price;
        }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string GetInfor()
        {
            return $"{name} / {price}";
        }

        public class MyPro
        {
            public string Mynamepro { get; set; }
        }
    }
}
