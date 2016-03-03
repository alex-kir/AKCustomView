using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AK
{
    public class CustomView : AKCustomView
    {
    }

    public class AKCustomView : View
    {
        public bool UserInteractionEnabled { get; set; }

        public Action _invalidate;
        public AKCustomView()
        {
            UserInteractionEnabled = false;
        }

        public virtual void OnDraw(Graphics g)
        {

        }
    
        public virtual void OnTouch(Touch[] touches)
        {
            
        }

        public void Invalidate()
        {
            if (_invalidate != null)
                _invalidate();
        }
    }

}

