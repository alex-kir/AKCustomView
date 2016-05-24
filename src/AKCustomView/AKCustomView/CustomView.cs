using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AK
{
    public class AKCustomView : CustomView
    {
    }

    public class CustomView : View
    {
        public static readonly BindableProperty UserInteractionEnabledProperty = BindableProperty.Create("UserInteractionEnabled", typeof(bool), typeof(CustomView), false);

        public bool UserInteractionEnabled
        {
            get
            {
                return (bool)base.GetValue(UserInteractionEnabledProperty);
            }
            set
            {
                base.SetValue(UserInteractionEnabledProperty, value);
            }
        }

        public Action _invalidate;

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

