using AutoMapper;
using Messenger.Domains.Dtos.Messages;
using Messenger.Server.Repositories.ChatRepository;
using Messenger.Server.Repositories.Messages;
using Messenger.Server.Repositories.UserRepository;

namespace Messenger.Server.Services
{
    public class MessageService
    {
        internal static Func<HttpContext, IUserRepository, IMessageRepository, IChatRepository, ITokenService, IMapper, MessageCreateDto, Task<IResult>> SendMessage(WebApplicationBuilder builder)
        {
            return [Authorize] async (
                HttpContext context,
                IUserRepository userRepository,
                IMessageRepository messageRepository,
                IChatRepository chatRepository,
                ITokenService tokenService,
                IMapper mapper,
                MessageCreateDto messageData) =>
            {

                var user = mapper.Map<User>(messageData.Sender);
                var chat = mapper.Map<Chat>(messageData.Chat);
                var replyMessage = mapper.Map<Message>(messageData.ReplyTo);
                var attachments = mapper.Map<IEnumerable<Attachment>>(messageData.Attachments);
                var message = mapper.Map<string>(messageData.Text);

                var isCurrentUser = tokenService.VerifyToken(builder, context, user);

                if (isCurrentUser == false) { return Results.Unauthorized(); }

                var userModel = await userRepository.GetUserByGuidAsync(user.GlobalGuid);

                if (userModel == null)
                {
                    return Results.NotFound(new
                    {
                        Message = "User not found"
                    });
                }

                var chatModel = await chatRepository.GetChatByGuidAsync(chat.GlobalGuid);

                if (chatModel == null)
                {
                    return Results.NotFound(new
                    {
                        Message = "Chat not found"
                    });
                }

                var replyMessageModel = await messageRepository.GetMessageByGuidAsync(replyMessage.GlobalGuid);

                var newMessage = await messageRepository.AddMessageAsync(userModel, chatModel, replyMessageModel, attachments, message);

                await messageRepository.SaveChangesAsync();

                return Results.Created($"/api/v1/messages/{newMessage!.GlobalGuid}", mapper.Map<MessageReadDto>(newMessage));

            };
        }

        internal static Func<HttpContext, ITokenService, IUserRepository, IMessageRepository, IMapper, MessageEditDto, Task<IResult>> EditMessage(WebApplicationBuilder builder)
        {
            return [Authorize] async (
                HttpContext context,
                ITokenService tokenService,
                IUserRepository userRepository,
                IMessageRepository messageRepository,
                IMapper mapper,
                MessageEditDto messageData) =>
            {

                var user = mapper.Map<User>(messageData.Sender);
                var attachments = mapper.Map<IEnumerable<Attachment>>(messageData.Attachments);
                var message = mapper.Map<string>(messageData.Text);

                var messageModel = await messageRepository.GetMessageByGuidAsync(messageData.GlobalGuid);

                if (messageModel == null)
                {
                    return Results.NotFound(new
                    {
                        Message = "Message not found."
                    });
                }

                var isCurrentUser = tokenService.VerifyToken(builder, context, user);

                if (isCurrentUser == false) { return Results.Unauthorized(); }

                var userModel = await userRepository.GetUserByGuidAsync(user.GlobalGuid);

                if (userModel == null)
                {
                    return Results.NotFound(new
                    {
                        Message = "User not found"
                    });
                }

                var editedMessage = await messageRepository.UpdateMessageAsync(messageModel, attachments, message);

                await messageRepository.SaveChangesAsync();

                return Results.Ok(mapper.Map<MessageReadDto>(editedMessage));


            };
        }
    }
}
