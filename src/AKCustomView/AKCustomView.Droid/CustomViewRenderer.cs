using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;
using Android.Views;
using System.Linq;

using NATIVE_VIEW = AK.Droid.NativeCustomView;

[assembly: ExportRenderer (typeof (AK.CustomView), typeof (AK.Droid.CustomViewRenderer))]

namespace AK.Droid
{
    public class CustomViewRenderer : ViewRenderer<AK.CustomView, NATIVE_VIEW>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<AK.CustomView> e)
        {
            if (this.Control == null) {
                SetNativeControl(new NATIVE_VIEW(this, this.Context));
            }

            e.NewElement._invalidateCallback = Control.RedrawImage;

            var pp = Control.LayoutParameters;
            if (pp != null) {
                pp.Width = (int)e.NewElement.WidthRequest;
                pp.Height = (int)e.NewElement.HeightRequest;
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == VisualElement.WidthProperty.PropertyName || e.PropertyName == VisualElement.HeightProperty.PropertyName)
                if (Element.Width > 0 && Element.Height > 0)
                    Element.Invalidate();
            //if (e.PropertyName == "InputTransparent")
            //    Control.Clickable = !Element.InputTransparent;

            base.OnElementPropertyChanged(sender, e);
        }
    }
}

