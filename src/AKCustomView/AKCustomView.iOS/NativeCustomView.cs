using System;
using CoreGraphics;
using UIKit;
using System.Linq;

namespace AK.iOS
{
    [Foundation.Preserve]
    public class NativeCustomView : UIKit.UIView
    {
        readonly CustomViewRenderer owner;
        [Foundation.Preserve]
        public NativeCustomView(CustomViewRenderer owner)
        {
            this.owner = owner;
            this.UserInteractionEnabled = true;
            this.Opaque = false;
        }

        [Foundation.Preserve]
        public override void Draw(CGRect rect)
        {
            var g = new AK.iOS.Graphics(UIKit.UIGraphics.GetCurrentContext(), (float)this.Frame.Width, (float)this.Frame.Height);
            owner.Element.OnDraw(g);
        }

        public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
        {
            ProcessTouches(evt);
            base.TouchesBegan(touches, evt);
        }

        public override void TouchesMoved(Foundation.NSSet touches, UIEvent evt)
        {
            ProcessTouches(evt);
            base.TouchesMoved(touches, evt);
        }

        public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
        {
            ProcessTouches(evt);
            base.TouchesEnded(touches, evt);
        }

        public override void TouchesCancelled(Foundation.NSSet touches, UIEvent evt)
        {
            ProcessTouches(evt);
            base.TouchesCancelled(touches, evt);
        }

        bool ProcessTouches(UIEvent evt)
        {
            var view = (AK.CustomView)owner.Element;
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
}

