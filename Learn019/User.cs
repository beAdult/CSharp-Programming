using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn019
{
    [Mota("Lớp biểu diễn người dùng")]                  // thêm Attribute cho lớp
    public class User
    {
        [Mota("Thuộc tính lưu tuổi")]                   // thêm Attribute cho thuộc tính lớp
        public int age { set; get; }

        [Mota("Phương thức này hiện thị age")]          // thêm Attribute cho phương thức
        public void ShowAge() { }
    }
}
