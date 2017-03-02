using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CoreGraphics;
using UIKit;
using System.Linq;
using System.Diagnostics;

[assembly: ExportRenderer (typeof (AK.CustomView), typeof (AK.iOS.CustomViewRenderer))]

namespace AK.iOS
{
    [Foundation.Preserve]
    public class CustomViewRenderer : ViewRenderer<AK.CustomView, NativeCustomView>
    {
        [Foundation.Preserve]
        public CustomViewRenderer()
        {
        }

        [Foundation.Preserve]
        protected override void OnElementChanged(ElementChangedEventArgs<AK.CustomView> e)
        {
            Debug.WriteLine("OnElementChanged:" + e.NewElement + ", " + this.Control);
            if (e.NewElement != null)
            {
                if (this.Control == null)
                {
                    SetNativeControl(new NativeCustomView(this));
                }

                if (this.Control != null && e.NewElement != null)
                {
                    e.NewElement._invalidate = this.Control.SetNeedsDisplay;
                }
            }
            base.OnElementChanged(e);
        }
    }
}

