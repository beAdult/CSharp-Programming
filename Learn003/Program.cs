// See https://aka.ms/new-console-template for more information

using System;
using System.Text;

namespace Learn003
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello to String and StringBuilder");
            string infor = "Hoc ve Chuoi hom nay 28/06/2023";
            Console.WriteLine(infor);

            // add "" to string: 
            string infor2 = "Hoc ve Chuoi hom nay \"28/06/2023";
            Console.WriteLine(infor2);

            string infor3 = "Hoc ve Chuoi hom nay \"28/06/2023\"";
            Console.WriteLine(infor3);

            // add \ to string: using \\
            string infor4 = "\\Hoc ve Chuoi\t hom nay\n \"28/06/2023\"";
            Console.WriteLine(infor4);

            //  @ intact character added

            string infor5 = @"Hoc tap Va lam theo 

            tam guong dao duc 

            Ho Chi Minh

            \ Day: ""2023""";
            Console.WriteLine(infor5);

            int year = 2023;
            string infor6 = $@"Hoc tap Va lam theo 

            tam guong dao duc 

            Ho Chi Minh

            \ Day: {year, 10}";
            //value = 10; // + 10 space left,  if -10, 10 space right

            Console.WriteLine(infor6);

            // loop in string
            int len = infor.Length; // size of this String
            for (int i = 0; i < len; i++)
            {
                char c = infor[i];
                Console.WriteLine($"index is: {i} and character is {c, 3}");
            }

            foreach(var i in infor)
            {
                char c = i;
                Console.WriteLine($"index is: {i} and character is {c,3}");
            }

            string infor7 = "    Dat NQ      ";
            infor7 = infor7.Trim();
            Console.WriteLine(infor7);
            // and maybe trim charracter you want: "****    Dat NQ      ***" trim('*');
            // ToLower(), ToUpper(), Replace(str1, str2) void method

            // Insert(start Index, string), Substring(start Index, length)
            // Remove(start Index, count)
            string infor8 = infor2.Substring(2, 10);
            Console.WriteLine(infor8);

            // Split(character)
            string[] array_infor3 = infor3.Split(' ');
            foreach(var i in array_infor3)
            {
                Console.WriteLine(i);
            }

            // Join(characer)
            string[] array_string = { "Nguyen", "Quoc", "Dat" };
            string infor9 = string.Join(' ', array_string);
            Console.WriteLine(infor9);

            StringBuilder infor10 = new StringBuilder();
            infor10.Append("xin");
            infor10.Append(infor2);
            infor10.Replace("Hom nay", "Chao mung");
            string kq = infor10.ToString();
            Console.WriteLine(kq);
        }
    }
}