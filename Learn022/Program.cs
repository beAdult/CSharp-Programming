// See https://aka.ms/new-console-template for more information

using System;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.Net.Http.Json;
using System.Text;

namespace Learn022 {
    //Namespace:
    //System.Net
    //System.Net.Mail
    //System.Net.NetworkInformation
    //System.Net.Http
    //Làm việc với máy chủ Dns, Uri, địa chỉ mạng có các lớp Dns, Uri, Cookie, IPAddress...
    //Làm việc với FTP Server có các lớp FtpStatusCode, FtpWebRequest, FtpWebResponse...
    //Làm việc với giao thức HTTP (máy chủ web) có các lớp như: HttpStatusCode, HttpWebRequest, HttpWebResponse, HttpClient, HttpMethod, HttpRequestMessage, HttpResponseMessage
    //Làm việc với máy chủ SMTP gửi email: SmtpClient, MailMessage, MailAddress, MailAddress
    //Làm việc với giao thức mạng: IPStatus, NetworkChange, Ping, TcpStatistics...

    // Lớp Dns(System.Net.Dns) cung cấp các phương thức tính để lấy thông tin về host(địa chỉ website, server cung cấp các dịch vụ mạng) từ hệ thống phân giải tên miền(Dns). Các thông tin truy vấn được nó trả về một đối tượng giao diện IPHostEntry
    // Some method: GetHostName(), GetHostEntry(): input: String, IPAddress
    // IPHostEntry có các thuộc tính để lấy thông tin về host như
    // HostName, AddressList

    // Lớp Ping(System.Net.NetworkInformation.Ping), lớp này cho phép ứng dụng xác định một máy từ xa(như server, máy trong mạng...) có phản hồi không.

    // Private methods is in Public methods (after them).
    class Program
    {
        static async Task Main(string[] args)
        {
            //awaint TestHttp();
            //await TestHttpRequest();
            //await TestReadDataByte();
            //await TestReadDataStream();
            await TestSendAsync();
        }

        public static void TestHttp()
        {
            string url = "https://xuanthulab.net/lap-trinh/csharp/?page=3#acff";
            var uri = new Uri(url);
            var uritype = typeof(Uri);
            uritype.GetProperties().ToList().ForEach(property => {
                Console.WriteLine($"{property.Name,15} {property.GetValue(uri)}");
            });
            Console.WriteLine($"Segments: {string.Join(",", uri.Segments)}");


            string url2 = "https://www.bootstrapcdn.com/";
            var uri2 = new Uri(url2);
            var hostEntry = Dns.GetHostEntry(uri2.Host);
            Console.WriteLine($"Host {uri2.Host} có các IP");
            hostEntry.AddressList.ToList().ForEach(ip => Console.WriteLine(ip));


            Console.WriteLine("Test ping of facebook");
            var ping = new Ping();
            var pingReply = ping.Send("facebook.com.vn");
            Console.WriteLine(pingReply.Status);
            if (pingReply.Status == IPStatus.Success)
            {
                Console.WriteLine(pingReply.RoundtripTime);
                Console.WriteLine(pingReply.Address);
            }
        }
        //1.Test Create Request with GetAsync

        // Test ReadAsStringArrayAsync
        public static async Task TestHttpRequest() {
            // HttpRequest: using HttpClient
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
            string url = "https://xuanthulab.net/networking-su-dung-httpclient-trong-c-tao-cac-truy-van-http.html";
            HttpResponseMessage response = await httpClient.GetAsync(url);

            //Console.WriteLine(response.EnsureSuccessStatusCode());
            Console.WriteLine(response.IsSuccessStatusCode);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.ReasonPhrase);

            var headers = response.Headers;

            Console.WriteLine();

            ShowHeaders(headers);
            var html = await GetWebContent(url);
            Console.WriteLine(html);
        }
        // Show the headers
        private static void ShowHeaders(HttpHeaders headers)
        {
            Console.WriteLine("Show cac headers");
            foreach (var header in headers)
            {
                foreach (var value in header.Value)
                {
                    Console.WriteLine($"{header.Key,25} : {value}");
                }
            }
            Console.WriteLine();
        }
        // From Url, return string to read data
        private static async Task<string> GetWebContent(string url)
        {
            using var httpClient = new HttpClient();
            // Change Request Header
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                ShowHeaders(response.Headers);
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"Tai thanh cang - statusCode {(int)response.StatusCode} {response.ReasonPhrase}");

