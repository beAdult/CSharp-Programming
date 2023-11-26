using System.Net;
using System.Text;

namespace Learn024
{
    class Program
    {
        static async Task Main(String[] args)
        {
            await TestMyHttpServer();
        }

        static async Task TestMyHttpServer()
        {
   
            var server = new MyHttpServer(new string[] { "http://localhost:8080/" });
            await server.StartAsync();
           
        }

        static async Task TestHttpListener()
        {
            if (HttpListener.IsSupported)
            {
                Console.WriteLine("Supported");
            }
            else
            {
                Console.WriteLine("Not Support");
                throw new Exception("Not Support");
            }
            string[] prefixes = new string[] { "http://*:8080/" };
            var server = new HttpListener();
            server.Prefixes.Add("http://localhost:8080/");

            server.Start();
            Console.WriteLine("Server Http Start");
            do
            {
                HttpListenerContext context = await server.GetContextAsync();
                Console.WriteLine("Client has connected");
                HttpListenerResponse response = context.Response;
                var outputstream = response.OutputStream;
                response.Headers.Add("content-type", "text/html");
                var html = "<h1> Hello MR Dat <h1>";
                var bytes = Encoding.UTF8.GetBytes(html);
                await outputstream.WriteAsync(bytes, 0, bytes.Length);
                outputstream.Close();
            } while (server.IsListening);
        }
    }
}