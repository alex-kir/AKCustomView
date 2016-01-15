using System;
using System.Collections.Generic;

namespace AK
{
    public struct Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public struct Size
    {
        public int Width;
        public int Height;
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }

    public struct PointF
    {
        public float X;
        public float Y;

        public PointF(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    public struct SizeF
    {
        public float Width;
        public float Height;
        public SizeF(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }

    public struct Rectangle
    {
        public readonly static Rectangle Empty = new Rectangle();
        private int x;
        private int y;
        private int width;
        private int height;

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }

        public int Left { get { return x; } }
        public int Right { get { return x + width; } }
        public int Top { get { return y; } }
        public int Bottom { get { return y + height; } }

        public Rectangle(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public Rectangle(Point location, Size size)
        {
            x = location.X;
            y = location.Y;
            width = size.Width;
            height = size.Height;
        }
    }

    public struct RectangleF
    {
        public readonly static RectangleF Empty = new RectangleF();
        private float x;
        private float y;
        private float width;
        private float height;

        public float X { get { return x; } set { x = value; } }
        public float Y { get { return y; } set { y = value; } }
        public float Width { get { return width; } set { width = value; } }
        public float Height { get { return height; } set { height = value; } }

        public float Left { get { return x; } }
        public float Right { get { return x + width; } }
        public float Top { get { return y; } }
        public float Bottom { get { return y + height; } }

        public RectangleF(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public RectangleF(PointF location, SizeF size)
        {
            x = location.X;
            y = location.Y;
            width = size.Width;
            height = size.Height;
        }
    }

    public enum GraphicsUnit
    {
        //Display,
        //Document,
        //Inch,
        //Millimeter,
        Pixel,
        //Point,
        //World,
    }

    public class Font
    {
        public string OriginalFontName { get; private set; }
        public float Size { get; private set; }
        public Font(string familyName, float emSize)
        {
            OriginalFontName = familyName;
            Size = emSize;
        }
    }

    public struct Color
    {
        private byte a;
        private byte r;
        private byte g;
        private byte b;

        public byte A { get { return a; } }
        public byte R { get { return r; } }
        public byte G { get { return g; } }
        public byte B { get { return b; } }

        public static Color FromArgb(int r, int g, int b)
        {
            return new Color(){ a = 255, r = (byte)r, g = (byte)g, b = (byte)b };
        }

        public static Color FromArgb(int a, int r, int g, int b)
        {
            return new Color(){ a = (byte)a, r = (byte)r, g = (byte)g, b = (byte)b };
        }
    }

    public class Pen
    {
        public Color Color { get; set; }
        public float Width { get; set; }

        public Pen(Color color, float width)
        {
            this.Color = color;
            this.Width = width;
        }
    }

    public abstract class Brush
    {
    }

    public class SolidBrush : Brush
    {
        public Color Color { get; set; }
        public SolidBrush(Color color)
        {
            this.Color = color;
        }
    }

    public class GraphicsPath
    {
        public class _LineSegment
        {
            public float x1; public float y1; public float x2; public float y2;
        }
        public class _BezierSegment
        {
            public float x1; public float y1; public float x2; public float y2; public float x3; public float y3; public float x4; public float y4;
        }

        public List<object> _segments = new List<object>();

        public bool _closed = false;

        public void AddBezier(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            _segments.Add(new _BezierSegment{ x1 = x1, y1 = y1, x2 = x2, y2 = y2, x3 = x3, y3 = y3, x4 = x4, y4 = y4 });
        }

        public void AddLine(float x1, float y1, float x2, float y2)
        {
            _segments.Add(new _LineSegment{ x1 = x1, y1 = y1, x2 = x2, y2 = y2 });
        }

        public void CloseFigure()
        {
            _closed = true;
        }
    }
}

