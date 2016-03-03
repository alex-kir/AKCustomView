using System;

using Xamarin.Forms;

namespace AK
{
    public static class Extentions
    {

        public static float Distance(this PointF self, PointF other)
        {
            float xx = self.X - other.X;
            float yy = self.Y - other.Y;
            return (float)Math.Sqrt(xx * xx + yy * yy);
        }
    }
}


