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
            if (this.Control == null && this.Tracker != null)
            {
                SetNativeControl(new NativeCustomView(this));
            }

            if (e.NewElement != null)
                e.NewElement._invalidateCallback = () => Control?.OnRebuild();

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == VisualElement.WidthProperty.PropertyName || e.PropertyName == VisualElement.HeightProperty.PropertyName)
                if (Element.Width > 0 && Element.Height > 0)
                    Element.Invalidate();
            if (e.PropertyName == "InputTransparent")
                Control.SetInputTransparent(Element.InputTransparent);
            base.OnElementPropertyChanged(sender, e);
        }
    }
}

