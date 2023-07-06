using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn021
{
    internal class DI_Constructor
    {
        public interface IHorn
        {
            void Beep();
        }
        public class Car
        {
            IHorn horn;                                  // IHorn (Interface) là một Dependecy của Car
            public Car(IHorn horn) => this.horn = horn; // Inject từ hàm  tạo
            public void Beep() => horn.Beep();
        }

        public class HeavyHorn : IHorn
        {
            public void Beep() => Console.WriteLine("(kêu to lắm) BEEP BEEP BEEP ...");
        }

        public class LightHorn : IHorn
        {
            public void Beep() => Console.WriteLine("(kêu bé lắm) beep  bep  bep ...");
        }

        public static void Test()
        {
            Car car1 = new Car(new HeavyHorn());
            car1.Beep();                             // (kểu to lắm) BEEP BEEP BEEP ...

            Car car2 = new Car(new LightHorn());
            car2.Beep();                             // (kểu bé lắm) beep  bep  bep ...
        }
    }
}
