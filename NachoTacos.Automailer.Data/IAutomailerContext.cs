using Microsoft.EntityFrameworkCore;
using NachoTacos.Automailer.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace NachoTacos.Automailer.Data
{
    public interface IAutomailerContext
    {

        DbSet<AutomailerModel> AutomailerModels { get; set; }
        DbSet<AutomailerTask> AutomailerTasks { get; set; }
        DbSet<Campaign> Campaigns { get; set; }
        DbSet<CampaignActivity> CampaignActivities { get; set; }
        DbSet<CampaignContact> CampaignContacts { get; set; }
        DbSet<CampaignSetting> CampaignSettings { get; set; }
        DbSet<CampaignTracking> CampaignTrackings { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<EmailTemplate> EmailTemplates { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}