using CommunityToolkit.Mvvm.ComponentModel;
using Enums;
using Messenger.Domains.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.Views.Converters
{
    public class GetChatsConverter : IMarkupExtension, IValueConverter
    {
        GetChatsConverter _instance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var chatModels = value as ObservableCollection<ChatModel>;
            //var chatType = (MessageType)int.Parse(parameter.ToString());

            //return chatModels.Where(c => c.MessageType == chatType);

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? new GetChatsConverter();
        }
    }
}
