using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AK
{
    public class AKCustomView : View
    {
        public Action _invalidate;
        public AKCustomView()
        {
        }

        public virtual void OnDraw(Graphics g)
        {

        }
    
        public void Invalidate()
        {
            if (_invalidate != null)
                _invalidate();
        }
    }

}

