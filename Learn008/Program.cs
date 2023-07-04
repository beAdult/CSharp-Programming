using System;
using System.Linq;

namespace Learn008
{
    class Program
    {
        public static void Main(string[] args) {
            Console.WriteLine("Anonymous & Dinamic data type");
            Console.WriteLine("Null & nullable");
            var product = new {
                id = "SP991",
                price = 1999
            };
            // read only, can not set
            Console.WriteLine(product.price + " " + product.id);

            // Anonymous in Class
            List<Student> students = new List<Student>() { new Student() { Id = "112", Name = "Dat", Age= 1},
            new Student() { Id = "113", Name = "Toan", Age= 2},
            new Student() { Id = "2233", Name = "Ng", Age= 4}};

            var result = from student in students
                         where student.Id == "113"
                         select new
                         {
                             Name = student.Name,
                             Age = student.Age
                         };
            foreach (var student in result)
            {
                Console.WriteLine(student.Name + " " + student.Age);
            }

            // Dynamic
            // Biên dịch không có lỗi, lỗi xảy ra khi thực thi.
            // dynamic cc = 11;
            // cc.Hloa = 112;
            // cc.dajkkas = 2;
            // PrintSound(cc);

            // dynamic type with class
            Cat c = new Cat();
            PrintSound(c);


            // Null
            Abc ab = null;
            Abc aa = new Abc();
            aa?.sayHello();

            int? a;
            a = null;
            a = 10;
            if (a.HasValue) // or a != null
            {
                int _age = a.Value; // or (int) a
                Console.WriteLine(_age);
            }

        }

        class Student
        {
            private string id, name;
            private int age;

            public Student(string _id, string _name, int _age) {
                this.id = _id;
                this.name = _name;
                this.age = _age;
            }

            public Student()
            {
                this.id = this.name = "";
                this.age = 0;
            }

            public string Id
            {
                get { return id; }
                set { id = value; }
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public int Age
            {
                get { return age; }
                set { age = value; }
            }
        }

        class Cat
        {
            public string Id { get; set; }

            public void Sound()
            {
                Console.WriteLine(Id);
            }
        }

        static void PrintSound(dynamic obj)
        {
            obj.Id = "Meo meo";
            Console.WriteLine(obj.Id);
            obj.Sound();
        }


        class Abc
        {
            public void sayHello() => Console.WriteLine("Hello Data");
        }

    }
}