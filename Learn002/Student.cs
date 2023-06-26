using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn002
{
    internal class Student
    {
        private string id, name;
        private int age;


        public Student()
        {
            this.id = this.name = "";
            this.age = 0;
        }

        public Student(string id, string name, int age)
        {
            this.id = id;
            this.name = name;
            this.age = age;
        }

        ~Student()
        {
            Console.WriteLine("Destroyed " + this.name);
        }

        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }


        public override string ToString()
        {
            return "Id: " + id + " " + "Name is: " + name + " " + "Age is: " + age;
        }
    }
}
