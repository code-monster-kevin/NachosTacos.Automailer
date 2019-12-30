using Microsoft.EntityFrameworkCore;
using NachoTacos.Automailer.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NachoTacos.Automailer.Data
{

    public class AutomailerContext : DbContext, IAutomailerContext
    {
        public AutomailerContext(DbContextOptions<AutomailerContext> options) : base(options)
        {

        }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignActivity> CampaignActivities { get; set; }
        public DbSet<CampaignContact> CampaignContacts { get; set; }
        public DbSet<CampaignSetting> CampaignSettings { get; set; }
        public DbSet<CampaignTracking> CampaignTrackings { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<EmailModel> EmailModels { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Campaign>()
                .HasData(
                    Campaign.Create(Guid.Parse("905C3CBE-E2AF-4323-ADD6-6B2350501DA7"), "DEF", "Default Campaign")
                );

            modelBuilder.Entity<CampaignContact>()
                .HasKey(c => new { c.CampaignId, c.ContactId });

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
            foreach (var changedEntity in ChangeTracker.Entries())
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
