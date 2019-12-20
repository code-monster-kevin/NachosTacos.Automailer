using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NachoTacos.Automailer.Data
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services,
            string connectionString)
        {
            services
                .AddDbContext<AutomailerContext>(
                    options =>
                        options.UseSqlServer(connectionString)
                );
        }
    }
}
