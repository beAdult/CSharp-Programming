// See https://aka.ms/new-console-template for more information

using System;
using System.Net;
using System.Net.Http.Headers;

namespace Learn023
{
    class Program
    {
        //Lớp HttpMessageHandler
        //Lớp HttpMessageHandler là lớp trừu tượng, nó là lớp cơ sở được thư viện.NET Core triển khai ra các lớp như
        //DelegatingHandler, HttpMessageHandler, HttpClientHandler...các lớp triển khai này
        //(hoặc nếu tự xây dựng lớp triển khai HttpMessageHandler) thì phải nạp chồng phương thức SendAsync:
        //protected Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
        //Các lớp triển khai HttpMessageHandler dùng để khởi tạo HttpClient, lúc này HttpCliet thực hiện gửi
        //truy vấn(SendAsync) thì SendAsync của handler sẽ thực thi.

        static async Task Main(string[] args) {
            await TestHttpClientHandler();
            await TestSocketsHttpHandler();
            await TestHttpClientHandlerV2();
            await TestSocketsHttpHandlerV2();
            await MyDelegatingHandler.TestDelegatingHandler();
        }

        private static async Task TestHttpClientHandler()
        {
            // Chú ý, từ .NET Core 2.1 khuyến khích sử dụng SocketsHttpHandler thay cho HttpClientHandler
            // Properties: AllowAutoRedirect: mặc định là true, tự động chuyển hướng.
            //             AutomaticDecompression: Thuộc tính để handler tự động giải nén / nén nội dung HTTP, nó thuộc kiểu enum DecompressionMethods gồm có:
            //                  DecompressionMethods.None không sử dụng nén
            //                  DecompressionMethods.GZip dùng thuật toán gZip
            //                  DecompressionMethods.Deflate dùng thuật toán nén deflate
            //                  Ví dụ có thể gán:AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            //             UserCookies: Mặc định là true: cho phép sử dụng thuộc tính CookieContainer để lưu các Cookie của server khi respone trả về, cũng như tự động gửi Cookie khi gửi truy vấn
            //             CookieContainer: Thuộc tính thuộc lớp CookieContainer, nó lưu các cookie.

            var url = "https://postman-echo.com/post";
            // Tạo handler
            using HttpClientHandler handler = new HttpClientHandler();
            // Tạo bộ chứa cookie và sử dụng bởi handler
            CookieContainer cookies = new CookieContainer();
            // Thêm các cookie nêu muốn
            // cookies.Add(new Uri(url), new Cookie("name", "value"));

            handler.CookieContainer = cookies;
            using var httpClient = new HttpClient(handler);

            // Tạo HttpRequestMessage
            using var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri = new Uri(url);
            httpRequestMessage.Headers.Add("User-Agent", "Mozilla/5.0");
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("key1", "value1"),
                new KeyValuePair<string, string>("key2", "value2")

            };
            httpRequestMessage.Content = new FormUrlEncodedContent(parameters);

            // Thực hiện truy vấn
            var response = await httpClient.SendAsync(httpRequestMessage);
            // Hiện thị các cookie (các cookie trả về có thể sử dụng cho truy vấn tiếp theo)
            cookies.GetCookies(new Uri(url)).ToList().ForEach(cookie => {
                Console.WriteLine($"{cookie.Name} = {cookie.Value}");
            });

            // Đọc chuỗi nội dung trả về (HTML)
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }

        private static async Task TestSocketsHttpHandler()
        {
            //Lớp handler SocketsHttpHandler sử dụng giống hệt HttpClientHandler nó được thiết kế để sử dụng tốt hơn
            //- nhanh hơn trên .NET Core, nó độc lập thiết bị tốt hơn(chạy tốt trên macOS, Linux).

            var url = "https://postman-echo.com/post";
            // Tạo bộ chứa cookie và sử dụng bởi handler
            CookieContainer cookies = new CookieContainer();
            // Thêm các cookie nêu muốn
            // cookies.Add(new Uri(url), new Cookie("name", "value"));

            // Tạo handler
            using SocketsHttpHandler handler = new SocketsHttpHandler();
            handler.CookieContainer = cookies;   
            handler.AllowAutoRedirect = false;          
            handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            handler.UseCookies = true;

            // Tạo HttpClient - thiết lập handler cho nó
            using var httpClient = new HttpClient(handler);

            // Tạo HttpRequestMessage
            using var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            httpRequestMessage.RequestUri = new Uri(url);
            httpRequestMessage.Headers.Add("User-Agent", "Mozilla/5.0");
            httpRequestMessage.Headers.Add("Accept", "text/html,application/xhtml+xml+json");

            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("key1", "value1"),
                new KeyValuePair<string, string>("key2", "value2")

            };
            httpRequestMessage.Content = new FormUrlEncodedContent(parameters);

