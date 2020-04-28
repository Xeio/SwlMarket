﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SwlMarket.Data;

namespace SwlMarket.Migrations
{
    [DbContext(typeof(MarketContext))]
    partial class MarketContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SwlMarket.Models.CurrentPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("IPId")
                        .HasColumnType("int");

                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IPId");

                    b.HasIndex("ItemID");

                    b.ToTable("MostRecentPrices");
                });

            modelBuilder.Entity("SwlMarket.Models.HistoricalPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("IPId")
                        .HasColumnType("int");

                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.Property<int>("Marks")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IPId");

                    b.HasIndex("ItemID");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("SwlMarket.Models.IPEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Blocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("IP")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("IPs");
                });

            modelBuilder.Entity("SwlMarket.Models.Item", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool?>("IsExtraordinary")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ItemCategory")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("Rarity")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("SwlMarket.Models.CurrentPrice", b =>
                {
                    b.HasOne("SwlMarket.Models.IPEntry", "IP")
                        .WithMany()
                        .HasForeignKey("IPId");

                    b.HasOne("SwlMarket.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SwlMarket.Models.HistoricalPrice", b =>
                {
                    b.HasOne("SwlMarket.Models.IPEntry", "IP")
                        .WithMany()
                        .HasForeignKey("IPId");

                    b.HasOne("SwlMarket.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
