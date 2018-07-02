using System;
using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using FlippingAndResizableView.Controls;
using FlippingAndResizableView.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(XNFlipView), typeof(XNFlipViewRenderer))]
namespace FlippingAndResizableView.iOS.Renderers
{
    public class XNFlipViewRenderer : ViewRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == XNFlipView.IsFlippedProperty.PropertyName)
            {
                var flipView = (XNFlipView) sender;
                if (flipView.IsFlipped)
                {
                    AnimateFlipHorizontally(NativeView, false, (double)flipView.AnimationTime/1000, () =>
                    {
                        // Change the visible content
                        flipView.FrontView.IsVisible = false;
                        flipView.BackView.IsVisible = true;

                        AnimateFlipHorizontally(NativeView, true, (double)flipView.AnimationTime/1000);
                    });
                }
                else
                {
                    AnimateFlipHorizontally(NativeView, false, (double)flipView.AnimationTime/1000, () =>
                    {
                        // Change the visible content
                        flipView.FrontView.IsVisible = true;
                        flipView.BackView.IsVisible = false;

                        AnimateFlipHorizontally(NativeView, true, (double)flipView.AnimationTime/1000);
                    });
                }
            }
        }
        
        //https://gist.github.com/aloisdeniel/3c8b82ca4babb1d79b29
        public void AnimateFlipHorizontally(UIView view, bool isIn, double duration = 0.3, Action onFinished = null)
        {
            var m34 = (nfloat)(-1 * 0.001);

            var minTransform = CATransform3D.Identity;
            minTransform.m34 = m34;
            minTransform = minTransform.Rotate((nfloat)((isIn ? 1 : -1) * Math.PI * 0.5), (nfloat)0.0f, (nfloat)1.0f, (nfloat)0.0f);
            var maxTransform = CATransform3D.Identity;
            maxTransform.m34 = m34;

            view.Layer.Transform = isIn ? minTransform : maxTransform;
            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut,
                () => {
                    view.Layer.AnchorPoint = new CGPoint((nfloat)0.5, (nfloat)0.5f);
                    view.Layer.Transform = isIn ? maxTransform : minTransform;
                },
                onFinished
            );
        }
    }
}