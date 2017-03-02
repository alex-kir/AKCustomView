using System;

using Xamarin.Forms;

namespace AK
{
    public class Touch
    {
        public int Id { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public PointF XY { get { return new PointF(X,Y); } }

        public float PrevX { get; set; }
        public float PrevY { get; set; }
        public PointF PrevXY { get { return new PointF(X,Y); } }

        public bool IsDown { get; set; }
        public bool IsUp { get; set; }
        public bool IsCancelled { get; set; }

        public override string ToString()
        {
            return string.Format("[Touch: {0:0}, {1:0}; {2} {3}]", X, Y, (IsDown ? "D" : "") + (IsUp ? "U" : "") + (IsCancelled ? "C" : ""), Id);
        }
    }
}


