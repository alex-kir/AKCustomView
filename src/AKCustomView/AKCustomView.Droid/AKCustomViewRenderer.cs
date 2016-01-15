using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;

[assembly: ExportRenderer (typeof (AK.AKCustomView), typeof (AK.Droid.AKCustomViewRenderer))]

namespace AK.Droid
{
    public class AKCustomViewRenderer : ViewRenderer
    {
        class AKCustomViewInternal : Android.Views.View
        {
            readonly AKCustomViewRenderer owner;
            public AKCustomViewInternal(AKCustomViewRenderer owner, Context context)
                : base(context)
            {
                this.owner = owner;
            }

            protected override void OnDraw(Android.Graphics.Canvas canvas)
            {
                base.OnDraw(canvas);
                var view = (AK.AKCustomView)owner.Element;
                var g = new AK.Droid.Graphics(canvas, this.Context);
                view.OnDraw(g);
            }
        }

        public AKCustomViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            if (this.Control == null) {
                SetNativeControl(new AKCustomViewInternal(this, this.Context));
            }

            var control = (AKCustomViewInternal)this.Control;
            var view = (AKCustomView)e.NewElement;

            view._invalidate = control.Invalidate;

            var pp = control.LayoutParameters;
            if (pp != null) {
                pp.Width = (int)e.NewElement.WidthRequest;
                pp.Height = (int)e.NewElement.HeightRequest;
            }
            base.OnElementChanged(e);
        }
    }
}

