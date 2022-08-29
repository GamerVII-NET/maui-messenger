using Messenger.Domains.Dtos.ChatUser;
using System.Globalization;

namespace Messenger.Client.Views.Converters
{
    public class InitialsChatConverter : IMarkupExtension, IValueConverter
    {
        InitialsChatConverter _instance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var chat = value as ChatUserChatsReadDto;

            if (chat == null) { return null; }

            return chat.Chat.Name;
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
