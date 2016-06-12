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

        public NativeCustomView(CustomViewRenderer owner)
        {
            this.owner = owner;
        }

        internal void OnRebuild()
        {
            var g = new WinPhone.Graphics(this);
            owner.Element.OnDraw(g);
        }
    }
}
