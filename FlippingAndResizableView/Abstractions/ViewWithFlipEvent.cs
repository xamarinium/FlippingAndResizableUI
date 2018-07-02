using System;
using Xamarin.Forms;

namespace FlippingAndResizableView.Abstractions
{
    public abstract class ViewWithFlipEvent: ContentView
    {
        public EventHandler Flip;
        
        protected void OnFlip(object sender, EventArgs e)
        {
            Flip?.Invoke(sender, e);
        }
    }
}