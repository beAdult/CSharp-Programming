// See https://aka.ms/new-console-template for more information
//Console.WriteLine("List, SoterdList, Queue, Stack");
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Learn014
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("List, SoterdList, Queue, Stack");


            // List using testList();
            testList();

            // SortedList using testSortedList();
            testSortedList();

            testQueue();
            testStack();
            testLinkedList();
            testDic();
            testHash();
        }
        public static void testSortedList()
        {

            // Khởi tạo SortedList
            var products = new SortedList<string, string>();
            products.Add("Iphone 6", "P-IPHONE-6"); // Thêm vào phần tử mới (key, value)
            products.Add("Laptop Abc", "P-LAP");
            products["Điện thoại Z"] = "P-DIENTHOAI"; // Thêm vào phần tử bằng Indexer
            products["Tai nghe XXX"] = "P-TAI";       // Thêm vào phần tử bằng Indexer


            // Duyệt qua các phần tử, mỗi phần tử lấy key/value lưu trong biến
            // kiểu KeyValuePair
            Console.WriteLine("TÊN VÀ MÃ");
            foreach (KeyValuePair<string, string> p in products)
            {
                Console.WriteLine($"    {p.Key} - {p.Value}");
            }
            // kết quả: (để ý danh sách đã xếp theo key)
            // TÊN và MÃ
            //     Điện thoại Z - P-DIENTHOAI
            //     Iphone 6 - P-IPHONE-6
            //     Laptop Abc - P-LAP
            //     Tai nghe XXX - P-TAI

            // Đọc value khi biết key
            string productName = "Tai nghe XXX";
            Console.WriteLine($"{productName} có mã là {products[productName]}");

            // Cập nhật giá trị vào phần tử theo key
            products[productName] = "P-TAI-UPDATED";

            // Duyệt qua các giá trị
            Console.WriteLine("\nDANH SÁCH MÃ SẢN PHẢM");
            foreach (var product_code in products.Values)
            {
                Console.WriteLine($"--- {product_code}");
            }
            // kết quả:
            // DANH SÁCH MÃ SẢN PHẢM
            // -- P-DIENTHOAI
            // -- P-IPHONE-6
            // -- P-LAP
            // -- P-TAI-UPDATED

            // Duyệt qua các key
            Console.WriteLine("\nDANH SÁCH TÊN SP");
            foreach (var product_name in products.Keys)
            {
                Console.WriteLine($"... {product_name}");
            }
            // DANH SÁCH TÊN SP
            // -- Điện thoại Z
            // -- Iphone 6
            // -- Laptop Abc
            // -- Tai nghe XXX

        }
        public static void testList()
        {
            List<int> ds = new List<int>();
            ds.Add(1);
            ds.Add(2);
            ds.AddRange(new int[] { 6, 7, 8, 9, 10, 11, 12, 13, });
            ds.Insert(2, 99);
            // ds.InsertRange(new int [] {});
            ds.Remove(2);
            ds.RemoveAt(3);
            // ds.RemoveAll(value);
            Console.WriteLine(ds.Count);
            // ds.Clear();
            foreach (int i in ds)
            {
                Console.WriteLine(i);
            }
            var nn = ds.FindAll(x => x > 1);
            var n = ds.Find(
                (e) =>
                {
                    return e > 10;
                });
            Console.WriteLine(n);
            Console.WriteLine(nn);
            var numbers = new List<int>() { 1, 2, 3, 4 };     // khởi tạo 4 phần tử
            var products = new List<Product>()            // khởi tạo 1 phần tử
            {
                new Product(1, "Iphone 6", 100, "Trung Quốc")
            };
            var p = new Product(2, "IPhone 7", 200, "Trung Quốc");
            products.Add(p);                                                // Thêm p vào cuối List
            products.Add(new Product(3, "IPhone 8", 400, "Trung Quốc"));

            var arrayProducts = new Product[]                  // Mảng 2 phần tử
            {
                new Product(4, "Glaxy 7", 500, "Việt Nam"),
                new Product(5, "Glaxy 8", 700, "Việt Nam"),
            };
            products.AddRange(arrayProducts);
            products.Insert(3, new Product(6, "Macbook Pro", 1000, "Mỹ"));     // chèn phần tử vào vị trí index 3, (thứ 4)
            var pro = products[2];                                             // đọc phần tử có index = 2
            Console.WriteLine(pro.ToString());

            for (int i = 1; i < products.Count; i++)
            {
                var pi = products[i - 1];
                Console.WriteLine(pi.ToString());
            }

            // Duyệt qua các phần tử bằng foreach
            foreach (var pi in products)
            {
                Console.WriteLine(pi.ToString());
            }
            products.RemoveAt(0);                           // xóa phần tử đầu tien
            products.RemoveRange(products.Count - 2, 2);    // xóa 2 phần tử cuối
            var pro_rm = products[1];
            products.Remove(pro_rm);

            //Product foundpr1 = products.Find(
            //    (Product ob) => { return (ob.Name == "Glaxy 8"); }
            //);
            //if (foundpr1 != null)
            //    Console.WriteLine("(found) " + foundpr1.ToString("O"));
            //// (found) Xuất xứ: Việt Nam - Tên: Glaxy 8 - Giá: 700 - ID: 5

            // tìm index của đối tượng có xuất xứ là "Trung Quốc"
            var ifound = products.FindIndex(x => x.Origin == "Trung Quốc");

            // tìm các sản phẩm có giá trên 100
            List<Product> p_100 = products.FindAll(product => product.Price > 100);

            Product pr1 = products.Find((new SearchNameProduct("Glaxy 8")).search);        // Tìm sản phẩm có tên Glaxy 8
            Product pr2 = products.Find((new SearchNameProduct("IPhone 6")).search);       // Tìm sản phẩm có tên IPhone 6


            products.Sort();
            foreach (var pi in products)
            {
                Console.WriteLine(pi.ToString("N"));
            }


            products.Sort(
                (p1, p2) => {
                    if (p1.ID > p2.ID)
                        return 1;
                    else if (p1.ID == p2.ID)
                        return 0;
                    return -1;
                }
            );
            foreach (var pi in products)
            {
                Console.WriteLine(pi.ToString("N"));
            }

            //Contains(obj) kiểm tra có chứa phần tử obj
            //Reverse() đảo thứ tự danh sách
            //ToArray() copy các phần tử ra mảng

        }

        public static void testQueue()
        {
            //Count Thuộc tính lấy tổng số phần tử trong hàng
            //Enqueue vào xếp hàng -đưa phần tử vào cuối hàng đợi
            //Dequeue đọc -và loại ngay phần tử ở đầu hàng đợi -lỗi nếu hàng đợi không có phần tử nào
            //Peek đọc phần tử đầu hàng đợi
            Queue<string> hoso_canxuly = new Queue<string>();

            hoso_canxuly.Enqueue("Hồ sơ A"); // Hồ sơ xếp thứ nhất trong hàng đợi
            hoso_canxuly.Enqueue("Hồ sơ B"); // Hồ sơ xếp thứ hai
            hoso_canxuly.Enqueue("Hồ sơ C");


            // Lấy hồ sơ xếp trước xử lý  trước, cho đến hết
            while (hoso_canxuly.Count > 0)
            {
                var hs = hoso_canxuly.Dequeue();
                Console.WriteLine($"Xử lý {hs}, còn lại {hoso_canxuly.Count}");
            }
        }

        public static void testStack()
        {
            // Count	Thuộc tính lấy tổng số phần tử trong hàng
            // Push đẩy(thêm) một phần tử vào đỉnh stack
            // Pop đọc -xóa phần tử đỉnh stack
            // Peek đọc phần tử đỉnh stack
            // Contains kiểm tra một phần tử có trong stack hay không

            var nhakho = new Stack<string>();

            nhakho.Push("Sản phẩm A");
            nhakho.Push("Sản phẩm B");
            nhakho.Push("Sản phẩm C");

            // Xếp vào sau thì tháo ra trước
            while (nhakho.Count > 0)
            {
                var sp = nhakho.Pop();
                Console.WriteLine($"Bốc dỡ {sp} / còn lại {nhakho.Count}");
            }

        }

        public static void testLinkedList()
        {
            //LinkedListNode trong C#
            //LinkedListNode<T> là lớp biểu diễn NÚT trong LinkedList, các đối tượng của LinkedListNode được tạo ra từ LinkedList. Nó có các thuộc tính sau:

            //Thuộc tính  Ý nghĩa
            //List Thuộc tính - tham chiếu(trỏ) đến LinkedList
            //Value Thuộc tính - là dữ liệu của Node
            //Next Thuộc tính - tham chiếu(trỏ) đến NÚT tiếp theo(phía sau) -null thì nó là nút cuối
            //Previous    Thuộc tính -tham chiếu(trỏ) đến NÚT phía trước -null thì nó là nút đầu tiên


            //Một số phương thức trong LinkedList
            //Member Ý nghĩa
            //Count   Số nút trong danh sách
            //First   Nút đầu tiên của danh sách
            //Last Nút đầu tiên của danh sách
            //AddFirst(T) Chèn một nút có dữ liệu T vào đầu danh sách
            //AddLast(T)  Chèn một nút có dữ liệu T vào cuối danh sách
            //AddAfter(Node, T)   Chèn nút với dữ liệu T, vào sau nút Node(kiểu LinkedListNode)
            //AddBefore(Node, T)  Chèn nút với dữ liệu T, vào trước nút Node(kiểu LinkedListNode)
            //Clear() Xóa toàn bộ danh sách
            //Contains(T) Kiểm tra xem có nút với giá trị dữ liệu bằng T
            //Remove(T)   Xóa nút có dữ liệu bằng T
            //RemoveFirst()   Xóa nút đầu tiên
            //RemoveLast()    Xóa nút cuối cùng
            //Find(T) Tìm một nút

            LinkedList<string> cacbaihoc = new LinkedList<string>();

            cacbaihoc.AddFirst("Bài học 3");   // thêm vào đầu danh sach
            cacbaihoc.AddLast("Bài học 4");    // thêm vào cuối
            cacbaihoc.AddFirst("Bài học 2");
            cacbaihoc.AddFirst("Bài học 1");


            // Lấy phần tử đầu tiên, sau đó duyệt đến cuối
            Console.WriteLine("---------Nút từ đầu về cuối");
            LinkedListNode<string> node = cacbaihoc.First;
            while (node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;   // node gán bằng nút sau nó
            }
            // Bài học 1
            // Bài học 2
            // Bài học 3
            // Bài học 4

            // Lấy phần tử cuối cùng, sau đó duyệt về phần tử đầu  tiên
            Console.WriteLine("--------Nút từ cuối đến đầu");
            node = cacbaihoc.Last;
            while (node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Previous;   // node gán bằng nút phía trước nó
            }
        }
        public static void testDic()
        {
            //Một đối tượng dữ liệu lưu vào Dictionary dưới dạng cặp key/ value, truy cập đến phần tử thông qua key hoặc thông qua vị trí(index) của dữ liệu trong danh sách. Chú ý, không cho phép trùng key.

            //Các thuộc tính, phương thức trong Dictionary

            //Thành viên  Diễn giải
            //Count Thuộc tính cho biết số phần tử
            //[key]   Indexer truy cập đến phần tử có key
            //Keys Thuộc tính là danh sách các key
            //Values Thuộc tính lấy danh sách các giá trị
            //Add(key, value) Thêm một phần tử vào Dictionary
            //Remove(key) Xóa phần tử bằng key của nó
            //Clear() Loại bỏ tất cả các phần tử
            //ContainKey(key) Kiểm tra có phần tử nào có khóa là key
            //ContainValue(value) Kiểm tra có phần tử nào có giá trị là value
            //Lớp SortedDictionary<Tkey,TValue> sử dụng, khai báo và khởi tạo ... giống như lớp Dictionary<Tkey,TValue>.

            // Khởi tạo với 2 phần tử
            Dictionary<string, int> sodem = new Dictionary<string, int>()
            {
                ["one"] = 1,
                ["tow"] = 2,
            };
            // Thêm hoặc cập nhật
            sodem["three"] = 3;


            var keys = sodem.Keys;
            foreach (var k in keys)
            {
                var value = sodem[k];
                Console.WriteLine($"{k} = {value}");
            }
        }

        public static void testHash()
        {
            //HashSet trong C#
            //HashSet là tập hợp danh sách không cho phép trùng giá trị.HashSet<T> khác với các collection khác là nó cung cấp cơ chế đơn giản nhất để lưu các giá trị, nó không chỉ mục thứ tự và các phần tử không sắp xếp theo thứ tự nào. HashSet<T> cung cấp hiệu năng cao cho các tác vụ tìm kiếm, thêm vào, xóa bỏ ...

            //Một số phương thức trong HashSet

            //Thành viên  Diễn giải
            //Count Thuộc tính cho biết số phần tử
            //Add(T)  Thêm phần tử vào HashSet
            //Remove(T)   Xóa phần tử khỏi HashSet
            //Clear() Xóa tất cả các phần tử
            //Contains(T) Kiểm tra xem có phần tử trong HashSet
            //IsSubsetOf(c)   Kiểm tra xem có là tập con của HashSet c
            //IsSupersetOf(c) Kiểm tra xem có chứa tập HashSet c
            //UnionWith(c)    Hợp với tập HashSet c
            //IntersectWith(c)    Giao với tập HashSet c
            //ExceptWith(c)   Loại bỏ hết các phần tử chung với c
            HashSet<int> hashset1 = new HashSet<int>() {
                5,2,3,4
            };

            Console.WriteLine($"Phần tử trong hashset1 {hashset1.Count}");
            foreach (var v in hashset1)
            {
                Console.Write(v + " ");
            }
            Console.WriteLine();

            HashSet<int> hashset2 = new HashSet<int>();
            hashset2.Add(3);
            hashset2.Add(4);
            if (hashset1.IsSupersetOf(hashset2))
                Console.WriteLine($"hashset1 là tập chứa hashset2");

        }
    }
    public class Product : IComparable<Product>, IFormattable
    {
        public int ID { set; get; }
        public string Name { set; get; }        // tên
        public double Price { set; get; }       // giá
        public string Origin { set; get; }      // xuất xứ

        public Product(int id, string name, double price, string origin)
        {
            ID = id; Name = name; Price = price; Origin = origin;
        }

        //Triển khai IComparable, cho biết vị trí sắp xếp so với đối tượng khác
        // trả về 0 - cùng vị trí; trả về > 0 đứng sau other; < 0 đứng trước trong danh sách
        public int CompareTo(Product other)
        {
            // sắp xếp về giá
            double delta = this.Price - other.Price;
            if (delta > 0)      // giá lớn hơn xếp trước
                return -1;
            else if (delta < 0) // xếp sau, giá nhỏ hơn
                return 1;
            return 0;

        }
        // Triển khai IFormattable, lấy chuỗi thông tin của đối tượng theo định dạng
        // format hỗ trợ "O" và "N"
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null) format = "O";
            switch (format.ToUpper())
            {
                case "O": // Xuất xứ trước
                    return $"Xuất xứ: {Origin} - Tên: {Name} - Giá: {Price} - ID: {ID}";
                case "N": // Tên xứ trước
                    return $"Tên: {Name} - Xuất xứ: {Origin} - Giá: {Price} - ID: {ID}";
                default: // Quăng lỗi nếu format sai
                    throw new FormatException("Không hỗ trợ format này");
            }
        }

        // Nạp chồng ToString
        override public string ToString() => $"{Name} - {Price}";

        // Quá tải thêm ToString - lấy chỗi thông tin sản phẩm theo định dạng
        public string ToString(string format) => this.ToString(format, null);
    }

    public class SearchNameProduct
    {
        string namesearch;
        public SearchNameProduct(string name)
        {
            namesearch = name;
        }
        // Hàm gán cho delegage
        public bool search(Product p)
        {
            return p.Name == namesearch;
        }
    }
}