using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CoreGraphics;

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
//                base.OnDraw(canvas);
                var view = (AK.AKCustomView)owner.Element;
                var g = new AK.iOS.Graphics(UIKit.UIGraphics.GetCurrentContext(), (float)this.Frame.Width, (float)this.Frame.Height);
                view.OnDraw(g);
            }
        }

        [Foundation.Preserve]
        public AKCustomViewRenderer()
        {
        }

        [Foundation.Preserve]
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            if (this.Control == null) {
                SetNativeControl(new AKCustomViewInternal(this));
            }

            var control = (AKCustomViewInternal)this.Control;
            var view = (AKCustomView)e.NewElement;

            view._invalidate = control.SetNeedsDisplay;

            base.OnElementChanged(e);
        }
    }
}

