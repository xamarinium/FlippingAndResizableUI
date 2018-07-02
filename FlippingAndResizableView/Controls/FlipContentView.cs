using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlippingAndResizableView.Controls
{
    public class FlipContentView : ContentView
    {
        public static readonly BindableProperty FrontViewProperty =
            BindableProperty.Create(
                nameof(FrontView),
                typeof(View),
                typeof(FlipContentView),
                null,
                BindingMode.Default,
                null,
                FrontViewPropertyChanged);

        public static readonly BindableProperty BackViewProperty =
            BindableProperty.Create(
                nameof(BackView),
                typeof(View),
                typeof(FlipContentView),
                null,
                BindingMode.Default,
                null,
                BackViewPropertyChanged);

        private readonly Grid _contentHolder;

        private bool _isAnimationRun;

        private bool _isFlipped;

        public FlipContentView()
        {
            _contentHolder = new Grid();
            Content = _contentHolder;
        }

        public View FrontView
        {
            get => (View) GetValue(FrontViewProperty);
            set => SetValue(FrontViewProperty, value);
        }

        public View BackView
        {
            get => (View) GetValue(BackViewProperty);
            set => SetValue(BackViewProperty, value);
        }

        public bool IsFlipped
        {
            get => _isFlipped;
            set
            {
                _isFlipped = value;
                if (_isFlipped)
                    FlipFromFrontToBack();
                else
                    FlipFromBackToFront();
            }
        }

        private static void FrontViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
                ((FlipContentView) bindable)
                    ._contentHolder
                    .Children
                    .Add(((FlipContentView) bindable).FrontView);
        }

        private static void BackViewPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            //Set BackView Rotation before rotating
            if (newvalue != null)
            {
                ((FlipContentView) bindable)
                    ._contentHolder
                    .Children
                    .Add(((FlipContentView) bindable).BackView);

                ((FlipContentView) bindable).BackView.IsVisible = false;
            }
        }

        public void SetFront()
        {
            BackView.IsVisible = false;
            FrontView.IsVisible = true;
            _isFlipped = false;
        }

        private async void FlipFromFrontToBack()
        {
            _isAnimationRun = true;
            await FrontToBackRotate();

            // Change the visible content
            FrontView.IsVisible = false;
            BackView.IsVisible = true;

            await BackToFrontRotate();
            _isAnimationRun = false;
        }

        private async void FlipFromBackToFront()
        {
            _isAnimationRun = true;
            await FrontToBackRotate();

            // Change the visible content
            BackView.IsVisible = false;
            FrontView.IsVisible = true;

            await BackToFrontRotate();
            _isAnimationRun = false;
        }

        #region Animation Stuff

        private async Task<bool> FrontToBackRotate()
        {
            ViewExtensions.CancelAnimations(this);

            RotationY = 360;

            await this.RotateYTo(270, 500, Easing.Linear);

            return true;
        }

        private async Task<bool> BackToFrontRotate()
        {
            ViewExtensions.CancelAnimations(this);

            RotationY = 90;

            await this.RotateYTo(0, 500, Easing.Linear);

            return true;
        }

        #endregion
    }
}