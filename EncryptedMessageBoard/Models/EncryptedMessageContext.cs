using Microsoft.EntityFrameworkCore;

namespace EncryptedMessageBoard.Models
{
    public class EncryptedMessageContext : DbContext
    {
        public EncryptedMessageContext(DbContextOptions<EncryptedMessageContext> options) : base(options) { }

        public DbSet<EncryptedMessage> EncryptedMessages { get; set; }
    }
}
