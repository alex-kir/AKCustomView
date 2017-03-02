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
            if (e.NewElement != null)
            {
                if (this.Control == null)
                {
                    SetNativeControl(new NativeCustomView(this));
                }

                if (this.Control != null && e.NewElement != null)
                {
                    e.NewElement._invalidateCallback = this.Control.SetNeedsDisplay;
                }
            }
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == VisualElement.WidthProperty.PropertyName ||
                e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.IsVisibleProperty.PropertyName
               )
            {
                if (Element.IsVisible && Control != null)
                    Control.SetNeedsDisplay();
            }
            base.OnElementPropertyChanged(sender, e);
        }
    }
}

