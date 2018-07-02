using System;
using Xamarin.Forms;

namespace FlippingAndResizableView
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnFlip(object sender, EventArgs e)
        {
            FlipView.IsFlipped = !FlipView.IsFlipped;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BitcoinFrontView.Flip += OnFlip;
            RequestBitcoinsBackView.Flip += OnFlip;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            BitcoinFrontView.Flip -= OnFlip;
            RequestBitcoinsBackView.Flip -= OnFlip;
        }
    }
}
