using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myNameSpace
{
    internal class Hello
    {
        public static void SayHello(string message)
        {
            Console.WriteLine(message);
        }
    }

    namespace child
    {
        internal class Hi
        {
            public static void SayHi(string message)
            {   
                Console.WriteLine(message);
            }
        }
    }
}
