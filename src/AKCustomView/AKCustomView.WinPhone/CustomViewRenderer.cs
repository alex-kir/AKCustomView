using System;
//using Xamarin.Forms.Platform.WinPhone;
using Xamarin.Forms;
using System.Windows;
//using System.Windows.Controls;
using System.Diagnostics;
using Xamarin.Forms.Platform.WinRT;
using System.ComponentModel;
//using CoreGraphics;

[assembly: ExportRenderer (typeof (AK.CustomView), typeof (AK.WinPhone.CustomViewRenderer))]

namespace AK.WinPhone
{
    public class CustomViewRenderer : ViewRenderer<CustomView, NativeCustomView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomView> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null) {
                SetNativeControl(new NativeCustomView(this));
            }

            //Debug.WriteLine("W:" + view.Width + ", H:" + view.Height);
            e.NewElement._invalidate = Control.OnRebuild;
            //e.NewElement.Invalidate();

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Width" || e.PropertyName == "Height")
                if (Element.Width > 0 && Element.Height > 0)
                    Element.Invalidate();
            base.OnElementPropertyChanged(sender, e);
        }
    }
}

