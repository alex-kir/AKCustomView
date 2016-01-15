using System;

namespace AK
{
    public class Graphics
    {
        //public SmoothingMode SmoothingMode { get; set; } // TODO

        public virtual void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            throw new NotImplementedException();
        }

        public virtual void DrawString(string s, Font font, Brush brush, PointF point)
        {
            DrawString(s, font, brush, point.X, point.Y);
        }

        public virtual void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            throw new NotImplementedException();
        }

        public virtual void DrawLine(Pen pen, Point pt1, Point pt2)
        {
            DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }

//        public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
//        {
//            DrawImage(image, new RectangleF(destRect.X, destRect.Y, destRect.Width, destRect.Height),
//                new RectangleF(srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height), srcUnit);
//        }
            
        public virtual void FillRectangle(Brush brush, Rectangle rect)
        {
            FillRectangle(brush, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }

        public virtual void FillRectangle(Brush brush, RectangleF rect)
        {
            FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public virtual void FillRectangle(Brush brush, int x, int y, int width, int height)
        {
            FillRectangle(brush, (float)x, (float)y, (float)width, (float)height);
        }

        public virtual void FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public virtual void FillPath(Brush brush, GraphicsPath path)
        {
            throw new NotImplementedException();
        }

        public virtual void DrawPath(Pen pen, GraphicsPath path)
        {
            throw new NotImplementedException();
        }
    }
}

