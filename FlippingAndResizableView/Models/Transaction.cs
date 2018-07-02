

using System;

namespace FlippingAndResizableView.Models
{
    public class Transaction
    {
        public Transaction()
        { 
        }
        
        public Transaction(DateTime time, double currency, double currencyBtc, TransactionType type)
        {
            Time = time;
            Currency = currency;
            CurrencyBtc = currencyBtc;
            Type = type;
        }
        
        public DateTime Time { get; set; }
        public double Currency { get; set; }
        public double CurrencyBtc { get; set; }
        public TransactionType Type { get; set; }

        public string TypeAndCurrencyBtc => $"{Type} {CurrencyBtc} BTC";
    }

    public enum TransactionType
    {
        Sent,
        Received
    }
}