using System.Globalization;

namespace Messenger.Client.Views.Converters
{
    public class LastMessageDateConverter : IMarkupExtension, IValueConverter
    {

        LastMessageDateConverter _instatce;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return null;

            var time = DateTime.Parse(value.ToString());

            if (DateTime.Now.ToString("dd.MM.yyyy") == time.ToString("dd.MM.yyyy"))
            {
                return time.ToString("HH:mm");
            }

            if (DateTime.Today.AddDays(-1).ToString("dd.MM.yyyy") == time.ToString("dd.MM.yyyy"))
            {
                return "Yesterday";
            }

            return time.ToString("dd.MM.yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instatce ?? new LastMessageDateConverter();
        }
    }
}
