using System;
using Xamarin.Forms.Platform.WinPhone;
using Xamarin.Forms;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
//using CoreGraphics;

[assembly: ExportRenderer (typeof (AK.AKCustomView), typeof (AK.WinPhone.AKCustomViewRenderer))]

namespace AK.WinPhone
{
    
    public class AKCustomViewRenderer : ViewRenderer
    {

        class AKCustomViewInternal : Canvas
        {
            readonly AKCustomViewRenderer owner;

            public AKCustomViewInternal(AKCustomViewRenderer owner)
            {
                this.owner = owner;
            }

            internal void OnRebuild()
            {
                var g = new WinPhone.Graphics(this);
                var view = (AK.AKCustomView)owner.Element;
                view.OnDraw(g);
            }
        }

       
        public AKCustomViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            if (this.Control == null) {
                SetNativeControl(new AKCustomViewInternal(this));
            }

            var control = (AKCustomViewInternal)this.Control;
            var view = (AKCustomView)e.NewElement;
            Debug.WriteLine("W:" + view.Width + ", H:" + view.Height);
            //control.Width = view.WidthRequest;
            //control.Height = view.HeightRequest;
            view._invalidate = control.OnRebuild;
            
            view.Invalidate();

            base.OnElementChanged(e);
        }
    }
}

