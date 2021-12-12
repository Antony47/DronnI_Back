﻿// <auto-generated />
using System;
using DronnI_Back.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DronnI_Back.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20211211163218_avd")]
    partial class avd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DronnI_Back.Models.DbModels.Backup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatingTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Backups");
                });

            modelBuilder.Entity("DronnI_Back.Models.DbModels.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DroneId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DroneId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("DronnI_Back.Models.DbModels.Drone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("X")
                        .HasColumnType("float");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Drones");
                });

            modelBuilder.Entity("DronnI_Back.Models.DbModels.Rent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("DroneId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OperatorId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StatisticId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DroneId");

                    b.HasIndex("OperatorId");

                    b.HasIndex("StatisticId");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("DronnI_Back.Models.DbModels.Statistic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("DronnI_Back.Models.DbModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DronnI_Back.Models.DbModels.Category", b =>
                {
                    b.HasOne("DronnI_Back.Models.DbModels.Drone", null)
                        .WithMany("Categories")
                        .HasForeignKey("DroneId");
                });

            modelBuilder.Entity("DronnI_Back.Models.DbModels.Drone", b =>
                {
                    b.HasOne("DronnI_Back.Models.DbModels.User", "Owner")
                        .WithMany("Drones")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DronnI_Back.Models.DbModels.Rent", b =>
                {
                    b.HasOne("DronnI_Back.Models.DbModels.User", "Customer")
                        .WithMany("CustomerRent")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DronnI_Back.Models.DbModels.Drone", "Drone")
                        .WithMany()
                        .HasForeignKey("DroneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DronnI_Back.Models.DbModels.User", "Operator")
                        .WithMany("OperatorRent")
                        .HasForeignKey("OperatorId")
                        .IsRequired();

                    b.HasOne("DronnI_Back.Models.DbModels.Statistic", "Statistic")
                        .WithMany()
                        .HasForeignKey("StatisticId");

                    b.Navigation("Customer");

                    b.Navigation("Drone");

                    b.Navigation("Operator");

                    b.Navigation("Statistic");
                });

            modelBuilder.Entity("DronnI_Back.Models.DbModels.Drone", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("DronnI_Back.Models.DbModels.User", b =>
                {
                    b.Navigation("CustomerRent");

                    b.Navigation("Drones");

                    b.Navigation("OperatorRent");
                });
#pragma warning restore 612, 618
        }
    }
}
