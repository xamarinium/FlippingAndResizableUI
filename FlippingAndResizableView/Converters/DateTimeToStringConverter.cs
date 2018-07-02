using System;
using System.Globalization;
using Xamarin.Forms;

namespace FlippingAndResizableView.Converters
{
    public class DateTimeToStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTimeOffset offset)
            {
                return ToTimeSinceString(offset.DateTime);
            }
            
            if (value is DateTime dateTime)
            {
                return ToTimeSinceString(dateTime);
            }
            
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
        private string ToTimeSinceString(DateTime value)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - value.Ticks);
            double seconds = ts.TotalSeconds;

            if (seconds < 1 * HOUR)
                return $"{ts.Minutes}m ago";
            
            if (seconds < 24 * HOUR)
                return $"{ts.Hours}h {ts.Minutes}m ago";

            if (seconds < 48 * HOUR)
                return $"Yesterday";

            if (seconds < 30 * DAY)
                return ts.Days + "d ago";
            

            if (seconds < 12 * MONTH) {
                int months = (int)(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }

            int years = (int)(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
    }
}