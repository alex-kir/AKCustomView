using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AK.WinPhone
{
    public class Graphics : AK.Graphics
    {
        Canvas canvas;

        public Graphics(Canvas canvas)
        {
            this.canvas = canvas;
            canvas.Children.Clear();
        }

        public override void DrawString(string s, Font font, Brush brush, float x, float y)
        {

            TextBlock textBlock = new TextBlock();

            textBlock.Text = s;

            textBlock.Foreground = ToSolidColorBrush(((SolidBrush)brush).Color);

            Canvas.SetLeft(textBlock, x);

            Canvas.SetTop(textBlock, y);

            canvas.Children.Add(textBlock);

            //UpdateIOSContext(font, brush, null);
            //new NSString(s).DrawString(new CGPoint(x, y), UIKit.UIFont.SystemFontOfSize(font.Size));
        }

        public override void FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            var rect = new System.Windows.Shapes.Rectangle();
            rect.Stroke = ToSolidColorBrush(((SolidBrush)brush).Color);
            rect.Fill = ToSolidColorBrush(((SolidBrush)brush).Color);
            rect.Width = width;
            rect.Height = height;
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            canvas.Children.Add(rect);



            //canvas.Children.Add(new System.Windows.Shapes.Rectangle() { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2, Stroke = ToSolidColorBrush(pen.Color) });
            //UpdateIOSContext(null, brush, null);
            //context.FillRect(new CoreGraphics.CGRect((nfloat)x, (nfloat)y, (nfloat)width, (nfloat)height));
        }

        public override void DrawLine(AK.Pen pen, float x1, float y1, float x2, float y2)
        {
            canvas.Children.Add(new Line() { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2, Stroke = ToSolidColorBrush(pen.Color) });
            //UpdateIOSContext(null, null, pen);
            //context.AddLines(new CGPoint[] {
            //            new CGPoint(x1, y1),
            //            new CGPoint(x2, y2),
            //        });
            //context.StrokePath();
        }

////        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
////        {
////            var cgRect = new CGRect(destRect.X, -destRect.Y - destRect.Height, destRect.Width, destRect.Height);
////            context.ScaleCTM((nfloat)1.0, (nfloat)(-1.0));
////            context.DrawImage(cgRect, image.NativeImage.CGImage);
////            context.ScaleCTM((nfloat)1.0, (nfloat)(-1.0));
////            //            image.NativeImage.Draw(ToCGRect(destRect));
////
////        }

//        public SizeF MeasureString(String text, Font font)
//        {
//            CGSize ret;
//            var str = new NSString (text);
//            if (UIKit.UIDevice.CurrentDevice.CheckSystemVersion (7, 0)) { 
//                var attributes = new UIStringAttributes { Font = UIKit.UIFont.SystemFontOfSize (font.Size) };
//                ret = str.GetSizeUsingAttributes (attributes);
//            } else {
//                ret = str.StringSize (UIFont.SystemFontOfSize (font.Size));
//            }
//            return new SizeF((float)ret.Width, (float)ret.Height);
//        }

//        CGRect ToCGRect(RectangleF rect)
//        {
//            return new CGRect(rect.X, rect.Y, rect.Width, rect.Height);
//        }

        System.Windows.Media.SolidColorBrush ToSolidColorBrush(Color color)
        {
            return new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
        }

//        void UpdateIOSContext(Font font, Brush brush, Pen pen)
//        {
//            if (font != null) {
//                context.SetFontSize(font.Size);
//                //                paint.TextSize = font.Size;
//                //                paint.SetTypeface(Typeface.Default);//TODO
//            }

//            if (brush != null) {
//                context.SetFillColor(ToCGColor(((SolidBrush)brush).Color));
//                context.SetFillColor(ToCGColor(((SolidBrush)brush).Color));
//                //                CGContextSetRGBStrokeColor(context, 1.0, 0.0, 0.0, 1.0);
//                //                CGContextSelectFont(context, "Helvetica", 20, kCGEncodingMacRoman);

//                //                paint.Color = ToAndroidColor(((SolidBrush)brush).Color);
//                //                paint.SetStyle(Android.Graphics.Paint.Style.Fill);
//            }

//            if (pen != null) {
//                context.SetStrokeColor (ToCGColor(pen.Color));
//                context.SetLineWidth(pen.Width);
//                //                paint.Color = ToAndroidColor(pen.Color);
//                //                paint.StrokeWidth = pen.Width;
//                //                paint.SetStyle(Android.Graphics.Paint.Style.Stroke);
//            }

//            //            paint.TextAlign = Android.Graphics.Paint.Align.Left;
//            //            paint.Flags = (Android.Graphics.PaintFlags)0;
//            //            paint.AntiAlias = true;
//            //            paint.FilterBitmap = true;
//            //            paint.Dither = true;
//        }
    }
}

