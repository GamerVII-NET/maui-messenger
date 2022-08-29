using Enums;
using Messenger.Domains.Dtos.ChatUser;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Messenger.Client.Views.Converters
{
    public class GetChatsConverter : IMarkupExtension, IValueConverter
    {
        GetChatsConverter _instance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var chatModels = value as ObservableCollection<ChatUserChatsReadDto>;
            var chatType = (ChatType)int.Parse(parameter.ToString());

            return chatModels.Where(c => c.Chat.Type == chatType);
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
