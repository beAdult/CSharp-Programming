using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn020
{
    internal class Invoke
    {
        //In thông tin, Task ID và thread ID đang chạy
        public static void PintInfo(string info) =>
        Console.WriteLine($"{info,10}    task:{Task.CurrentId,3}    " + $"thread: {Thread.CurrentThread.ManagedThreadId}");

        public static async void RunTask(string s)
        {
            PintInfo($"Start {s,10}");
            await Task.Delay(1);
            PintInfo($"Finish {s,10}");
        }

        public static void actionA()
        {
            PintInfo($"Finish {"ActionA",10}");
        }

        public static void actionB()
        {
            PintInfo($"Finish {"ActionB",10}");
        }


        public static void ParallelInvoke()
        {
            Action action1 = () => {
                RunTask("Action1");
            };

            Parallel.Invoke(action1, actionA, actionB);
        }
    }
}
