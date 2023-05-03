﻿// <auto-generated />
using System;
using FYPManager.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FYPManager.Migrations
{
    [DbContext(typeof(FYPMContext))]
    partial class FYPMContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("FYPManager.Entity.Requests.AllocateProjectRequest", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestID"));

                    b.Property<string>("AllocateToStudentID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsSeen")
                        .HasColumnType("bit");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("RequestStatus")
                        .HasColumnType("int");

                    b.HasKey("RequestID");

                    b.HasIndex("AllocateToStudentID");

                    b.HasIndex("ProjectID");

                    b.ToTable("AllocateProjectRequests");
                });

            modelBuilder.Entity("FYPManager.Entity.Requests.DeallocateProjectRequest", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestID"));

                    b.Property<string>("DeallocateStudentID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsSeen")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RequestAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("RequestStatus")
                        .HasColumnType("int");

                    b.HasKey("RequestID");

                    b.HasIndex("DeallocateStudentID");

                    b.ToTable("DeallocateProjectRequests");
                });

            modelBuilder.Entity("FYPManager.Entity.Requests.TitleChangeRequest", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestID"));

                    b.Property<bool>("IsSeen")
                        .HasColumnType("bit");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestByStudentID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RequestStatus")
                        .HasColumnType("int");

                    b.Property<string>("RequestToSupervisorID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("RequestByStudentID");

                    b.HasIndex("RequestToSupervisorID");

                    b.ToTable("TitleChangeRequests");
                });

            modelBuilder.Entity("FYPManager.Entity.Requests.TransferStudentRequest", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestID"));

                    b.Property<bool>("IsSeen")
                        .HasColumnType("bit");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("RequestStatus")
                        .HasColumnType("int");

                    b.Property<string>("TransferFromSupervisorID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TransferToSupervisorID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RequestID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("TransferFromSupervisorID");

                    b.HasIndex("TransferToSupervisorID");

                    b.ToTable("TransferStudentRequests");
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

            modelBuilder.Entity("FYPManager.Entity.Requests.AllocateProjectRequest", b =>
                {
                    b.HasOne("FYPManager.Entity.Users.Student", "AllocateTo")
                        .WithMany()
                        .HasForeignKey("AllocateToStudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FYPManager.Entity.Projects.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AllocateTo");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("FYPManager.Entity.Requests.DeallocateProjectRequest", b =>
                {
                    b.HasOne("FYPManager.Entity.Users.Student", "DeallocateStudent")
                        .WithMany()
                        .HasForeignKey("DeallocateStudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeallocateStudent");
                });

            modelBuilder.Entity("FYPManager.Entity.Requests.TitleChangeRequest", b =>
                {
                    b.HasOne("FYPManager.Entity.Projects.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FYPManager.Entity.Users.Student", "RequestBy")
                        .WithMany()
                        .HasForeignKey("RequestByStudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FYPManager.Entity.Users.Supervisor", "RequestTo")
                        .WithMany()
                        .HasForeignKey("RequestToSupervisorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("RequestBy");

                    b.Navigation("RequestTo");
                });

            modelBuilder.Entity("FYPManager.Entity.Requests.TransferStudentRequest", b =>
                {
                    b.HasOne("FYPManager.Entity.Projects.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FYPManager.Entity.Users.Supervisor", "TransferFrom")
                        .WithMany()
                        .HasForeignKey("TransferFromSupervisorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FYPManager.Entity.Users.Supervisor", "TransferTo")
                        .WithMany()
                        .HasForeignKey("TransferToSupervisorID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("TransferFrom");

                    b.Navigation("TransferTo");
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
