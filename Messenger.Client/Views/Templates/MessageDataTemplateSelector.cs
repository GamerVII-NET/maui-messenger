using Messenger.Domains.Models;

namespace Messenger.Client.Views.Templates
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DirectMessengerTemplate { get; set; }
        public DataTemplate GroupMessengerTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var chat = item as ChatModel;

            switch (chat.Type)
            {
                case Enums.ChatType.Conversation: 
                    return GroupMessengerTemplate;

                case Enums.ChatType.Direct: 
                    return DirectMessengerTemplate;

                case Enums.ChatType.Channel: 
                    return GroupMessengerTemplate;

                default: 
                    return DirectMessengerTemplate;

            }
        }
    }
}
