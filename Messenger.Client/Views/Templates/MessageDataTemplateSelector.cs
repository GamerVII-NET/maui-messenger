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

            switch (chat.MessageType)
            {
                case Enums.MessageType.Group:
                    return GroupMessengerTemplate;

                case Enums.MessageType.Direct:
                    return DirectMessengerTemplate;

                default:
                    return DirectMessengerTemplate;
            }
        }
    }
}
