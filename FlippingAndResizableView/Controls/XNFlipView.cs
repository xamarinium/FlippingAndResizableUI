using Xamarin.Forms;

namespace FlippingAndResizableView.Controls
{
    public class XNFlipView : ContentView
    {
        public static readonly BindableProperty FrontViewProperty =
            BindableProperty.Create(
                nameof(FrontView),
                typeof(View),
                typeof(XNFlipView),
                null,
                BindingMode.Default,
                null,
                FrontViewPropertyChanged);

        public static readonly BindableProperty BackViewProperty =
            BindableProperty.Create(
                nameof(BackView),
                typeof(View),
                typeof(XNFlipView),
                null,
                BindingMode.Default,
                null,
                BackViewPropertyChanged);


        public static readonly BindableProperty IsFlippedProperty =
            BindableProperty.Create(
                nameof(IsFlipped),
                typeof(bool),
                typeof(XNFlipView),
                false,
                BindingMode.Default);
        
        public static readonly BindableProperty AnimationTimeProperty =
            BindableProperty.Create(
                nameof(AnimationTime),
                typeof(int),
                typeof(XNFlipView),
                250,
                BindingMode.Default);

        private readonly Grid _contentHolder;

        public XNFlipView()
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
            get => (bool) GetValue(IsFlippedProperty);
            set => SetValue(IsFlippedProperty, value);
        }
        
        public int AnimationTime
        {
            get => (int) GetValue(AnimationTimeProperty);
            set => SetValue(AnimationTimeProperty, value);
        }

        private static void FrontViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
                ((XNFlipView) bindable)
                    ._contentHolder
                    .Children
                    .Add(((XNFlipView) bindable).FrontView);
        }

        private static void BackViewPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            //Set BackView Rotation before rotating
            if (newvalue != null)
                ((XNFlipView) bindable)
                    ._contentHolder
                    .Children
                    .Insert(0, ((XNFlipView) bindable).BackView);
        }
    }
}