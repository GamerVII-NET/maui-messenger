namespace Messenger.Server.Repositories.Messages
{
    public interface IMessageRepository
    {
        Task SaveChangesAsync();

        Task<Message?> GetMessageByGuidAsync(Guid guid);

        Task<Message?> AddMessageAsync(User user, Chat chat, Message? replyMessage, IEnumerable<Attachment> attachments, string message);

        Task<Message?> UpdateMessageAsync(Message message, IEnumerable<Attachment> attachments, string text);

        Task<IEnumerable<Message>> GetAllMessagesAsync();

        void DeleteMessage(Message chat);
    }
}
