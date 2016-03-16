using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CoreGraphics;
using UIKit;
using System.Linq;

[assembly: ExportRenderer (typeof (AK.AKCustomView), typeof (AK.iOS.AKCustomViewRenderer))]

namespace AK.iOS
{
    [Foundation.Preserve]
    public class AKCustomViewRenderer : ViewRenderer
    {
        [Foundation.Preserve]
        class AKCustomViewInternal : UIKit.UIView
        {
            readonly AKCustomViewRenderer owner;
            [Foundation.Preserve]
            public AKCustomViewInternal(AKCustomViewRenderer owner)
            {
                this.owner = owner;
                this.UserInteractionEnabled = true;
                this.Opaque = false;
            }

            [Foundation.Preserve]
            public override void Draw(CGRect rect)
            {
                var view = (AK.AKCustomView)owner.Element;
                var g = new AK.iOS.Graphics(UIKit.UIGraphics.GetCurrentContext(), (float)this.Frame.Width, (float)this.Frame.Height);
                view.OnDraw(g);
            }

            public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
            {
                if (!ProcessTouches(touches, evt))
                    base.TouchesBegan(touches, evt);
            }

            public override void TouchesMoved(Foundation.NSSet touches, UIEvent evt)
            {
                if (!ProcessTouches(touches, evt))
                    base.TouchesMoved(touches, evt);
            }

            public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
            {
                if (!ProcessTouches(touches, evt))
                    base.TouchesEnded(touches, evt);
            }

            public override void TouchesCancelled(Foundation.NSSet touches, UIEvent evt)
            {
                if (!ProcessTouches(touches, evt))
                    base.TouchesCancelled(touches, evt);
            }

            bool ProcessTouches(Foundation.NSSet touches, UIEvent evt)
            {
                var view = (AK.AKCustomView)owner.Element;
                if (!view.UserInteractionEnabled)
                    return false;
                
                var tt = evt.AllTouches.Cast<UITouch>().Select(it => new AK.Touch{
                    Id = it.GetHashCode(), // TODO
                    IsDown = it.Phase == UITouchPhase.Began,
                    IsUp = it.Phase == UITouchPhase.Ended || it.Phase == UITouchPhase.Cancelled,
                    X = (float)it.LocationInView(this).X,
                    Y = (float)it.LocationInView(this).Y,
                    PrevX = (float)it.PreviousLocationInView(this).X,
                    PrevY = (float)it.PreviousLocationInView(this).Y
                }).ToArray();

                view.OnTouch(tt);
                return true;
            }

        }

        [Foundation.Preserve]
        public AKCustomViewRenderer()
        {
        }

        [Foundation.Preserve]
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            if (e.NewElement != null)
            {
                if (this.Control == null)
                {
                    SetNativeControl(new AKCustomViewInternal(this));
                }

                var control = this.Control as AKCustomViewInternal;
                var view = e.NewElement as AKCustomView;
                if (control != null && view != null)
                {
                    view._invalidate = control.SetNeedsDisplay;
                }
            }
            base.OnElementChanged(e);
        }
    }
}

