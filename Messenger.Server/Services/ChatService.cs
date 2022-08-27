using AutoMapper;
using Messenger.Domains.Dtos.Chat;
using Messenger.Server.Repositories.ChatRepository;

namespace Messenger.Server.Services
{
    public class ChatService
    {
        internal static RequestDelegate AddUserToChat()
        {
            throw new NotImplementedException();
        }

        internal static Func<IChatRepository, IMapper, ChatCreateDto, Task<IResult>> CreateChat()
        {
            return [Authorize] async (IChatRepository repository, IMapper mapper, ChatCreateDto chat) =>
            {
                var chatModel = mapper.Map<Chat>(chat.Chat);
                var userModel = mapper.Map<User>(chat.User);

                var createdChat = await repository.CreateChat(userModel, chatModel);

                await repository.SaveChanges();

                return Results.Created($"/api/v1/chats/{createdChat.GlobalGuid}", mapper.Map<ChatReadDto>(createdChat));
            };
        }

        internal static Func<IChatRepository, IMapper, Task<IResult>> GetChatsList()
        {
            return [Authorize] async (IChatRepository repository, IMapper mapper) =>
            {
                var chats = await repository.GetAllChatsAsync();

                return Results.Ok(mapper.Map<IEnumerable<ChatReadDto>>(chats));
            };
        }
    }
}
