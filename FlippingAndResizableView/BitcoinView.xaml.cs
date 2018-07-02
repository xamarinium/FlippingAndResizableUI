using System;
using System.Collections.Generic;
using FlippingAndResizableView.Abstractions;
using FlippingAndResizableView.Models;
using Xamarin.Forms;

namespace FlippingAndResizableView
{
    public partial class BitcoinView : ViewWithFlipEvent
    {
        private const string ExpandAnimationName = "ExpandAnimation";
        private const string CollapssAnimationName = "CollapssAnimation";

        public BitcoinView()
        {
            InitializeComponent();
            TransactionsListView.ItemsSource = new List<Transaction>
            {
                new Transaction(DateTime.Now.AddMinutes(-55), 522.36, 1.020, TransactionType.Sent),
                new Transaction(DateTime.Now.AddDays(-1.2), 23.05, 0.045, TransactionType.Received),
                new Transaction(DateTime.Now.AddDays(-2.5), 522.36, 1020, TransactionType.Sent),
                new Transaction(DateTime.Now.AddDays(-3.5), 23.05, 0.045, TransactionType.Received),
                new Transaction(DateTime.Now.AddDays(-4.5), 522.36, 1020, TransactionType.Sent)
            };
            TransactionsListView.ItemSelected += (sender, args) =>
            {
                TransactionsListView.SelectedItem = null;
            };
        }

        private void OnExpandExchange(object sender, EventArgs e)
        {
            if (ExchangeRow.Height.Value == 0)
            {
                GetExpandAnimation().Commit(this, ExpandAnimationName, 16,
                    100,
                    Easing.CubicIn,
                    null, () => false);
            }
            else
            {
                GetCollapssAnimation().Commit(this, CollapssAnimationName, 16,
                    100,
                    Easing.CubicInOut,
                    null, () => false);
            }
        }

        private Animation GetExpandAnimation()
        {
            return new Animation
            {
                {0, 1, new Animation(v => ExchangeRow.Height = v, 0, 100)}
            };
        }

        private Animation GetCollapssAnimation()
        {
            return new Animation
            {
                {0, 1, new Animation(v => ExchangeRow.Height = v, 100, 0)}
            };
        }
    }
}