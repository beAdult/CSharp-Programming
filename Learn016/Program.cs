// See https://aka.ms/new-console-template for more information

using System;
namespace Learn016
{
    class Program
    {
        static void DoSomething(int seconds, string message, ConsoleColor color) {
            lock (Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine("Start...");
                Console.ResetColor();
            }
            string a = "Hello Mr Dat";
            lock (a)
            {
                // khoi lenh thuc hien xog thi moi mo khoa
            }
            for (int i = 0; i < seconds; i++)
            {
                lock (Console.Out)
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine(message);
                    Console.ResetColor();
                }
                Thread.Sleep(2000);
            }

            lock(Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine("End...");
                Console.ResetColor();
            }
           
        }

        static async Task Task3()
        {
            Task t3 = new Task(
                (object obj) =>
                {
                    string nametask = (string)obj;
                    DoSomething(5, nametask, ConsoleColor.Red);
                }, "Task3"
            );
            t3.Start();
            //t3.Wait();
            await t3; // tại thời điểm await, trả về luôn, không khóa phương thức chính
            Console.WriteLine("Finished Task");
            //return t3;
        }

        static Task Task2()
        {
            Task t2 = new Task(
                (object obj) =>
                {
                    string nametask = (string)obj;
                    DoSomething(5, nametask, ConsoleColor.Red);
                }, "Task2"
            );
            t2.Start();
            t2.Wait();
            Console.WriteLine("Finished Task");
            return t2;
        }

        static async Task Task4()
        {
            // Task<string> t3 = new Task<string>(Func<string>) // () => return string
            // Task<string> t4 = new Task<string>(Func<object, string>, object) // () => return string input is object
            //https://xuanthulab.net/lap-trinh-bat-dong-bo-asynchronou-c-c-sharp-voi-bat-dong-bo-theo-mo-hinh-tac-vu.html

        }

        static async  Task<string> Task5()
        {
            // tạo biến delegate trả về kiểu string, có một tham số object
            Func<object, string> myfunc = (object thamso) => {
                // Đọc tham số (dùng kiểu động - xem kiểu động để biết chi tiết)
                dynamic ts = thamso;
                for (int i = 1; i <= 10; i++)
                {
                    //  Thread.CurrentThread.ManagedThreadId  trả về ID của thread đạng chạy
                    Console.WriteLine($"{i,5} {Thread.CurrentThread.ManagedThreadId,3} Tham số {ts.x} {ts.y}",
                        ConsoleColor.Green);
                    Thread.Sleep(500);
                }
                return $"Kết thúc Async1! {ts.x}";
            };

            Task<string> task = new Task<string>(myfunc, new { x = 11, y = 12 });
            task.Start();  // chý ý dòng này, để đảm bảo  task được kích hoạt

            await task;


            // Từ đây là code sau await (trong Async1) sẽ chỉ thi hành khi task kết thúc
            string ketqua = task.Result;       // Đọc kết quả trả về của task - không phải lo block thread gọi Async1

            Console.WriteLine(ketqua);        // In kết quả trả về của task
            return ketqua;
        }
        static void test()
        {
            Console.WriteLine("Asyschronous, Create Method with asysn & await");
            // synchronous
            DoSomething(2, "Dat", ConsoleColor.Green);
            DoSomething(3, "Hehe", ConsoleColor.Red);
            DoSomething(4, "Hellooo World", ConsoleColor.Blue);
            Console.WriteLine("Hello");

            // asynchronous 

            Task t1 = new Task(
                () =>
                    {
                        DoSomething(5, "Task2", ConsoleColor.Green);
                    }
                ); // () => {}
            Task t2 = new Task(
                (object obj) =>
                {
                    string nametask = (string)obj;
                    DoSomething(5, nametask, ConsoleColor.Red);
                }, "Task3"
            );

            t1.Start();
            t2.Start();
            DoSomething(2, "Dat", ConsoleColor.Green);
            DoSomething(3, "Hehe", ConsoleColor.Red);
            DoSomething(4, "Hellooo World", ConsoleColor.Blue);
            t2.Wait();
            t1.Wait();
            //Task.WaitAll(t2, t1);

            Task t3 = Task3();
            Task t4 = Task2();

            Console.WriteLine("Press any Key");
            Console.ReadKey();
        }
        // static void Main(string[] args) // khi chạy đồng bộ
        static async Task Main(string[] args)
        {
            // DownloadWebsite01.TestDownloadWebpage();

            /* Sử dụng Task */
            // Console.WriteLine($"{' ',5} {Thread.CurrentThread.ManagedThreadId,3} MainThread");
            // Task<string> t1 = TestAsync01.Async1("A", "B");
            // Task t2 = TestAsync01.Async2();

            // Console.WriteLine("Làm gì đó ở thread chính sau khi 2 task chạy");

            /* Chờ t1 kết thúc và đọc kết quả trả về */
            // t1.Wait();
            // String s = t1.Result;
            // TestAsync01.WriteLine(s, ConsoleColor.Red);

            // // Ngăn không cho thread chính kết thúc
            // // Nếu thread chính kết thúc mà t2 đang chạy nó sẽ bị ngắt
            // Console.ReadKey(); 


            /* Chạy thử Async */
            // var t1 = TestAsyncAwait.Async1("x", "y");
            // var t2 = TestAsyncAwait.Async2();
            // // Làm gì đó khi t1, t2 đang chạy
            // Console.WriteLine("Task1, Task2 đang chạy");


            // await t1; // chờ t1 kết thúc
            // Console.WriteLine("Làm gì đó khi t1 kết thúc");

            // await t2; // chờ t2 kết thúc


            /* Ví dụ download file */
            // string url = "https://github.com/microsoft/vscode/archive/1.48.0.tar.gz";
            // var taskdonload = DownloadAsync.DownloadFile(url);
            // //..
            // Console.WriteLine("Làm gì đó khi file đang tải");
            // //..
            // await taskdonload;
            // Console.WriteLine("Làm gì đó khi file tải xong");
        }
    }
}