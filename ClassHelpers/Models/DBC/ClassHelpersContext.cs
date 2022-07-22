using ClassHelpers.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ClassHelpers.Models.DBC
{
    public class ClassHelpersContext : DbContext
    {
        public ClassHelpersContext()
        {
        }

        public ClassHelpersContext(DbContextOptions<ClassHelpersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMessage> GroupMessages { get; set; }
    }
}
