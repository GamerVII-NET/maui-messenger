using System.Globalization;

namespace Messenger.Client.Views.Converters
{
    public class MessageCountConverter : IMarkupExtension, IValueConverter
    {

        MessageCountConverter _instance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int messageCount = 0;

            if (int.TryParse(value.ToString(), out messageCount) && messageCount > 0)
            {
                return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? new MessageCountConverter();
        }
    }
}
