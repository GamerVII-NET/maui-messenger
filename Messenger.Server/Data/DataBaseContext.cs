using Microsoft.EntityFrameworkCore;

namespace Messenger.Server.Data
{
    public class DataBaseContext : DbContext
    {

        public DataBaseContext(DbContextOptions options) : base(options) { }

        public DbSet<UserModel> Users => Set<UserModel>();

    }
}
