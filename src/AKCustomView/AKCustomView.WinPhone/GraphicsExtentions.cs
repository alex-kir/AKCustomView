using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK
{
    public static class GraphicsExtentions
    {
        public static AK.SizeF ToAKSize(this Windows.Foundation.Size self)
        {
            return new AK.SizeF((float)self.Width, (float)self.Height);
        }
    }
}
