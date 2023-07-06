using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn021
{
    internal class DI
    {
        public class Horn
        {
            int level; // thêm độ lớn còi xe
            public Horn(int level) => this.level = level; // thêm khởi tạo level

            public void Beep() => Console.WriteLine("Beep - beep - beep ...");
        }

        public class Car
        {
            // horn là một Dependecy của Car
            Horn horn;

            // dependency Horn được đưa vào Car qua hàm khởi tạo
            public Car(Horn horn) => this.horn = horn;

            public void Beep()
            {
                // Sử dụng Dependecy đã được Inject
                horn.Beep();
            }
        }

        public static void Test()
        {

            Horn horn = new Horn(10);
            var car = new Car(horn); // horn inject vào car
            car.Beep(); // Beep - beep - beep ...
        }
    }
}
