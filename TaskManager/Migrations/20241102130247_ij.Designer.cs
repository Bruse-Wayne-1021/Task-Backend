﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManager.DBContext;

#nullable disable

namespace TaskManager.Migrations
{
    [DbContext(typeof(TaskContext))]
    [Migration("20241102130247_ij")]
    partial class ij
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskManager.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Addressline1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Addressline2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Userid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Userid")
                        .IsUnique()
                        .HasFilter("[Userid] IS NOT NULL");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("TaskManager.Models.CheckList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<bool>("isDone")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("CheckList");
                });

            modelBuilder.Entity("TaskManager.Models.TaskItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssigneeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskManager.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskManager.Models.UserRegistration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserRegistration");
                });

            modelBuilder.Entity("TaskManager.Models.Address", b =>
                {
                    b.HasOne("TaskManager.Models.User", "user")
                        .WithOne("Address")
                        .HasForeignKey("TaskManager.Models.Address", "Userid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("user");
                });

            modelBuilder.Entity("TaskManager.Models.CheckList", b =>
                {
                    b.HasOne("TaskManager.Models.TaskItems", "TaskItems")
                        .WithMany("CheckList")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaskItems");
                });

            modelBuilder.Entity("TaskManager.Models.TaskItems", b =>
                {
                    b.HasOne("TaskManager.Models.User", "Assigee")
                        .WithMany("TaskItems")
                        .HasForeignKey("AssigneeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assigee");
                });

            modelBuilder.Entity("TaskManager.Models.TaskItems", b =>
                {
                    b.Navigation("CheckList");
                });

            modelBuilder.Entity("TaskManager.Models.User", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("TaskItems");
                });
#pragma warning restore 612, 618
        }
    }
}