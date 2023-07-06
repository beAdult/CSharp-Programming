using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn021
{
    internal class Example
    {
        class ClassC
        {
            public void ActionC() => Console.WriteLine("Action in ClassC");
        }

        class ClassB
        {
            // Phụ thuộc của ClassB là ClassC
            ClassC c_dependency;

            public ClassB(ClassC classc) => c_dependency = classc;
            public void ActionB()
            {
                Console.WriteLine("Action in ClassB");
                c_dependency.ActionC();
            }
        }

        class ClassA
        {
            // Phụ thuộc của ClassA là ClassB
            ClassB b_dependency;

            public ClassA(ClassB classb) => b_dependency = classb;
            public void ActionA()
            {
                Console.WriteLine("Action in ClassA");
                b_dependency.ActionB();
            }
        }

        public static void Test()
        {
            Console.WriteLine("Test Example about Dependence Injection");
            ClassC objectC = new ClassC();
            ClassB objectB = new ClassB(objectC);
            ClassA objectA = new ClassA(objectB);

            objectA.ActionA();
        }
    }
}
