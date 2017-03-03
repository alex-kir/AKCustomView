using System;
using AK;
using System.Linq;

namespace Demo
{
    public class RoundBoxView : AK.CustomView
    {
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
                var radius = cornerRadius - borderWidth / 2;
                g.DrawRoundedRectangle(borderWidth / 2, borderWidth / 2, (float)Width - borderWidth, (float)Height - borderWidth,
                    radius, radius, radius, radius,
                    new Pen(borderColor.GetAKColor(), borderWidth), new SolidBrush(fillColor.GetAKColor()));
            }
            else {
                g.DrawRoundedRectangle(borderWidth / 2, borderWidth / 2, (float)Width - borderWidth, (float)Height - borderWidth,
                    cornerRadius, cornerRadius, cornerRadius, cornerRadius,
                    null, new SolidBrush(fillColor.GetAKColor()));
            }
        }
    }
}

