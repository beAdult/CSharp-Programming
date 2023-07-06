// See https://aka.ms/new-console-template for more information

using System;
using System.Text;

namespace Learn021{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dependence Injection");
            Console.WriteLine("*******************************");
            // example about dependence injection: 
            Example.Test();

            // Inverse of Control
            Console.WriteLine("*******************************");
            Example_Inverse_Control.Test();

            // Non Using DI
            Console.WriteLine("*******************************");
            Non_DI.Test();
            // Using DI:
            Console.WriteLine("*******************************");
            DI.Test();

            // Cac loai DI: 
            //Inject thông qua phương thức khởi tạo: cung cấp các Dependency cho đối tượng thông qua hàm khởi tạo(như đã thực hiện ở ví dụ trên) -tập trung vào cách này vì thư viện.NET hỗ trợ sẵn
            //Inject thông qua setter: tức các Dependency như là thuộc tính của lớp, sau đó inject bằng gán thuộc tính cho Depedency object.denpendency = obj;
            //Inject thông qua các Interface - xây dựng Interface có chứa các phương thức Setter để thiết lập dependency, interface này sử dụng bởi các lớp triển khai, lớp triển khai phải định nghĩa các setter quy định trong interface

            Console.WriteLine("*******************************");
            DI_Constructor.Test();

            // các thư viện hỗ trợ DI: 
            // DI Container: ServiceCollection
            // - Đăng ký các dịch vụ lớp
            // ServiceProvider -> Get Service

            Console.WriteLine("*******************************");

            Service_Test.Test();
        }
    }
}
