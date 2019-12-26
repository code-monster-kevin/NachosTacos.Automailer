using Microsoft.EntityFrameworkCore;
using NachoTacos.Automailer.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        public override int SaveChanges()
        {
            UpdateEntityDates();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateEntityDates();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateEntityDates()
        {
            var now = DateTime.UtcNow;
            foreach(var changedEntity in ChangeTracker.Entries())
            {
                if (changedEntity.Entity is IUpdateable entity)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            entity.CreatedDate = now;
                            entity.UpdatedDate = now;
                            break;

                        case EntityState.Modified:
                            Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                            entity.UpdatedDate = now;
                            break;
                    }
                }
            }
        }
    }
}
