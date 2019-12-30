using FluentEmail.Core;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using NachoTacos.Automailer.Data;

namespace NachosTacos.Automailer.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            NachoTacos.Automailer.Data.Startup
                .ConfigureServices(services, Configuration.GetConnectionString("AutomailerConnection"));
            services.AddTransient<IAutomailerContext, AutomailerContext>();

            services
                .AddLogging();

            services
                .AddFluentEmail(Configuration.GetSection("smtp").GetValue<string>("defaultFromEmail"))
                .AddRazorRenderer()
                .AddSmtpSender(Configuration.GetSection("smtp").GetValue<string>("host"), 25);
            services.AddTransient<IFluentEmailFactory, FluentEmailFactory>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NachosTacos Automailer API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("AutomailerConnection")));
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "NachosTacos Automailer API");
                c.RoutePrefix = string.Empty;  // in development environment
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();
        }
    }
}
