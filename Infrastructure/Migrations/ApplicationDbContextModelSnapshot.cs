﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.TodoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BackgroundColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ListId");

                    b.ToTable("TodoItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BackgroundColor = "#ffffff",
                            Created = new DateTime(2024, 5, 25, 13, 0, 32, 432, DateTimeKind.Utc).AddTicks(1787),
                            CreatedBy = "system",
                            IsCompleted = false,
                            ListId = 1,
                            Note = "Milk, Bread, Eggs",
                            Priority = 2,
                            Status = 0,
                            Title = "Buy groceries"
                        },
                        new
                        {
                            Id = 2,
                            BackgroundColor = "#ffffff",
                            Created = new DateTime(2024, 5, 25, 13, 0, 32, 432, DateTimeKind.Utc).AddTicks(1789),
                            CreatedBy = "system",
                            IsCompleted = false,
                            ListId = 2,
                            Note = "Due end of the week",
                            Priority = 3,
                            Status = 0,
                            Title = "Complete project report"
                        });
                });

            modelBuilder.Entity("Domain.Entities.TodoItemTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TodoItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TodoItemId");

                    b.ToTable("TodoItemTags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2024, 5, 25, 13, 0, 32, 432, DateTimeKind.Utc).AddTicks(1797),
                            CreatedBy = "system",
                            Status = 0,
                            Tag = "Shopping",
                            TodoItemId = 1
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2024, 5, 25, 13, 0, 32, 432, DateTimeKind.Utc).AddTicks(1797),
                            CreatedBy = "system",
                            Status = 0,
                            Tag = "Work",
                            TodoItemId = 2
                        });
                });

            modelBuilder.Entity("Domain.Entities.TodoList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriorityLevel")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TodoLists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2024, 5, 25, 13, 0, 32, 432, DateTimeKind.Utc).AddTicks(1721),
                            CreatedBy = "system",
                            PriorityLevel = 3,
                            Status = 0,
                            Title = "Personal"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2024, 5, 25, 13, 0, 32, 432, DateTimeKind.Utc).AddTicks(1723),
                            CreatedBy = "system",
                            PriorityLevel = 2,
                            Status = 0,
                            Title = "Work"
                        });
                });

            modelBuilder.Entity("Domain.Entities.TodoItem", b =>
                {
                    b.HasOne("Domain.Entities.TodoList", "List")
                        .WithMany("Items")
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("List");
                });

            modelBuilder.Entity("Domain.Entities.TodoItemTag", b =>
                {
                    b.HasOne("Domain.Entities.TodoItem", "TodoItem")
                        .WithMany("Tags")
                        .HasForeignKey("TodoItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TodoItem");
                });

            modelBuilder.Entity("Domain.Entities.TodoItem", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Domain.Entities.TodoList", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
