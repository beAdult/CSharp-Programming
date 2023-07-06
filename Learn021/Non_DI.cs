using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn021
{
    internal class Non_DI
    {
        public class Horn
        {
            public void Beep() => Console.WriteLine("Beep - beep - beep ...");
            //public Horn(int level) => this.level = level; // thêm khởi tạo level

        }

        public class Car
        {
            public void Beep()
            {
                // chức năng Beep xây dựng có định với Horn
                // tự tạo đối tượng horn (new) và dùng nó
                Horn horn = new Horn();
                //Horn horn = new Horn(10);     // Khởi tạo với Horn với tham số level
                horn.Beep();
            }
        }

        public static void Test()
        {
            var car = new Car();
            car.Beep();         // Beep - beep - beep ...
        }
    }
}
