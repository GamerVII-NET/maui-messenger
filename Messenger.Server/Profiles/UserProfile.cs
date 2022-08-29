using AutoMapper;
using Messenger.Domains.Dtos.Chat;
using Messenger.Domains.Dtos.User;

namespace Messenger.Server.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<User, UserReadResponse>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<ChatUserCreateDto, User>();
            CreateMap<UserConnectDto, User>();
        }
    }
}
