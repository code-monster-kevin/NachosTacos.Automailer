﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NachoTacos.Automailer.Data;

namespace NachoTacos.Automailer.Data.Migrations
{
    [DbContext(typeof(AutomailerContext))]
    partial class AutomailerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NachoTacos.Automailer.Domain.AutomailerTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EmailTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EmailTemplateId");

                    b.ToTable("MailerTasks");
                });

            modelBuilder.Entity("NachoTacos.Automailer.Domain.EmailTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailSubject")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmailTemplates");
                });

            modelBuilder.Entity("NachoTacos.Automailer.Domain.EmailTemplateModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AutomailerTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AutomailerTaskId");

                    b.ToTable("EmailTemplateModels");
                });

            modelBuilder.Entity("NachoTacos.Automailer.Domain.AutomailerTask", b =>
                {
                    b.HasOne("NachoTacos.Automailer.Domain.EmailTemplate", "EmailTemplate")
                        .WithMany()
                        .HasForeignKey("EmailTemplateId");
                });

            modelBuilder.Entity("NachoTacos.Automailer.Domain.EmailTemplateModel", b =>
                {
                    b.HasOne("NachoTacos.Automailer.Domain.AutomailerTask", null)
                        .WithMany("ModelList")
                        .HasForeignKey("AutomailerTaskId");
                });
#pragma warning restore 612, 618
        }
    }
}
