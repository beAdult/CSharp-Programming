using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn020
{
    internal class ForEach
    {
        public static void PintInfo(string info) =>
        Console.WriteLine($"{info,10}    task:{Task.CurrentId,3}    " + $"thread: {Thread.CurrentThread.ManagedThreadId}");

        public static async void RunTask(string s)
        {
            PintInfo($"Start {s,10}");
            await Task.Delay(1);                 // Task.Delay là một async nên có thể await, RunTask chuyển điểm gọi nó tại đây 
            PintInfo($"Finish {s,10}");
        }

        public static void ParallelFor()
        {

            string[] source = new string[] {"dat1","dat2","dat3",
                                            "dat4","dat5","dat6",
                                            "dat7","dat8","dat9"};
            // Dùng List thì khởi tạo
            // List<string> source = new List<string>();
            // source.Add("datdat");


            ParallelLoopResult result = Parallel.ForEach(
                source, RunTask
            );

            Console.WriteLine($"All task started: {result.IsCompleted}");
        }
    }
}
