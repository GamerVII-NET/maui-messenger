using AutoMapper;
using Enums;
using Messenger.Domains.Dtos.Chat;
using Messenger.Domains.Dtos.Links;
using Messenger.Server.Repositories.ChatRepository;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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
                var inviterUser = mapper.Map<User>(chat.Inviter);
                var users = mapper.Map<IEnumerable<User>>(chat.Users);

                try
                {
                    var createdChat = await repository.CreateChat(inviterUser, chatModel, users);

                    await repository.SaveChanges();

                    return Results.Created($"/api/v1/chats/{createdChat.GlobalGuid}", mapper.Map<ChatReadDto>(createdChat));
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new
                    {
                        MessageType = "Bad Request",
                        Message = ex.Message
                    });
                }
            };
        }

        internal static Func<HttpContext, ITokenService, IChatRepository, IMapper, UserInChatDto, Task<IResult>> ExitFromChat(WebApplicationBuilder builder)
        {
            return [Authorize] async (HttpContext context, ITokenService tokenService, IChatRepository repository, IMapper mapper, UserInChatDto connectData) =>
            {
                var userModel = mapper.Map<User>(connectData.User);
                var chatModel = mapper.Map<Chat>(connectData.Chat);

                var isCurrentUser = tokenService.VerifyToken(builder, context, userModel);

                if (isCurrentUser == false) { return Results.Unauthorized(); }

                chatModel = await repository.GetChatByGuidAsync(chatModel.GlobalGuid);

                if (chatModel == null)
                    return Results.NotFound(new
                    {
                        Chat = connectData.User.GlobalGuid,
                        Message = "The chat with the specified guid was not found."
                    });

                chatModel = await repository.ExitUserFromChat(userModel, chatModel);

                await repository.SaveChanges();

                return Results.Ok(mapper.Map<ChatReadDto>(chatModel));

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

        internal static Func<HttpContext, ITokenService, IChatRepository, IMapper, UserInChatDto, Task<IResult>> JoinToChat(WebApplicationBuilder builder)
        {
            return [Authorize] async (HttpContext context, ITokenService tokenService, IChatRepository repository, IMapper mapper, UserInChatDto connectData) =>
            {
                var userModel = mapper.Map<User>(connectData.User);
                var chatModel = mapper.Map<Chat>(connectData.Chat);

                var isCurrentUser = tokenService.VerifyToken(builder, context, userModel);

                if (isCurrentUser == false) { return Results.Unauthorized(); }

                chatModel = await repository.GetChatByGuidAsync(chatModel.GlobalGuid);

                if (chatModel == null)
                    return Results.NotFound(new
                    {
                        Chat = connectData.User.GlobalGuid,
                        Message = "The chat with the specified guid was not found."
                    });

                chatModel = await repository.AddUserToChat(userModel, chatModel);

                await repository.SaveChanges();

                return Results.Ok(mapper.Map<ChatReadDto>(chatModel));

            };
        }
    }
}
