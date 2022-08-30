using AutoMapper;
using Messenger.Domains.Dtos.Messages;

namespace Messenger.Server.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageCreateDto, Message>();
            CreateMap<Message, MessageReadDto>();
            CreateMap<Message, MessageChatReadDto>();
        }
    }
}
