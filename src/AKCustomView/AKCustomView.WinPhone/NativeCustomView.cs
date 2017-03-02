using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace AK.WinPhone
{
    public class NativeCustomView : Grid
    {
        readonly Canvas internalCanvas;
        readonly Canvas internalHandler;
        readonly CustomViewRenderer renderer;
        readonly List<Touch> touches = new List<Touch>();

        public NativeCustomView(CustomViewRenderer renderer)
        {
            this.renderer = renderer;

            this.Children.Add(internalCanvas = new Canvas());
            this.Children.Add(internalHandler = new Canvas() { Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0)) });

            SetInputTransparent(renderer.Element.InputTransparent);
        }

        internal void SetInputTransparent(bool yes)
        {
            if (yes)
                Unsubscribe();
            else
                Subscribe();
        }

        internal void Subscribe()
        {
            internalHandler.PointerPressed += NativeCustomView_PointerPressed;
            internalHandler.PointerReleased += NativeCustomView_PointerReleased;
            internalHandler.PointerCanceled += NativeCustomView_PointerCanceled;
            internalHandler.PointerCaptureLost += NativeCustomView_PointerCaptureLost;
            internalHandler.PointerEntered += NativeCustomView_PointerEntered;
            internalHandler.PointerExited += NativeCustomView_PointerExited;
            internalHandler.PointerMoved += NativeCustomView_PointerMoved;
        }

        internal void Unsubscribe()
        {
            internalHandler.PointerPressed -= NativeCustomView_PointerPressed;
            internalHandler.PointerReleased -= NativeCustomView_PointerReleased;
            internalHandler.PointerCanceled -= NativeCustomView_PointerCanceled;
            internalHandler.PointerCaptureLost -= NativeCustomView_PointerCaptureLost;
            internalHandler.PointerEntered -= NativeCustomView_PointerEntered;
            internalHandler.PointerExited -= NativeCustomView_PointerExited;
            internalHandler.PointerMoved -= NativeCustomView_PointerMoved;
        }

        private void NativeCustomView_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
        }

        private void NativeCustomView_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            NativeCustomView_PointerCanceled(sender, e);
        }

        private void NativeCustomView_PointerCaptureLost(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            NativeCustomView_PointerCanceled(sender, e);
        }

        private void NativeCustomView_PointerCanceled(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var touch = touches.FirstOrDefault(it => it.Id == (int)e.Pointer.PointerId);
            if (touch != null)
            {
                touch.PrevX = touch.X;// xz?
                touch.PrevY = touch.Y;// xz?
                touch.IsDown = false;
                touch.IsUp = false;
                touch.IsCancelled = true;
                renderer.Element.OnTouch(touches.ToArray());
                touches.Remove(touch);
            }
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
                touch.IsDown = false;
                touch.IsUp = false;
                touch.IsCancelled = false;
                renderer.Element.OnTouch(touches.ToArray());
            }
        }

        private void NativeCustomView_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var touch = touches.FirstOrDefault(it => it.Id == (int)e.Pointer.PointerId);
            if (touch != null)
            {
                touch.PrevX = touch.X;// xz?
                touch.PrevY = touch.Y;// xz?
                touch.IsDown = false;
                touch.IsUp = true;
                touch.IsCancelled = false;
                renderer.Element.OnTouch(touches.ToArray());
                touches.Remove(touch);
            }
        }

        private void NativeCustomView_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var xy = e.GetCurrentPoint(this);
            touches.Add(new Touch
            {
                Id = (int)e.Pointer.PointerId,
                X = (float)xy.Position.X,
                Y = (float)xy.Position.Y,
                PrevX = (float)xy.Position.X,
                PrevY = (float)xy.Position.Y,
                IsDown = true,
                IsUp = false,
                IsCancelled = false,
            });
            renderer.Element.OnTouch(touches.ToArray());
        }

        internal void OnRebuild()
        {
            try
            {
                if (renderer.Element.Width > 0 && renderer.Element.Height > 0)
                {
                    var width = Math.Max(0, renderer.Element.Width);
                    var height = Math.Max(0, renderer.Element.Height);

                    this.Width = width;
                    this.Height = height;
                    internalCanvas.Width = width;
                    internalCanvas.Height = height;
                    internalHandler.Width = width;
                    internalHandler.Height = height;

                    var g = new WinPhone.Graphics(this.internalCanvas);
                    renderer.Element.OnDraw(g);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


    }
}
