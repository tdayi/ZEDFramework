using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.Enumeration
{
    public class EnumDescription : DescriptionAttribute
    {
        public readonly bool IsViewable;

        public EnumDescription(string description)
            : base(description)
        {
            this.IsViewable = true;
        }

        public EnumDescription(string description, bool isViewable)
            : base(description)
        {
            this.IsViewable = isViewable;
        }
    }
}
