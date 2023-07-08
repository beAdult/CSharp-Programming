using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace Learn023
{
    internal class MyDelegatingHandler
    {
        private static CookieContainer cookieContainer = new CookieContainer();

        private class ChangeUri : DelegatingHandler
        {
            public ChangeUri(HttpMessageHandler innerHandler) : base(innerHandler)
            {
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
            {
                var host = httpRequestMessage.RequestUri.Host.ToLower();
                Console.WriteLine($"Check in  ChangeUri - {host}");
                if (host.Contains("google.com"))
                {
                    httpRequestMessage.RequestUri = new Uri("https://github.com/");
                }

                return base.SendAsync(httpRequestMessage, cancellationToken);
            }
        }
        private class DenyAccessFacebook : DelegatingHandler
        {
            public DenyAccessFacebook(HttpMessageHandler innerHandler) : base(innerHandler)
            {
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
            {

                var host = httpRequestMessage?.RequestUri?.Host.ToLower();
                Console.WriteLine($"Check in  DenyAccessFacebook - {host}");
                if (host.Contains("facebook.com"))
                {
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new ByteArrayContent(Encoding.UTF8.GetBytes("Không được truy cập"));
                    return Task.FromResult<HttpResponseMessage>(response);
                }

                return base.SendAsync(httpRequestMessage, cancellationToken);
            }
        }


        private class MyHttpClientHandler : HttpClientHandler
        {
            public MyHttpClientHandler(CookieContainer cookie_container)
            {

                CookieContainer = cookie_container;
                AllowAutoRedirect = false;             
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                UseCookies = true;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                ShowHeaders("Request header trước khi qua Handler MyHttpClientHandler", request.Headers);

                var task = base.SendAsync(request, cancellationToken); 
                await task;

                ShowHeaders("Request header sau khi qua Handler MyHttpClientHandler", request.Headers);


                return task.Result;
            }
        }

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


        private static async Task<string> GetWebContent(string url)
        {
            //  Midleware Pipeline:
            //  facebookHandler -- changeUri -- myHttpClientHandler

            using (var myHttpClientHandler = new MyHttpClientHandler(cookieContainer))
            using (var changeUri = new ChangeUri(myHttpClientHandler))
            using (var facebookHandler = new DenyAccessFacebook(changeUri))

            using (var httpClient = new HttpClient(facebookHandler))
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

        public static async Task TestDelegatingHandler()
        {
            // string url = "https://www.facebook.com/xuanthulab/";
            // string url = "https://xuanthulab.net";
            string url = "https://www.google.com/";

            var html = await GetWebContent(url);                                                   
            Console.WriteLine(html != null ? html.Substring(0, Math.Min(150, html.Length)) : "Lỗi");
        }
    }
}
