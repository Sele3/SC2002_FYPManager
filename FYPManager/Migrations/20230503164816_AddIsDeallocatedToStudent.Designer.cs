﻿// <auto-generated />
using FYPManager.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FYPManager.Migrations
{
    [DbContext(typeof(FYPMContext))]
    [Migration("20230503164816_AddIsDeallocatedToStudent")]
    partial class AddIsDeallocatedToStudent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FYPManager.Entity.Projects.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectID"));

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StudentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SupervisorID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectID");

                    b.HasIndex("StudentID")
                        .IsUnique()
                        .HasFilter("[StudentID] IS NOT NULL");

                    b.HasIndex("SupervisorID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("FYPManager.Entity.Users.Student", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeallocated")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("FYPManager.Entity.Users.Supervisor", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Supervisors");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Supervisor");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("FYPManager.Entity.Users.Coordinator", b =>
                {
                    b.HasBaseType("FYPManager.Entity.Users.Supervisor");

                    b.HasDiscriminator().HasValue("Coordinator");
                });

            modelBuilder.Entity("FYPManager.Entity.Projects.Project", b =>
                {
                    b.HasOne("FYPManager.Entity.Users.Student", "Student")
                        .WithOne("Project")
                        .HasForeignKey("FYPManager.Entity.Projects.Project", "StudentID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FYPManager.Entity.Users.Supervisor", "Supervisor")
                        .WithMany("Projects")
                        .HasForeignKey("SupervisorID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("FYPManager.Entity.Users.Student", b =>
                {
                    b.Navigation("Project");
                });

            modelBuilder.Entity("FYPManager.Entity.Users.Supervisor", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}