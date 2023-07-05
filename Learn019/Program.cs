// See https://aka.ms/new-console-template for more information

using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Learn019
{


    class Program
    {
        public class A
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
        public static void testType()
        {
            A a = new A
            {
                Name = "HOTEN",
                ID = 10
            };

            //Lấy tên và giá trị các thuộc tính có trong a
            foreach (PropertyInfo property in a.GetType().GetProperties())
            {
                string property_name = property.Name;         // Lấy tên thuộc tính
                object property_value = property.GetValue(a); // Đọc giá trị thuộc tính đối tượng a
                Console.WriteLine($"Thuộc tính {property_name} giá trị là {property_value}");
            }
        }
        



        public class MyClass
        {

            [Obsolete("Phương thức này lỗi thời, hãy  dùng phương thức Abc")]
            public static void Method1()
            {
                Console.WriteLine("Phương thức chạy");
            }
        }
        public static void checkValidationContext()
        {
            Employer user = new Employer();
            user.Name = "AF";
            user.Age = 6;
            user.PhoneNumber = "1234as";
            user.Email = "test@re";


            ValidationContext context = new ValidationContext(user, null, null);
            // results - lưu danh sách ValidationResult, kết quả kiểm tra
            List<ValidationResult> results = new List<ValidationResult>();
            // thực hiện kiểm tra dữ liệu
            bool valid = Validator.TryValidateObject(user, context, results, true);

            if (!valid)
            {
                // Duyệt qua các lỗi và in ra
                foreach (ValidationResult vr in results)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{vr.MemberNames.First(),13}");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"    {vr.ErrorMessage}");
                }
            }
        }

        static void Main(string[] args)
        {
            testType();
            TestReadAttribute.test();
            checkValidationContext();
        }
    }
}
