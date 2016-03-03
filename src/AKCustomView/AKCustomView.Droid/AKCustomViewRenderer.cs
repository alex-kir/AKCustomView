using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;
using Android.Views;
using System.Linq;

[assembly: ExportRenderer (typeof (AK.AKCustomView), typeof (AK.Droid.AKCustomViewRenderer))]

namespace AK.Droid
{
    public class AKCustomViewRenderer : ViewRenderer
    {
        class AKCustomViewInternal : Android.Views.View
        {
            PointF[] prevPointers = null;
            readonly AKCustomViewRenderer owner;
            public AKCustomViewInternal(AKCustomViewRenderer owner, Context context)
                : base(context)
            {
                this.owner = owner;
            }

            protected override void OnDraw(Android.Graphics.Canvas canvas)
            {
                base.OnDraw(canvas);
                var view = (AK.AKCustomView)owner.Element;
                var g = new AK.Droid.Graphics(canvas, this.Context);
                view.OnDraw(g);
            }

            public override bool OnTouchEvent(MotionEvent e)
            {
                var view = (AK.AKCustomView)owner.Element;
                if (!view.UserInteractionEnabled)
                    return false;
                
                try
                {
                    var density = Context.Resources.DisplayMetrics.Density;
                    var touches = new Touch[e.PointerCount];
                    for (int i = 0; i < touches.Length; i++) {
                        var t = new Touch();
                        t.Id = e.GetPointerId(i);
                        t.X = e.GetX(i) / density;
                        t.Y = e.GetY(i) / density;
                        touches[i] = t;
                    }

                    touches[e.ActionIndex].IsDown = e.ActionMasked == MotionEventActions.Down || e.ActionMasked == MotionEventActions.PointerDown;
                    touches[e.ActionIndex].IsUp = e.ActionMasked == MotionEventActions.Up || e.ActionMasked == MotionEventActions.PointerUp;

                    for (int i = 0; i < touches.Length; i++) {
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

                    view.OnTouch(touches);
                }
                catch(Exception ex) {
                    Console.WriteLine(ex);
                }
                return true;
            }
        }

        public AKCustomViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            if (this.Control == null) {
                SetNativeControl(new AKCustomViewInternal(this, this.Context));
            }

            var control = (AKCustomViewInternal)this.Control;
            var view = (AKCustomView)e.NewElement;

            view._invalidate = control.Invalidate;

            var pp = control.LayoutParameters;
            if (pp != null) {
                pp.Width = (int)e.NewElement.WidthRequest;
                pp.Height = (int)e.NewElement.HeightRequest;
            }
            base.OnElementChanged(e);
        }
    }
}

