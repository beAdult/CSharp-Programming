// See https://aka.ms/new-console-template for more information

namespace Learn011
{
    /*
    // Lambda - Anonymous Function
        1. 
        (tham so) => Bieu thuc
        2. 
        (tham so) => {
            cac bieu thuc;
            return bieu thuc tra ve;
    */
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lambda Expression");
            Action<string> thongbao;
            thongbao = (string message) => Console.WriteLine(message);
            // thongbao = () => Console.WriteLine("XIn CHao Dat");
            // thongbao = (message) => Console.WriteLine(message);
            thongbao?.Invoke("Hello Dat");


            Func<int, int, int> TinhToan;

            TinhToan = (int a, int b) =>
            {
                int res = a + b;
                return res;
            };
            Console.WriteLine($"Gia tri Tinh Toan la: {TinhToan?.Invoke(10, 11)}");

            int[] array = { 1, 2, 3, 4, 5, 6, 7 };
            var neww_array = array.Select((x) =>
            {
                return x * x;
            });

            foreach( var x in neww_array )
            {
                Console.WriteLine($"{x.ToString()}");
            }

            array.ToList().ForEach( (x) => Console.WriteLine(x * x));


            var new_array = array.Where(
                (x) =>
                {
                    return x % 6 == 0;
                }
            );

            foreach( var x in new_array)
            {
                Console.WriteLine($"{x.ToString()}");
            }

        }
    }
}