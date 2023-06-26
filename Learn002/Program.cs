// See https://aka.ms/new-console-template for more information

using System;
// public thành viên có thể truy cập được bởi code bât kỳ đâu, ngoài đối tượng, không có hạn chế truy cập nào.
// private phương thức, thuộc tính, trường khai báo với private chỉ có thể truy cập, gọi bởi các dòng code cùng lớp.
// protected phương thức, thuộc tính, trường chỉ có thể truy cập, gọi bởi các dòng code cùng lớp hoặc các lớp kế thừa nó.
// internal truy cập được bởi code ở cùng assembly (file).
// protected internal truy cập được từ code assembly, hoặc lớp kế thừa nó ở assembly khác.
// private protected truy cập được code khi cùng assembly trong cùng lớp, hoặc các lớp kế thừa nó.
// override methods
// IsDispose and key word Using
namespace Learn002
{
    class Program
    {
        static void Main(string [] args)
        {
            Student std = new Student("B20DCPT053", "Nguyen Quoc Dat", 22);

            Console.WriteLine(std.ToString());
        }
    }
}
