using System;
using Android.Content;
using Android.Views;
using System.Linq;
using Android.Widget;
using Android.Graphics;

namespace AK.Droid
{
    public class NativeCustomView : View
    {
        PointF[] prevPointers = null;
        //readonly CustomViewRenderer owner;

        private readonly Action<Graphics> onDraw;
        private readonly Func<bool> isInputTransparent;
        private readonly Action<Touch[]> onTouches;

        public NativeCustomView(CustomViewRenderer owner, Context context)
            : base(context)
        {
            //this.owner = owner;
            onDraw = g => owner.Element.OnDraw(g);
            isInputTransparent = () => owner.Element.InputTransparent;
            onTouches = tt => owner.Element.OnTouch(tt);
        }

        public NativeCustomView(Action<Graphics> onDraw, Func<bool> isInputTransparent, Action<Touch[]> onTouches, Context context) : base(context)
        {
            this.onDraw = onDraw;
            this.isInputTransparent = isInputTransparent;
            this.onTouches = onTouches;

            try
            {
                this.SetLayerType(LayerType.Hardware, null);
            }
            catch
            {
            }
        }


        public void RedrawImage()
        {
            Invalidate();
        }

        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);// can I remove this line?
            var g = new AK.Droid.Graphics(canvas, this.Context);
            onDraw(g);
            //owner.Element.OnDraw(g);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            //var view = owner.Element;
            //if (view.InputTransparent)
            if (isInputTransparent())
                return false;

            try
            {
                var density = Context.Resources.DisplayMetrics.Density;
                var touches = new Touch[e.PointerCount];
                for (int i = 0; i < touches.Length; i++)
                {
                    var t = new Touch();
                    t.Id = e.GetPointerId(i);
                    t.X = e.GetX(i) / density;
                    t.Y = e.GetY(i) / density;
                    touches[i] = t;
                }

                touches[e.ActionIndex].IsDown = e.ActionMasked == MotionEventActions.Down || e.ActionMasked == MotionEventActions.PointerDown;
                touches[e.ActionIndex].IsUp = e.ActionMasked == MotionEventActions.Up || e.ActionMasked == MotionEventActions.PointerUp;
                touches[e.ActionIndex].IsCancelled = e.ActionMasked == MotionEventActions.Cancel || e.Action == MotionEventActions.Cancel;

                for (int i = 0; i < touches.Length; i++)
                {
                    var t = touches[i];
                    if (t.IsDown || prevPointers == null || prevPointers.Length == 0)
                    {
                        t.PrevX = t.X;
                        t.PrevY = t.Y;
                        continue;
                    }
                    else
                    {
                        var p = prevPointers.OrderBy(it => it.Distance(t.XY)).First();
                        t.PrevX = p.X;
                        t.PrevY = p.Y;
                    }
                }

                prevPointers = touches.Select(it => new PointF(it.X, it.Y)).ToArray();

                //view.OnTouch(touches);
                onTouches(touches);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return true;
        }
    }
}

