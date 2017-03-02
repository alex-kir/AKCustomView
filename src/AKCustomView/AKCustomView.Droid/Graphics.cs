using System;
using Android.Graphics;
using Android.Content;

namespace AK.Droid
{
    public class Graphics : AK.Graphics
    {
        readonly Canvas canvas;
        readonly Paint paint = new Paint();//PaintFlags.AntiAlias);
        readonly Rect bounds = new Rect();
        readonly float density;

        public Graphics(Canvas canvas, Context context)
        {
            this.canvas = canvas;
            density = context.Resources.DisplayMetrics.Density;
        }

        public override void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            UpdateAndroidPaint(font, brush, null);
            var text = s ?? "";
            var tmp = MeasureString(text, font);
            var tmp2 = paint.GetFontMetrics().Bottom / density;
            canvas.DrawText(text, x * density, (y + tmp.Height) * density - tmp2, paint);
        }

        public override SizeF MeasureString(String text, Font font)
        {
            UpdateAndroidPaint(font, null, null);
            var s = text ?? "";
            return new SizeF(paint.MeasureText(s, 0, s.Length) / density, font.Size);
        }

        //public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
        //{
        //    UpdateAndroidPaint(null, null, null);
        //    var src = new Rect((int)srcRect.Left, (int)srcRect.Top, (int)srcRect.Right, (int)srcRect.Bottom);
        //    var dst = new Rect((int)(destRect.Left * density), (int)(destRect.Top * density), (int)(destRect.Right * density), (int)(destRect.Bottom * density));
        //    canvas.DrawBitmap(image.AndroidBitmap, src, dst, paint);
        //}

        public override void FillRectangle(Brush brush, float x, float y, float width, float height )
        {
            UpdateAndroidPaint(null, brush, null);
            canvas.DrawRect(x * density, y * density, (x + width) * density, (y + height) * density, paint);
        }

        public override void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            UpdateAndroidPaint(null, null, pen);
            canvas.DrawLine(x1 * density, y1 * density, x2 * density, y2 * density, paint);
        }

        public override void DrawPath(Pen pen, GraphicsPath path)
        {   
            UpdateAndroidPaint(null, null, pen);
            AndroidDrawPath(path);
        }

        public override void FillPath(Brush brush, GraphicsPath path)
        {
            UpdateAndroidPaint(null, brush, null);
            AndroidDrawPath(path);
        }

        private void AndroidDrawPath(GraphicsPath p)
        {    
            var path = new Android.Graphics.Path();
            float d = density;

            for (int i = 0; i < p._segments.Count; i++) {
                {
                    var ln = p._segments[i] as GraphicsPath._LineSegment;
                    if (ln != null) {
                        if (i == 0)
                            path.MoveTo(ln.x1 * d, ln.y1 * d);
                        else
                            path.LineTo(ln.x1 * d, ln.y1 * d);
                        path.LineTo(ln.x2 * d, ln.y2 * d);
                        continue;
                    }
                }
                {
                    var bz = p._segments[i] as GraphicsPath._BezierSegment;
                    if (bz != null) {
                        if (i == 0)
                            path.MoveTo(bz.x1 * d, bz.y1 * d);
                        else
                            path.LineTo(bz.x1 * d, bz.y1 * d);
                    
                        path.CubicTo(
                            bz.x2 * d, bz.y2 * d,
                            bz.x3 * d, bz.y3 * d,
                            bz.x4 * d, bz.y4 * d);
                        continue;
                    }
                }
            }
            if (p._closed)
                path.Close();
            canvas.DrawPath(path, paint);
        }

        void UpdateAndroidPaint(Font font, Brush brush, Pen pen)
        {
            if (font != null) {
                paint.TextSize = font.Size * density;
                if (font.Bold)
                    paint.SetTypeface(Typeface.DefaultBold);
                else
                    paint.SetTypeface(Typeface.Default);
            }

            if (brush != null) {
                paint.Color = ToAndroidColor(((SolidBrush)brush).Color);
                paint.SetStyle(Paint.Style.Fill);
            }

            if (pen != null) {
                paint.Color = ToAndroidColor(pen.Color);
                paint.StrokeWidth = pen.Width;
                paint.SetStyle(Paint.Style.Stroke);
            }

            paint.TextAlign = Paint.Align.Left;
            paint.Flags = (PaintFlags)0;
            paint.AntiAlias = true;
            paint.FilterBitmap = true;
            paint.Dither = true;
        }

        Android.Graphics.Color ToAndroidColor(Color color)
        {
            return new Android.Graphics.Color(color.R, color.G, color.B, color.A);
        }
    }
}

