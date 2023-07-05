// See https://aka.ms/new-console-template for more information

namespace Learn018
{
    class Program
    {
        static void Main(string[] args)
        {
            String html = "Ví dụ sử dụng HtmlHelper".HtmlTag("div", "text-danger");
            Console.WriteLine(html);

            "abc".HtmlTag("p");
            Console.WriteLine("Hello World!");
        }
    }
}
