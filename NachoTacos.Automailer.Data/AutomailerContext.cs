using Microsoft.EntityFrameworkCore;
using NachoTacos.Automailer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NachoTacos.Automailer.Data
{
    public class AutomailerContext : DbContext
    {
        public AutomailerContext(DbContextOptions<AutomailerContext> options) : base(options)
        {

        }

        public DbSet<AutomailerTask> MailerTasks { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailTemplateModel> EmailTemplateModels { get; set; }
    }
}
