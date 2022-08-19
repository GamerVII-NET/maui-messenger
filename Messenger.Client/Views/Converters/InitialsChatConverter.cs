using Messenger.Domains.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.Views.Converters
{
    public class InitialsChatConverter : IMarkupExtension, IValueConverter
    {
        InitialsChatConverter _instance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var chat = value as ChatModel;

            if (chat == null) { return null; }

            if (chat.Users.Count() == 2)
            {
                var sender = chat.Users.FirstOrDefault();
                return $"{sender.LastName} {sender.FirstName}";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? new InitialsChatConverter();
        }
    }
}