                Console.WriteLine("Starting read data");
                string htmltext = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Nhan đuoc {htmltext.Length} ky tu");
                Console.WriteLine();
                return htmltext;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }


        // Test ReadAsByteArrayAsync
        public static async Task TestReadDataByte()
        {
            var url = "https://raw.githubusercontent.com/xuanthulabnet/jekyll-example/master/images/jekyll-01.png";
            byte[] bytes = await DownloadDataBytes(url);

            string filepath = "anh1.png";
            using (var stream = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                stream.Write(bytes, 0, bytes.Length);
                Console.WriteLine("save " + filepath);
            }
        }
        // Download from Url, return byte array to read data
        private static async Task<byte[]> DownloadDataBytes(string url)
        {
            using var httpClient = new HttpClient();
            // Change Request Header: User, ...
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                ShowHeaders(response.Headers);
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"Tai thanh cang - statusCode {(int)response.StatusCode} {response.ReasonPhrase}");

                Console.WriteLine("Starting read data");
                byte[] htmltext = await response.Content.ReadAsByteArrayAsync();

                return htmltext;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Array.Empty<byte>();
            }
        }



        // Test ReadAsStreamAsync
        public static async Task TestReadDataStream()
        {
            var url = "https://raw.githubusercontent.com/xuanthulabnet/linux-centos/master/docs/samba1.png";
            await DownloadDataStream(url, "anh2.png");
        }
        // Download from url, return stream to read data
        private static async Task DownloadDataStream(string url, string filename)
        {
            var httpClient = new HttpClient();
            Console.WriteLine($"Starting connect {url}");
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();

                int SIZEBUFFER = 500;
                using var streamwrite = File.OpenWrite(filename);
                byte[] buffer = new byte[SIZEBUFFER];

                bool endread = false;
                do                                                  // thực hiện đọc các byte từ stream và lưu ra streamwrite
                {
                    int numberRead = await stream.ReadAsync(buffer, 0, SIZEBUFFER);
                    Console.WriteLine(numberRead);
                    if (numberRead == 0)
                    {
                        endread = true;
                    }
                    else
                    {
                        await streamwrite.WriteAsync(buffer, 0, numberRead);
                    }

                } while (!endread);
                Console.WriteLine("Download success");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }



        //2. Test Create Request with SendAsync
        // Ngoài phương thức GetAsync gửi Request với phương thức GET ở trên ra,
        // có thể dùng phương thức SendAsync (hoặc Send nếu sử dụng code đồng bộ synchronous).
        public static async Task TestSendAsync()
        {
            await TestGetAsyncWithSendAsync();
            await TestFormURLEncodeContent();
            await TestStringContent();
            await TestMultipartFormDataContent();
        }

        private static async Task TestGetAsyncWithSendAsync()
        {
            // Method Get
            var httpClient = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage();

            httpRequestMessage.Method = HttpMethod.Get;
            httpRequestMessage.RequestUri = new Uri("https://code.ptit.edu.vn/student/question");
            httpRequestMessage.Headers.Add("Use-Agent", "Mozilla/5.0");

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            var html = await httpResponseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(html);
        }

        private static async Task TestFormURLEncodeContent()
        {
            //Với FormUrlEncodedContent bạn có thể tạo Content tương ứng như một Form HTML,
            //nó chứa các giá trị(key / value) sẽ Post đến Server.
            //Ví dụ sau, nó post đến server hai giá trị tương ứng key và value là
            //key1/ value1(có thể hiểu tương ứng với phần tử HTML Input có name là key1
            //và value là value1) và key2/ value2(trường hợp này chứa nhiều giá trị,
            //tương ứng với HTML Multi Select)

            var httpClient = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage();

            // Method Post
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri = new Uri("https://postman-echo.com/post");

            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("key1", "value1"));

            parameters.Add(new KeyValuePair<string, string>("key2", "value2-1"));
            parameters.Add(new KeyValuePair<string, string>("key2", "value2-2"));

            // Thiết lập Content
            var content = new FormUrlEncodedContent(parameters);
            httpRequestMessage.Content = content;

            // Thực hiện Post
            var response = await httpClient.SendAsync(httpRequestMessage);

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }

        private static async Task TestStringContent()
        {
            var httpClient = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri = new Uri("https://postman-echo.com/post");

            string jsonContent = "{\"https://postman-echo.com/post\"}";
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            httpRequestMessage.Content = httpContent;

            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseContent);
        }

        private static async Task TestMultipartFormDataContent()
        {
            var httpClient = new HttpClient();

            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri = new Uri("https://postman-echo.com/post");


            // Tạo đối tượng MultipartFormDataContent
            var content = new MultipartFormDataContent();

            // Tạo StreamContent chứa nội dung file upload, sau đó đưa vào content
            Stream fileStream = System.IO.File.OpenRead("C:\\Users\\admin\\source\\repos\\Learn022\\Program.cs");
            content.Add(new StreamContent(fileStream), "fileupload", "abc.xyz");

            // Thêm vào MultipartFormDataContent một StringContent
            content.Add(new StringContent("value1"), "key1");
            // Thêm phần tử chứa mạng giá trị (HTML Multi Select)
            content.Add(new StringContent("value2-1"), "key2[]");
            content.Add(new StringContent("value2-2"), "key2[]");


            httpRequestMessage.Content = content;
            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }
    }
}
