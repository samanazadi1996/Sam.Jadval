﻿// <auto-generated />
using System;
using Jadval.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Jadval.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230719124413_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Jadval.Domain.Crosswords.Entities.Crossword", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Crosswords");
                });

            modelBuilder.Entity("Jadval.Domain.Crosswords.Entities.CrosswordQuestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("CrosswordId")
                        .HasColumnType("bigint");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CrosswordId");

                    b.ToTable("CrosswordQuestions");
                });

            modelBuilder.Entity("Jadval.Domain.Crosswords.Entities.CrosswordQuestionValue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("CrosswordQuestionId")
                        .HasColumnType("bigint");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Question")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CrosswordQuestionId");

                    b.ToTable("CrosswordQuestionValues");
                });

            modelBuilder.Entity("Jadval.Domain.OutBoxEventItems.Entities.OutBoxEventItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EventName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("EventPayload")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("EventTypeName")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("OutBoxEventItems");
                });

            modelBuilder.Entity("Jadval.Domain.Crosswords.Entities.CrosswordQuestion", b =>
                {
                    b.HasOne("Jadval.Domain.Crosswords.Entities.Crossword", "Crossword")
                        .WithMany("Questions")
                        .HasForeignKey("CrosswordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Crossword");
                });

            modelBuilder.Entity("Jadval.Domain.Crosswords.Entities.CrosswordQuestionValue", b =>
                {
                    b.HasOne("Jadval.Domain.Crosswords.Entities.CrosswordQuestion", "CrosswordQuestion")
                        .WithMany("Value")
                        .HasForeignKey("CrosswordQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CrosswordQuestion");
                });

            modelBuilder.Entity("Jadval.Domain.Crosswords.Entities.Crossword", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Jadval.Domain.Crosswords.Entities.CrosswordQuestion", b =>
                {
                    b.Navigation("Value");
                });
#pragma warning restore 612, 618
        }
    }
}