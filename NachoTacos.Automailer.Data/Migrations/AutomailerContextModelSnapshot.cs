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

            modelBuilder.Entity("NachoTacos.Automailer.Domain.EmailModel", b =>
                {
                    b.Property<Guid>("EmailModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmailModelId");

                    b.ToTable("EmailModels");
                });

            modelBuilder.Entity("NachoTacos.Automailer.Domain.EmailTask", b =>
                {
                    b.Property<Guid>("EmailTaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmailTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmailTaskId");

                    b.HasIndex("EmailTemplateId");

                    b.ToTable("EmailTasks");
                });

            modelBuilder.Entity("NachoTacos.Automailer.Domain.EmailTaskModel", b =>
                {
                    b.Property<Guid>("EmailTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmailModelId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmailTaskId", "EmailModelId");

                    b.HasIndex("EmailModelId");

                    b.ToTable("EmailTaskModels");
                });

            modelBuilder.Entity("NachoTacos.Automailer.Domain.EmailTemplate", b =>
                {
                    b.Property<Guid>("EmailTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailSubject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmailTemplateId");

                    b.ToTable("EmailTemplates");
                });

            modelBuilder.Entity("NachoTacos.Automailer.Domain.EmailTask", b =>
                {
                    b.HasOne("NachoTacos.Automailer.Domain.EmailTemplate", "EmailTemplate")
                        .WithMany()
                        .HasForeignKey("EmailTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NachoTacos.Automailer.Domain.EmailTaskModel", b =>
                {
                    b.HasOne("NachoTacos.Automailer.Domain.EmailModel", "EmailModel")
                        .WithMany()
                        .HasForeignKey("EmailModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NachoTacos.Automailer.Domain.EmailTask", "EmailTask")
                        .WithMany()
                        .HasForeignKey("EmailTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
