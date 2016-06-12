﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK
{
    public static class GraphicsExtensions
    {
        private static float ellipseConstant = (float)(1 - (Math.Sqrt(2) - 1) * 4 / 3);

        internal static AK.Color GetAKColor(this Xamarin.Forms.Color color)
        {
            return Color.FromArgb((int)(255 * color.A), (int)(255 * color.R), (int)(255 * color.G), (int)(255 * color.B));
        }

        public static void RoundedRect(this Graphics g,
            float x, float y, float w, float h,
            float lt, float rt, float rb, float lb,
            Pen pen, Brush brush)
        {
            float borderWidth = pen == null ? 0 : pen.Width;
            float bw2 = borderWidth / 2;
            var path = GetPath(x + bw2, y + bw2, w - borderWidth, h - borderWidth,
                lt - bw2, rt - bw2, rb - bw2, lb - bw2);
            if (brush != null)
                g.FillPath(brush, path);
            if (pen != null)
                g.DrawPath(pen, path);
        }

        private static GraphicsPath GetPath(float x, float y, float w, float h, float lt, float rt, float rb, float lb)
        {
            //            r = new []{ r, w / 2, h / 2 }.Min();

            //-------[start point] (control point) (control point) [end point]-------//
            //                                                                       //
            //            (tx2,ty1) [tx3,ty1]         [tx4,ty1] (tx5,ty1)            //
            //    (lx1,ly2)                                             (rx6,ry2)    //
            //    [lx1,ly3]                                             [rx6,ry3]    //
            //                                                                       //
            //    [lx1,ly4]                                             [rx6,ry4]    //
            //    (lx1,ly5)                                             (rx6,ry5)    //
            //            (bx2,by6) [bx3,by6]         [bx4,by6] (bx5,by6)            //
            //                                                                       //
            //-----------------------------------------------------------------------//

            float lx1 = x;

            float ly2 = y + lt * ellipseConstant;
            float ly3 = y + lt;
            float ly4 = y + h - lb;
            float ly5 = y + h - lb * ellipseConstant;

            float tx2 = x + lt * ellipseConstant;
            float tx3 = x + lt;
            float tx4 = x + w - rt;
            float tx5 = x + w - rt * ellipseConstant;

            float ty1 = y;

            float rx6 = x + w;

            float ry2 = y + rt * ellipseConstant;
            float ry3 = y + rt;
            float ry4 = y + h - rb;
            float ry5 = y + h - rb * ellipseConstant;

            float bx2 = x + lb * ellipseConstant;
            float bx3 = x + lb;
            float bx4 = x + w - rb;
            float bx5 = x + w - rb * ellipseConstant;

            float by6 = y + h;

            //------------------------

            GraphicsPath path = new GraphicsPath();

            path.AddBezier(lx1, ly3, lx1, ly2, tx2, ty1, tx3, ty1);
            path.AddBezier(tx4, ty1, tx5, ty1, rx6, ry2, rx6, ry3);
            path.AddBezier(rx6, ry4, rx6, ry5, bx5, by6, bx4, by6);
            path.AddBezier(bx3, by6, bx2, by6, lx1, ly5, lx1, ly4);

            path.CloseFigure();

            return path;
        }
    }
}
