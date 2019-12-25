using Microsoft.EntityFrameworkCore;
using NachoTacos.Automailer.Domain;

namespace NachoTacos.Automailer.Data
{
    public class AutomailerContext : DbContext
    {
        public AutomailerContext(DbContextOptions<AutomailerContext> options) : base(options)
        {

        }

        public DbSet<EmailTask> EmailTasks { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailModel> EmailModels { get; set; }
        public DbSet<EmailTaskModel> EmailTaskModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmailTaskModel>()
                .HasKey(c => new { c.EmailTaskId, c.EmailModelId });

        }
    }
}
