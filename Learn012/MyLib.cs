using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn012
{
    internal class MyLib
    {

    }

    public static class Xyz
    {
        public static double Bp(this double x) => x * x;
        public static double Can2(this double x) => Math.Sqrt(x);

        public static double Sin(this double x) => Math.Sin(x); 
    }
}
