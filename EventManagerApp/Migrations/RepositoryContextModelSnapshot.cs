﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositories;

#nullable disable

namespace EventManagerApp.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.7");

            modelBuilder.Entity("Entities.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Spor"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Müzik"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Sinema"
                        });
                });

            modelBuilder.Entity("Entities.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("EventId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventId = 1,
                            CategoryId = 1,
                            CreatedAt = new DateTime(2025, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Çfl Halısahası",
                            EndDate = new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImgUrl = "https://example.com/image.jpg",
                            Name = "Halısaha",
                            StartDate = new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EventId = 2,
                            CategoryId = 2,
                            CreatedAt = new DateTime(2025, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Yaz Festivali",
                            EndDate = new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImgUrl = "https://example.com/image.jpg",
                            Name = "Müzik Festivali",
                            StartDate = new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EventId = 3,
                            CategoryId = 3,
                            CreatedAt = new DateTime(2025, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "F1 Filmi Açılışı",
                            EndDate = new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ImgUrl = "https://example.com/image.jpg",
                            Name = "F1 Filmi",
                            StartDate = new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Entities.Models.Key", b =>
                {
                    b.Property<int>("KeyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("KeyValue")
                        .HasColumnType("TEXT");

                    b.HasKey("KeyId");

                    b.ToTable("Keys");

                    b.HasData(
                        new
                        {
                            KeyId = 1,
                            KeyValue = "z4sbUQxWv/fbiQv4OnQYzjTwcbNTe9I9KR2DZhBPUrQ="
                        });
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Entities.Models.Event", b =>
                {
                    b.HasOne("Entities.Models.Category", "Category")
                        .WithMany("Events")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Entities.Models.Category", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
