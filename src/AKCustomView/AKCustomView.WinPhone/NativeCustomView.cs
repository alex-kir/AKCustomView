using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AK.WinPhone
{
    public class NativeCustomView : Canvas
    {
        readonly CustomViewRenderer owner;
        readonly List<Touch> touches = new List<Touch>();

        public NativeCustomView(CustomViewRenderer owner)
        {
            this.owner = owner;
            this.PointerPressed += NativeCustomView_PointerPressed;
            this.PointerReleased += NativeCustomView_PointerReleased;
            this.PointerCanceled += NativeCustomView_PointerCanceled;
            this.PointerCaptureLost += NativeCustomView_PointerCaptureLost;
            this.PointerEntered += NativeCustomView_PointerEntered;
            this.PointerExited += NativeCustomView_PointerExited;
            this.PointerMoved += NativeCustomView_PointerMoved;

        }

        private void NativeCustomView_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
        }

        private void NativeCustomView_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            NativeCustomView_PointerReleased(sender, e);
        }

        private void NativeCustomView_PointerCaptureLost(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            NativeCustomView_PointerReleased(sender, e);
        }

        private void NativeCustomView_PointerCanceled(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            NativeCustomView_PointerReleased(sender, e);
        }

        private void NativeCustomView_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var xy = e.GetCurrentPoint(this);
            var touch = touches.FirstOrDefault(it => it.Id == (int)e.Pointer.PointerId);
            if (touch != null)
            {
                touch.PrevX = touch.X;
                touch.PrevY = touch.Y;
                touch.X = (float)xy.Position.X;
                touch.Y = (float)xy.Position.Y;
                owner.Element.OnTouch(touches.ToArray());
            }
        }

        private void NativeCustomView_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var touch = touches.FirstOrDefault(it => it.Id == (int)e.Pointer.PointerId);
            if (touch != null)
            {
                touch.IsDown = false;
                touch.IsUp = true;
                owner.Element.OnTouch(touches.ToArray());
                touches.Remove(touch);
            }
        }

        private void NativeCustomView_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var xy = e.GetCurrentPoint(this);
            touches.Add(new Touch {
                Id = (int)e.Pointer.PointerId,
                X = (float)xy.Position.X,
                Y = (float)xy.Position.Y,
                PrevX = (float)xy.Position.X,
                PrevY = (float)xy.Position.Y,
                IsDown = true,
                IsUp = false,
            });
            owner.Element.OnTouch(touches.ToArray());
        }

        internal void OnRebuild()
        {
            var g = new WinPhone.Graphics(this);
            owner.Element.OnDraw(g);
        }

        
    }
}
