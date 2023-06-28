// See https://aka.ms/new-console-template for more information

using System;
using myNameSpace;
using myNameSpace.child;

namespace Learn006
{
    class Program
    {
        class Animal
        {
            public int Legs { get; set; }
            public float Weigh { get; set; }


            public void ShowLegs()
            {
                Console.WriteLine($"Legs: {Legs}");
            }
        }
        class Cat : Animal
        {
            public string food;      // thuộc tính mới thêm

            public Cat()
            {
                Legs = 4;           // Thuộc tính Legs có sẵn - vì nó kế thừa từ Animal
                food = "Mouse";
            }

            public void Eat()
            {
                Console.WriteLine(food);
            }
        }

        static void Main(string[] args)
        {
            // Cat cat = new Cat();
            // cat.ShowLegs();             // Phương thức này kế thừa từ lớp cơ sở
            // cat.Eat();                  // phương thức của riêng Cat

            // Legs: 4
            // Mouse

            // Keyword Seal: uninherit from Animal 
            // Như đã biết, phương thức khởi tạo chạy khi đối tượng được tạo (new), vấn đề là khi có sự kế thừa thì lưu ý: hàm khởi tạo của lớp cơ sở chạy trước, xong đến hàm khởi tạo của lớp kế thừa.
            // Tuy nhiên, khi phương thức khởi tạo lớp cơ sở có tham số, hoặc ấn định một phương thức khởi tạo của lớp cơ sở (nếu lớp cơ sở có quá tải nhiều phương thức khởi tạo), thì hàm tạo của lớp kế thừa phải chỉ định sẽ khởi chạy phương thức khởi tạo (và truyền tham số) nào của lớp cơ sở.
            // Sau phương thức tạo lớp kế thừa thấy có : base(abc) đây chính là chỉ ra hàm tạo lớp cơ sở sẽ chạy, đó là hàm có một tham số - và giá trị tham số được truyền vào.

            // B: A, C : B -> C: A
            // C c = new C();
            // a = (A)c;       // chuyển kiểu tường minh
            // a = c;          // ngầm định
            // a = new C();    // ngầm định
            // B b = c;        // ngầm định
            // c = new A();    // lỗi - không thể chuyển kiểu thuận cây kế thừa -  Lớp cha không chuyển thành con được
            // Lớp kế thừa luôn có thể cast (chuyển) về lớp cơ sở


            Hello.SayHello("Hello Dat");
            Hi.SayHi("Hi Dat");
            // key word: using static System.Math, System.Console
            //Sử dụng using chỉ thị truy cập trực tiếp các phương thức tĩnh, mà không cần viết tên lớp cú pháp nạp phương thức tĩnh của lớp có dạng using static namespace ... class.



            Products.Product product = new Products.Product("Quan", 100);
            Console.WriteLine(product.GetInfor());

        }
    }
}