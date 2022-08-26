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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatUser>()
                .HasKey(c => c.GlobalGuid);

            modelBuilder.Entity<User>()
                .HasKey(c => c.GlobalGuid);


            // One chat -> More messages
            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Messages)
                .WithOne(c => c.Chat);

            // One message -> More attachments
            modelBuilder.Entity<Message>()
                .HasMany(c => c.Attachments)
                .WithOne(c => c.Message);

            // One user -> More ChatUsers
            modelBuilder.Entity<User>()
                .HasMany(c => c.UserChats)
                .WithOne(c => c.User);

            // More chat -> More users
            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Users)
                .WithOne(c => c.Chat);


        }


    }
}