            // Thực hiện truy vấn
            var response = await httpClient.SendAsync(httpRequestMessage);

            // Hiện thị các cookie (các cookie trả về có thể sử dụng cho truy vấn tiếp theo)
            cookies.GetCookies(new Uri(url)).ToList().ForEach(cookie => {
                Console.WriteLine($"{cookie.Name} = {cookie.Value}");
            });

            // Đọc chuỗi nội dung trả về (HTML)
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }



        // Create Nested Class
        private static CookieContainer cookieContainer = new CookieContainer();

        private class MyHttpClientHandler: HttpClientHandler
        {
            public MyHttpClientHandler(CookieContainer cookie_container)
            {
                CookieContainer = cookie_container;
                AllowAutoRedirect = false;
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                UseCookies = true;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
            {
                ShowHeaders("Request header trước khi qua Handler ", httpRequestMessage.Headers);

                var task = base.SendAsync(httpRequestMessage, cancellationToken);
                await task;

                ShowHeaders("Request header sau khi qua Handler ", httpRequestMessage.Headers);

                // Xem Cookie nếu  có
                // var uri = request.RequestUri;
                // var cookieHeader = CookieContainer.GetCookieHeader(uri);
                // Console.WriteLine(cookieHeader); 

                return task.Result;
            }
        }
        // Method Showheaders
        private static void ShowHeaders(string lable, HttpHeaders headers)
        {
            Console.WriteLine(lable);
            foreach (var header in headers)
            {
                string value = string.Join(" ", header.Value);
                Console.WriteLine($"{header.Key,20} : {value}");
            }
            Console.WriteLine();
        }

        private static async Task <string> GetWebContentHttpClientHandler(string url)
        {
            cookieContainer = new CookieContainer();
            using (var myHttpClientHandler = new MyHttpClientHandler(cookieContainer))
                using (var httpClient = new HttpClient(myHttpClientHandler))
                {
                    Console.WriteLine($"Starting connect {url}");
                    httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string html = await response.Content.ReadAsStringAsync();
                    return html;
                };  
        }

        public static async Task TestHttpClientHandlerV2()
        {
            string url = "https://www.google.com.vn/";

            cookieContainer.Add(new Uri(url), new Cookie("NameCookie", "ValueCookie"));   
            var html = await GetWebContentHttpClientHandler(url);
            Console.WriteLine(html != null ? html.Substring(0, 150) : "Lỗi");

            Console.WriteLine();
            Console.WriteLine("Cookie Header:");
            Console.WriteLine(cookieContainer.GetCookieHeader(new Uri(url)));
        }


        private static async Task<string> GetWebContentSocketsHttpHandler(string url)
        {
            cookieContainer = new CookieContainer();
            using (var socketsHandler = new SocketsHttpHandler())
            {
                socketsHandler.CookieContainer = cookieContainer;    
                socketsHandler.AllowAutoRedirect = false;              
                socketsHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                socketsHandler.UseCookies = true;

                using (var httpClient = new HttpClient(socketsHandler))
                {
                    Console.WriteLine($"Starting connect {url}");
                    httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string htmltext = await response.Content.ReadAsStringAsync();
                    return htmltext;
                }
            }
        }

        public static async Task TestSocketsHttpHandlerV2()
        {
            string url = "https://www.google.com.vn/";
            cookieContainer.Add(new Uri(url), new Cookie("NameCookie", "ValueCookie"));     

            var html = await GetWebContentSocketsHttpHandler(url);
            Console.WriteLine(html != null ? html.Substring(0, 150) : "Lỗi");

            Console.WriteLine();
            Console.WriteLine("Cookie Header:");
            Console.WriteLine(cookieContainer.GetCookieHeader(new Uri(url)));
        }

    }

}
