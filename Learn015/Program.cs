// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Learn015
{
    //    Lớp Path - Hỗ trợ làm việc với đường dẫn
    //Để hỗ trợ quản lý, tạo các đường dẫn đến file, thư mục - nhất là hỗ trợ cross-platform thì lớp tĩnh System.IO.Path chứa các phương thức(tĩnh) với mục đích đó.

    //Phương thức Ý nghĩa
    //Path.DirectorySeparatorChar Thuộc tính chứa ký tự phân cách đường dẫn thư mục (\ trên Windows, / trên* nix)
    //Path.PathSeparator Thuộc tính chứa ký tự phân chia thư mục trong biến môi trường
    //Combine Kết hợp các chuỗi thành dường dẫn
    //var path = Path.Combine("home", "ReadMe.txt"); //  "home/ReadMe.txt"
    //    ChangeExtension Thay đổi phần mở rộng của đường dẫn
    //    var path = Path.ChangeExtension("/home/abc/ReadMe.txt", "md"); //  "/home/abc/ReadMe.md"
    //GetDirectoryName Lấy đường dẫn đến file(thư mục)
    //var path = Path.GetDirectoryName("/home/abc/zyz/ReadMe.txt"); //  "/home/abc/zyz"
    //    GetExtension Lấy phần mở rộng
    //    var path = Path.GetExtension("/home/ReadMe.txt"); //  ".txt"
    //GetFileName Lấy tên file
    //var path = Path.GetFileName("/home/abc/ReadMe.txt"); //  "ReadMe.txt"
    //    GetFileNameWithoutExtension Lấy tên file
    //var path = Path.GetFileNameWithoutExtension("/home/ReadMe.txt"); //  "ReadMe"
    //    GetFullPath Lấy đường dẫn đầy đủ - từ đường dẫn tương đối
    //    var path = Path.GetFullPath("ReadMe.txt");
    //GetPathRoot Lấy gốc của đường dẫn
    //GetRandomFileName Tạo tên file ngẫu nhiên
    //var path = Path.GetRandomFileName();
    //    GetTempFileName Tạo file duy nhất, rỗng
    //var path = Path.GetTempFileName();
    //    Để lấy đường dẫn đến một số thư mục đặc biệt của hệ thống, có thể dùng phương thức Environment.GetFolderPath.Ví dụ, lấy thư mục MyDocument

    //    var path_mydoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    //Làm việc với lớp File
    //Lớp System.IO.File cung cấp cho bạn cách thức đơn giản để làm việc với các tệp.Nó có nhiều phương thức cho những mục đích khác nhau File class, như copy, xóa, di chuyển, lưu text vào file, đọc nội dung file, kiểm tra sự tồn tại, tra cứu thông tin về file...


    //File.Create(filename) tạo file
    //File.Delete(filename) xóa file
    //File.Exists(filename) kiểm tra file có tồn tại
    //File.Copy(path_src, path_des) copy file
    //File.Move(path_src, path_des) di chuyển file

    //Làm việc với lớp Directory
    //Lớp System.IO.Directory cung cấp các phương thức chuyên tương tác với các thư mục.

    //Phương thức Ý nghĩa
    //Exists(path)    Kiểm tra xem thư mục có tồn tại(true) hay không(false)
    //CreateDirectory(path)   Tạo thư mục, trả về đối tượng System.IO.DirectoryInfo chứa thông tin thư mục.
    //Delete(path)    Xóa thư mục.
    //GetFiles(path) Lấy các file trong thư mục.
    //GetDirectories(path) Lấy các thư mục trong thư mục.
    //Move(src, des)  Di chuyển thư mục.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File IO in C#");
            var directory_mydoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            String[] files = System.IO.Directory.GetFiles(directory_mydoc);
            String[] directories = System.IO.Directory.GetDirectories(directory_mydoc);

            foreach (var file in files)
            {
                Console.WriteLine(file);
            }

            foreach (var directory in directories)
            {
                Console.WriteLine(directory);
            }

            // file Fstream: 
            // https://xuanthulab.net/stream-trong-c-lam-viec-voi-filestream-lap-trinh-c-sharp.html
        }

        static void testWriteAllText()
        {
            var filename = "test.txt";
            string contentfile = "Xin chào! xuanthulab.net";

            // Lấy thư mục Document của User trên hệ thống
            var directory_mydoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var fullpath = Path.Combine(directory_mydoc, filename);
            File.WriteAllText(filename, contentfile);

            Console.WriteLine($"File lưu tại {directory_mydoc}{Path.DirectorySeparatorChar}{filename}");

        }
        static void testAppendAllText()
        {

            var filename = "test.txt";
            string contentfile = "\nXin chào! xuanthulab.net - " + DateTime.Now.ToString();

            var directory_mydoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fullpath = Path.Combine(directory_mydoc, filename);

            if (File.Exists(fullpath))
            {
                // File đã tồn tại - nối thêm nội dung
                File.AppendAllText(fullpath, contentfile);
            }
            else
            {
                // tạo mới vì chưa tồn tại file
                File.WriteAllText(fullpath, contentfile);
            }
            // Đọc nội dung File
            Console.WriteLine(fullpath);
            string s = File.ReadAllText(fullpath);
            Console.WriteLine(s);
        }
        static void ListFileDirectory(string path)
        {
            String[] directories = System.IO.Directory.GetDirectories(path);
            String[] files = System.IO.Directory.GetFiles(path);
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
            foreach (var directory in directories)
            {
                Console.WriteLine(directory);
                ListFileDirectory(directory); // Đệ quy
            }
        }
    }
    public class GetDriveInfomation
    {
        /// <summary>
        /// In các thông tin ổ đĩa trong máy
        /// </summary>
        public static void GetDrivesInfo()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  Drive type: {0}", d.DriveType);
                if (d.IsReady == true)
                {
                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine("  Available space to current user:{0, 15} bytes", d.AvailableFreeSpace);
                    Console.WriteLine("  Total available space:          {0, 15} bytes", d.TotalFreeSpace);
                    Console.WriteLine("  Total size of drive:            {0, 15} bytes ", d.TotalSize);
                }
            }
        }
    }

}