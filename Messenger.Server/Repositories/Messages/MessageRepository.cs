using Microsoft.EntityFrameworkCore;

namespace Messenger.Server.Repositories.Messages
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataBaseContext _context;

        public MessageRepository(DataBaseContext context)
        {

            _context = context;
        }

        public async Task<Message?> AddMessageAsync(User user, Chat chat, Message? replyMessage, IEnumerable<Attachment> attachments, string message)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            var checkChat = await _context.Chats
                .Include(c => c.Users)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(c => c.GlobalGuid == chat.GlobalGuid);

            if (checkChat == null) { return null; }

            var checkUserInChat = checkChat.Users.FirstOrDefault(c => c.User.GlobalGuid == user.GlobalGuid);

            if (checkUserInChat == null) { return null; }

            var newMessage = new Message
            {
                Chat = chat,
                Sender = user,
                SendingTime = DateTime.Now,
                Text = message,
                UpdatedAt = null
            };


            if (replyMessage != null)
            {
                newMessage.ReplyTo = replyMessage;
            }

            checkChat.Messages.Add(newMessage);


            return newMessage;
        }

        public void DeleteMessage(Message chat)
        {
            throw new NotImplementedException();
        }

        public async Task<Message?> UpdateMessageAsync(Message message, IEnumerable<Attachment> attachments, string text)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var messageModel = await _context.Messages.FirstOrDefaultAsync(c => c.GlobalGuid == message.GlobalGuid);

            messageModel!.UpdatedAt = DateTime.Now;
            messageModel.Text = text;

            return messageModel;
        }

        public Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Message?> GetMessageByGuidAsync(Guid guid)
        {
            return await _context.Messages.FirstOrDefaultAsync(c => c.GlobalGuid == guid);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
