using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.VisualBasic.FileIO;

namespace Learn021
{
    internal class Service_Test
    {
        interface IClassB
        {
            public void ActionB();
        }
        interface IClassC
        {
            public void ActionC();
        }

        class ClassC : IClassC
        {
            public ClassC() => Console.WriteLine("ClassC is created");
            public void ActionC() => Console.WriteLine("Action in ClassC");
        }

        class ClassB : IClassB
        {
            IClassC c_dependency;
            public ClassB(IClassC classc)
            {
                c_dependency = classc;
                Console.WriteLine("ClassB is created");
            }
            public void ActionB()
            {
                Console.WriteLine("Action in ClassB");
                c_dependency.ActionC();
            }
        }


        class ClassA
        {
            IClassB b_dependency;
            public ClassA(IClassB classb)
            {
                b_dependency = classb;
                Console.WriteLine("ClassA is created");
            }
            public void ActionA()
            {
                Console.WriteLine("Action in ClassA");
                b_dependency.ActionB();
            }
        }

        // ĐỊnh nghĩa thêm: 
        class ClassC1 : IClassC
        {
            public ClassC1() => Console.WriteLine("ClassC1 is created");
            public void ActionC()
            {
                Console.WriteLine("Action in C1");
            }
        }

        class ClassB1 : IClassB
        {
            IClassC c_dependency;
            public ClassB1(IClassC classc)
            {
                c_dependency = classc;
                Console.WriteLine("ClassB1 is created");
            }
            public void ActionB()
            {
                Console.WriteLine("Action in B1");
                c_dependency.ActionC();
            }
        }
        // ussing for delegate in Service
        class ClassB2 : IClassB
        {
            IClassC c_dependency;
            string message;
            public ClassB2(IClassC classc, string mgs)
            {
                c_dependency = classc;
                message = mgs;
                Console.WriteLine("ClassB2 is created");
            }
            public void ActionB()
            {
                Console.WriteLine(message);
                c_dependency.ActionC();
            }
        }
        public static void Test()
        {
            RegisterService();
            InjectService();
            RegisterDelegateService();
            IOptionsInject();
            ConfigFromFile();
        }
        // Đăng ký dịch vụ
        public static void RegisterService()
        {
            var service = new ServiceCollection();

            // Đăg ký các dịch vụ: 
            // Đăng ký cho IClassC, ClassC, ClassC1

            // Đăng ký theo kiểu Signleton - 1 phiên bản tồn tại trong suốt vòng đời của provider
            // service.AddSingleton<IClassC, ClassC>();
            // gọi bao nhiêu lần với kiểu đối tượng khác nhau thì cũng chỉ tạo ra một đối tượng với 1 mã hascode

            // Đăng ký theo kiểu Transient - nhiều phiên bản theo lời gọi
            //service.AddTransient<IClassC, ClassC>();

            // Đăng ký kiểu Scoped
            service.AddScoped<IClassC, ClassC>();
            // ĐỐi tượng được tạo ra theo scope

            var provider = service.BuildServiceProvider();
            //var a = provider.GetService<>();
            var classc = provider.GetService<IClassC>();

            for (int i = 0; i < 5; ++i)
            {
                IClassC ii = provider.GetService<IClassC>();
                Console.WriteLine(ii.GetHashCode());
            }

            // tạo ra 1 scoped khác
            using (var scope = provider.CreateScope())
            {
                var provider1 = scope.ServiceProvider;
                for (int i = 0; i < 5; ++i)
                {
                    IClassC ii = provider1.GetService<IClassC>();
                    Console.WriteLine(ii.GetHashCode());
                }
            }
        }

        // Inject vào
        public static void InjectService()
        {
            // ClassA
            // IClassB -> ClassB,  ClassB1
            // IClassC -> ClassC,  ClassC1

            ServiceCollection services = new ServiceCollection();

            services.AddSingleton<ClassA, ClassA>();
            services.AddSingleton<IClassB, ClassB>();
            services.AddSingleton<IClassC, ClassC1>();

            var provider = services.BuildServiceProvider();

            ClassA service_a = provider.GetService<ClassA>();

            service_a.ActionA();

            // ClassC is created
            // ClassB is created
            // ClassA is created
            // Action in ClassA
            // Action in ClassB
            // Action in ClassC
        }

