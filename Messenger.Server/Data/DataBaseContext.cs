using Microsoft.EntityFrameworkCore;

namespace Messenger.Server.Data
{
    public class DataBaseContext : DbContext
    {

        public DataBaseContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Chat> Chats => Set<Chat>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Attachment> Attachments => Set<Attachment>();
        public DbSet<ChatUser> ChatUsers => Set<ChatUser>();


    }
}
