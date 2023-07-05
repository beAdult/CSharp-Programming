using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn019
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
    public class MotaAttribute : Attribute // có thể đặt tên Mota thay cho MotaAttribute
    {
        public MotaAttribute(string v) => Description = v;

        public string Description { set; get; }
    }
}
