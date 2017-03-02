using System;
using Foundation;
using AK;
using UIKit;
using CoreGraphics;
using ObjCRuntime;
using System.Diagnostics;

namespace AK.iOS
{
    public class Graphics : AK.Graphics
    {
        CoreGraphics.CGContext context;
        CGColorSpace colorspace;

        public Graphics(CoreGraphics.CGContext context, float width, float height)
        {
            this.context = context;
            this.colorspace = CGColorSpace.CreateDeviceRGB();
        }

        public override void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            UpdateIOSContext(font, brush, null);
            //var weight = font.Bold ? UIFontWeight.Bold : UIFontWeight.Regular;
            var f = font.Bold ? UIFont.BoldSystemFontOfSize(font.Size) : UIFont.SystemFontOfSize(font.Size);
            new NSString(s ?? "").DrawString(new CGPoint(x, y), f);
        }

        public override SizeF MeasureString(String text, Font font)
        {
            CGSize ret;

            var str = new NSString(text ?? "");
            var f = font.Bold ? UIFont.BoldSystemFontOfSize(font.Size) : UIFont.SystemFontOfSize(font.Size);
            if (UIKit.UIDevice.CurrentDevice.CheckSystemVersion(7, 0))
            {
                var attributes = new UIStringAttributes { Font = f };
                ret = str.GetSizeUsingAttributes(attributes);
            }
            else {
                ret = str.StringSize(f);
            }
            return new SizeF((float)ret.Width, (float)ret.Height);
        }

        public override void FillRectangle(Brush brush, float x, float y, float width, float height )
        {
            UpdateIOSContext(null, brush, null);
            context.FillRect(new CoreGraphics.CGRect((nfloat)x, (nfloat)y, (nfloat)width, (nfloat)height));
        }

        public override void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            UpdateIOSContext(null, null, pen);
            context.AddLines(new CGPoint[] {
                new CGPoint(x1, y1),
                new CGPoint(x2, y2),
            });
            context.StrokePath ();
        }

        public override void DrawPath(Pen pen, GraphicsPath path)
        {
            UpdateIOSContext(null, null, pen);
            var bz = UIKit.UIBezierPath.FromPath(iOSCreatePath(path));
            bz.LineWidth = pen.Width;
            bz.Stroke();
        }

        public override void FillPath(Brush brush, GraphicsPath path)
        {
            UpdateIOSContext(null, brush, null);
            var bz = UIKit.UIBezierPath.FromPath(iOSCreatePath(path));
            bz.Fill();
        }

        CGPath iOSCreatePath(GraphicsPath p)
        {
            CGPath path = new CGPath();

            for (int i = 0; i < p._segments.Count; i++) {
                {
                    var ln = p._segments[i] as GraphicsPath._LineSegment;
                    if (ln != null) {
                        if (i == 0)
                            path.MoveToPoint(ln.x1, ln.y1);
                        else
                            path.AddLineToPoint(ln.x1, ln.y1);
                        path.AddLineToPoint(ln.x2, ln.y2);
                        continue;
                    }
                }
                {
                    var bz = p._segments[i] as GraphicsPath._BezierSegment;
                    if (bz != null) {
                        if (i == 0)
                            path.MoveToPoint(bz.x1, bz.y1);
                        else
                            path.AddLineToPoint(bz.x1, bz.y1);

                        path.AddCurveToPoint(
                            bz.x2, bz.y2,
                            bz.x3, bz.y3,
                            bz.x4, bz.y4);
                        continue;
                    }
                }
            }
            if (p._closed)
                path.CloseSubpath();
            
            return path;
        }

//        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
//        {
//            var cgRect = new CGRect(destRect.X, -destRect.Y - destRect.Height, destRect.Width, destRect.Height);
//            context.ScaleCTM((nfloat)1.0, (nfloat)(-1.0));
//            context.DrawImage(cgRect, image.NativeImage.CGImage);
//            context.ScaleCTM((nfloat)1.0, (nfloat)(-1.0));
//            //            image.NativeImage.Draw(ToCGRect(destRect));
//
//        }

        CGRect ToCGRect(RectangleF rect)
        {
            return new CGRect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        CGColor ToCGColor(Color color)
        {
            return new CoreGraphics.CGColor(colorspace, new nfloat[] { color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f });
        }

        void UpdateIOSContext(Font font, Brush brush, Pen pen)
        {
            if (font != null) {
                context.SetFontSize(font.Size);
            }

            if (brush != null) {
                context.SetFillColor(ToCGColor(((SolidBrush)brush).Color));
                context.SetFillColor(ToCGColor(((SolidBrush)brush).Color));
            }

            if (pen != null) {
                context.SetStrokeColor (ToCGColor(pen.Color));
                context.SetLineWidth(pen.Width);
            }
        }
    }
}

