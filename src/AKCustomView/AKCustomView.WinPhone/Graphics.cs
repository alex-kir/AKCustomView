using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

using UIColor = Windows.UI.Color;
using UIBrush = Windows.UI.Xaml.Media.Brush;
using UISolidColorBrush = Windows.UI.Xaml.Media.SolidColorBrush;

using UIPoint = Windows.Foundation.Point;

using UILine = Windows.UI.Xaml.Shapes.Line;
using UIRectangle = Windows.UI.Xaml.Shapes.Rectangle;
using UIPath = Windows.UI.Xaml.Shapes.Path;


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
            var rect = new UIRectangle();
            rect.Stroke = ToSolidColorBrush(((SolidBrush)brush).Color);
            rect.Fill = ToSolidColorBrush(((SolidBrush)brush).Color);
            rect.Width = width;
            rect.Height = height;
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            canvas.Children.Add(rect);
        }

        public override void DrawLine(AK.Pen pen, float x1, float y1, float x2, float y2)
        {
            canvas.Children.Add(new UILine() { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2, Stroke = ToSolidColorBrush(pen.Color) });
        }

        private static UIPath GetUIPath(GraphicsPath path)
        {
            var uipath = new UIPath();

            var geometry = new PathGeometry();
            var figure = new PathFigure();
            bool first = true;
            foreach (var obj in path._segments)
            {
                var line = obj as GraphicsPath._LineSegment;
                if (line != null)
                {
                    if (first)
                    {
                        first = false;
                        figure.StartPoint = new UIPoint(line.x1, line.y1);
                    }

                    figure.Segments.Add(new LineSegment() { Point = new UIPoint(line.x1, line.y1) });
                    figure.Segments.Add(new LineSegment() { Point = new UIPoint(line.x2, line.y2) });
                    continue;
                }

                var bezier = obj as GraphicsPath._BezierSegment;
                if (bezier != null)
                {
                    if (first)
                    {
                        first = false;
                        figure.StartPoint = new UIPoint(bezier.x1, bezier.y1);
                    }

                    figure.Segments.Add(new LineSegment() { Point = new UIPoint(bezier.x1, bezier.y1) });
                    figure.Segments.Add(new BezierSegment()
                    {
                        Point1 = new UIPoint(bezier.x2, bezier.y2),
                        Point2 = new UIPoint(bezier.x3, bezier.y3),
                        Point3 = new UIPoint(bezier.x4, bezier.y4),
                    });
                    continue;
                }
            }
            figure.IsClosed = path._closed;
            geometry.Figures.Add(figure);
            uipath.Data = geometry;

            return uipath;
        }

        public override void DrawPath(Pen pen, GraphicsPath path)
        {
            var uipath = GetUIPath(path);
            uipath.StrokeThickness = pen.Width;
            uipath.Stroke = ToSolidColorBrush(pen.Color);
            canvas.Children.Add(uipath);
        }

        public override void FillPath(Brush brush, GraphicsPath path)
        {
            var uipath = GetUIPath(path);
            uipath.Stroke = ToSolidColorBrush(((SolidBrush)brush).Color);
            uipath.Fill = ToSolidColorBrush(((SolidBrush)brush).Color);
            canvas.Children.Add(uipath);
        }

        UIBrush ToSolidColorBrush(Color color)
        {
            return new UISolidColorBrush(UIColor.FromArgb(color.A, color.R, color.G, color.B));
        }

    }
}

