using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;
using Android.Views;
using System.Linq;

[assembly: ExportRenderer (typeof (AK.CustomView), typeof (AK.Droid.CustomViewRenderer))]

namespace AK.Droid
{
    public class CustomViewRenderer : ViewRenderer<AK.CustomView, NativeCustomView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<AK.CustomView> e)
        {
            if (this.Control == null) {
                SetNativeControl(new NativeCustomView(this, this.Context));
            }

            e.NewElement._invalidate = Control.Invalidate;

            var pp = Control.LayoutParameters;
            if (pp != null) {
                pp.Width = (int)e.NewElement.WidthRequest;
                pp.Height = (int)e.NewElement.HeightRequest;
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}

