using AutoMapper;
using Messenger.Domains.Dtos.Chat;

namespace Messenger.Server.Profiles
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<Chat, ChatReadDto>();
            CreateMap<ChatNewDto, Chat>();
            CreateMap<ChatConnectDto, Chat>();
            CreateMap<ChatUser, ChatUserReadDto>();
        }
    }
}
