﻿// <auto-generated />
using System;
using ExpensesSplitter.WebApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExpensesSplitter.WebApi.Migrations
{
    [DbContext(typeof(ExpensesSplitterContext))]
    partial class ExpensesSplitterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExpensesSplitter.WebApi.Database.Models.Expense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SettlementId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("WhoPaidId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SettlementId");

                    b.HasIndex("WhoPaidId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("ExpensesSplitter.WebApi.Database.Models.Settlement", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdOwner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ownerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ownerId");

                    b.ToTable("Settlements");
                });

            modelBuilder.Entity("ExpensesSplitter.WebApi.Database.Models.SettlementUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SettlementId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SettlementUsers");
                });

            modelBuilder.Entity("ExpensesSplitter.WebApi.Database.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ExpensesSplitter.WebApi.Database.Models.Expense", b =>
                {
                    b.HasOne("ExpensesSplitter.WebApi.Database.Models.Settlement", "Settlement")
                        .WithMany()
                        .HasForeignKey("SettlementId");

                    b.HasOne("ExpensesSplitter.WebApi.Database.Models.SettlementUser", "WhoPaid")
                        .WithMany()
                        .HasForeignKey("WhoPaidId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExpensesSplitter.WebApi.Database.Models.Settlement", b =>
                {
                    b.HasOne("ExpensesSplitter.WebApi.Database.Models.User", "owner")
                        .WithMany()
                        .HasForeignKey("ownerId");
                });
#pragma warning restore 612, 618
        }
    }
}
