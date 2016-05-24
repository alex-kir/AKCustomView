using System;
using AK;
using System.Linq;

namespace AKCustomView.Demo
{
    public class RoundBoxView : AK.CustomView
    {
        private static float ellipseConstant = (float)(1 - (Math.Sqrt(2) - 1) * 4 / 3);

        private float cornerRadius;

        public float CornerRadius
        {
            get{ return cornerRadius; }
            set
            {
                cornerRadius = value;
                Invalidate();
            }
        }

        private Xamarin.Forms.Color fillColor;

        public Xamarin.Forms.Color FillColor
        {
            get{ return fillColor; }
            set
            {
                fillColor = value;
                Invalidate();
            }
        }

        private Xamarin.Forms.Color borderColor;

        public Xamarin.Forms.Color BorderColor
        {
            get{ return borderColor; }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        private float borderWidth;

        public float BorderWidth
        {
            get{ return borderWidth; }
            set
            {
                borderWidth = value;
                Invalidate();
            }
        }

        public RoundBoxView()
        {
        }

        public override void OnDraw(Graphics g)
        {
            if (borderWidth > 0.1f) {
                var path = GetPath(borderWidth / 2, borderWidth / 2, (float)Width - borderWidth, (float)Height - borderWidth, cornerRadius - borderWidth / 2);
                g.FillPath(new SolidBrush(GetColor(fillColor)), path);
                g.DrawPath(new Pen(GetColor(borderColor), borderWidth), path);
            }
            else {
                var path = GetPath(0, 0, (float)Width, (float)Height, cornerRadius);
                g.FillPath(new SolidBrush(GetColor(fillColor)), path);
            }
        }

        private static AK.GraphicsPath GetPath(float x, float y, float w, float h, float r)
        {
            r = new []{ r, w / 2, h / 2 }.Min();

            //---[start point] (control point) (control point) [end point]---//
            //                                                               //
            //            (x2,y1) [x3,y1]         [x4,y1] (x5,y1)            //
            //    (x1,y2)                                         (x6,y2)    //
            //    [x1,y3]                                         [x6,y3]    //
            //                                                               //
            //    [x1,y4]                                         [x6,y4]    //
            //    (x1,y5)                                         (x6,y5)    //
            //            (x2,y6) [x3,y6]         [x4,y6] (x5,y6)            //
            //                                                               //
            //---------------------------------------------------------------//

            float x1 = x;
            float x2 = x + r * ellipseConstant;
            float x3 = x + r;
            float x4 = x + w - r;
            float x5 = x + w - r * ellipseConstant;
            float x6 = x + w;

            float y1 = y;
            float y2 = y + r * ellipseConstant;
            float y3 = y + r;
            float y4 = y + h - r;
            float y5 = y + h - r * ellipseConstant;
            float y6 = y + h;

            //------------------------

            AK.GraphicsPath path = new GraphicsPath();

            path.AddBezier(x1, y3, x1, y2, x2, y1, x3, y1);
            path.AddBezier(x4, y1, x5, y1, x6, y2, x6, y3);
            path.AddBezier(x6, y4, x6, y5, x5, y6, x4, y6);
            path.AddBezier(x3, y6, x2, y6, x1, y5, x1, y4);

            path.CloseFigure();

            return path;
        }

        private static AK.Color GetColor(Xamarin.Forms.Color color)
        {
            return Color.FromArgb((int)(255 * color.A), (int)(255 * color.R), (int)(255 * color.G), (int)(255 * color.B));
        }
    }
}

