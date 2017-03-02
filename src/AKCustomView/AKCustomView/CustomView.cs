using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AK
{
    public class CustomView : View
    {
        public Action _invalidateCallback;

        public virtual void OnDraw(Graphics g)
        {

        }
    
        public virtual void OnTouch(Touch[] touches)
        {
            
        }

        public void Invalidate()
        {
            if (_invalidateCallback != null)
                _invalidateCallback();
        }
    }

}