        // Factory - Delegate create object service:
        // Factory nhận tham số là IServiceProvider và trả về đối tượng địch vụ cần tạo
        private static ClassB2 CreateB2Factory(IServiceProvider serviceprovider)
        {
            var service_c = serviceprovider.GetService<IClassC>();
            var sv = new ClassB2(service_c, "Thực hiện trong ClassB2");
            return sv;
        }
        public static void RegisterDelegateService()
        {
            //services.AddSingleton<ServiceType>((IServiceProvider provider) =>
            //{
            //// các chỉ thị
            //// ...
            //    return (đối tượng kiểu ImplementationType);
            //});

            //Trong cú pháp trên thì Delegate đó là

            //(IServiceProvider provider) => {
            //    // các chỉ thị
            //    // ...
            //    return (đối tượng kiểu ImplementationType);
            //}

            // Nó nhận tham số là IServiceProvider(chính là đối tượng được sinh ra bởi
            // ServiceCollection.BuildServiceProvider()), Delegate phải trả về một đối
            // tượng triển khai từ ServiceType


            // ClassA
            // IClassB -> ClassB,  ClassB1, ClassB2
            // IClassC -> ClassC,  ClassC1

            ServiceCollection services = new ServiceCollection();

            services.AddSingleton<ClassA, ClassA>();
            services.AddSingleton<IClassB, ClassB2>(
                (provider) => {
                    var b2 = new ClassB2(
                        provider.GetService<IClassC>(),
                        "Tra ve B2");
                    return b2;
            });

            // Factory:
            //services.AddSingleton<IClassB, ClassB2>(CreateB2Factory);

            services.AddSingleton<IClassC, ClassC1>();
            //services.AddSingleton<string, string>(); khoong su dung duoc vi khong co ham khoi tao lop string

            var provider = services.BuildServiceProvider();

            ClassA service_a = provider.GetService<ClassA>();

            service_a.ActionA();
        }

        // Sử dụng Options khởi tạo dịch vụ trong DI
        public class MyServiceOptions
        {
            public string data1 { get; set; }
            public int data2 { get; set; }
        }

        public class MyService
        {
            public string data1 { get; set; }
            public int data2 { get; set; }

            // Tham số khởi tạo là IOptions, các tham số khởi tạo khác nếu có khai báo như bình thường
            public MyService(IOptions<MyServiceOptions> options)
            {
                // Đọc được MyServiceOptions từ IOptions
                MyServiceOptions opts = options.Value;
                data1 = opts.data1;
                data2 = opts.data2;
            }
            public void PrintData() => Console.WriteLine($"{data1} / {data2}");
        }
        public static void IOptionsInject()
        {
            ServiceCollection services = new ServiceCollection();
            //services.AddSingleton<MyService>();
            //var provider = services.BuildServiceProvider();
            //var myservicess = provider.GetService<MyService>();
            //myservicess.PrintData();

            //services.Configure<T>(
            //    (T options)
            //    {
            //                // T là tên lớp chứa các thiết lập
            //                // Hãy thiết lập các giá trị cho options
            //            }
            //);

            services.AddSingleton<MyService>();
            services.Configure<MyServiceOptions>(
                (MyServiceOptions options) =>
                {
                    options.data1 = " Hello World";
                    options.data2 = 10;
                }
            );

            var provider = services.BuildServiceProvider();
            var myservicess = provider.GetService<MyService>();
            myservicess.PrintData();


            //Lưu ý 1: nếu muốn lấy đối tượng lớp MyServiceOptions trong DI Container, thì:
            //var config = serviceprovider.GetService<IOptions<MyServiceOptions>>()
            //MyServiceOptions myServiceOptions = config.Value;

            //Lưu ý 2: nếu muốn tạo trực tiếp đối tượng IOptions<MyServiceOptions>, 
            //    dành cho trường hợp muốn tạo MyService trực tiếp không thông qua DI Container.
            //    Thì dùng phương thức Factory Options.Create(obj), ví dụ:
            //var opts = Options.Create(new MyServiceOptions()
            //{
            //    data1 = "DATA-DATA-DATA-DATA-DATA",
            //    data2 = 12345
            //});
            //MyService myService = new MyService(opts);
            //myService.ShowData();
        }

        // Sử dụng Options khởi tạo dịch vụ trong DI
        public static void ConfigFromFile()
        {
            //Console.WriteLine(Directory.GetCurrentDirectory());
            var configBuilder = new ConfigurationBuilder()
                       .SetBasePath("C:\\Users\\admin\\source\\repos\\Learn021")      // file config ở thư mục hiện tại
                       .AddJsonFile("config.json");                  // nạp config định dạng JSON
            var configurationroot = configBuilder.Build();

            var cf1 = configurationroot.GetSection("Option2").GetSection("key1").Value; // Test
            var cf2 = configurationroot.GetSection("Option2").GetSection("key2").Value; // 789
            var cf3 = configurationroot.GetSection("Option2").GetSection("key3").Value; // null, không tồn tại

            Console.WriteLine(cf1 + " " + cf2 + " " + cf3);

            //Nạp config json vào IOption
            ServiceCollection services = new ServiceCollection();
            // Nạp mở phương thức mở rộng
            services.AddOptions();
            services.Configure<MyServiceOptions>(configurationroot.GetSection("MyServiceOptions"));

            services.AddSingleton<MyService>();

            var provider = services.BuildServiceProvider();
            var myservice = provider.GetService<MyService>();
            myservice.PrintData();
            // Lưu ý: phải cài package  ConfigurationExtensions
            // dotnet add package Microsoft.Extensions.Options.ConfigurationExtensions
            //Kỹ thuật DI với thư viện DependencyInjection ở trên là kiến rất cần nắm vững, nó là cơ sở để học các các mô hình lập trình hiện đại, nhất là sau này áp dụng với Asp.Net Core bạn cần hiểu nó.
        }
    }
}
