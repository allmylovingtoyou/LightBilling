﻿// <auto-generated />
using System;
using Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Db.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Domain.Client.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApartmentNumber");

                    b.Property<double>("Balance");

                    b.Property<string>("Comment");

                    b.Property<double>("Credit");

                    b.Property<DateTime?>("CreditValidFrom");

                    b.Property<DateTime?>("CreditValidTo");

                    b.Property<string>("FullName");

                    b.Property<int?>("GreyAddressId");

                    b.Property<int?>("HouseId");

                    b.Property<string>("HwIpAddress");

                    b.Property<string>("HwPort");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Login");

                    b.Property<string>("MacAddress");

                    b.Property<string>("PassportData");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("GreyAddressId")
                        .IsUnique();

                    b.HasIndex("HouseId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Domain.House.House", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalNumber");

                    b.Property<string>("Address");

                    b.Property<string>("Comment");

                    b.Property<string>("Number");

                    b.Property<string>("Porch");

                    b.Property<int?>("SubnetId");

                    b.HasKey("Id");

                    b.HasIndex("SubnetId");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("Domain.Network.GreyAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int?>("ClientId");

                    b.Property<int?>("SubnetId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("SubnetId");

                    b.ToTable("GreyAddresses");
                });

            modelBuilder.Entity("Domain.Network.Subnet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Gateway");

                    b.Property<int>("Mask");

                    b.Property<string>("Net");

                    b.HasKey("Id");

                    b.ToTable("Subnets");
                });

            modelBuilder.Entity("Domain.Network.WhiteAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int?>("GrayAddressId");

                    b.HasKey("Id");

                    b.HasIndex("GrayAddressId")
                        .IsUnique();

                    b.ToTable("WhiteAddresses");
                });

            modelBuilder.Entity("Domain.Payment.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int>("ClientId");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Domain.Tariff.JoinClientsTariffs", b =>
                {
                    b.Property<int>("ClientId");

                    b.Property<int>("TariffId");

                    b.HasKey("ClientId", "TariffId");

                    b.HasIndex("TariffId");

                    b.ToTable("JoinClientsTariffs");
                });

            modelBuilder.Entity("Domain.Tariff.Tariff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<double>("Cost");

                    b.Property<int>("InputRate");

                    b.Property<bool>("IsPeriodic");

                    b.Property<string>("Name");

                    b.Property<int>("OutputRate");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Tariffs");
                });

            modelBuilder.Entity("Domain.User.SystemUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.ToTable("SystemUsers");
                });

            modelBuilder.Entity("Domain.Client.Client", b =>
                {
                    b.HasOne("Domain.Network.GreyAddress", "GreyAddress")
                        .WithMany()
                        .HasForeignKey("GreyAddressId");

                    b.HasOne("Domain.House.House", "House")
                        .WithMany()
                        .HasForeignKey("HouseId");
                });

            modelBuilder.Entity("Domain.House.House", b =>
                {
                    b.HasOne("Domain.Network.Subnet", "Subnet")
                        .WithMany("Houses")
                        .HasForeignKey("SubnetId");
                });

            modelBuilder.Entity("Domain.Network.GreyAddress", b =>
                {
                    b.HasOne("Domain.Client.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("Domain.Network.Subnet")
                        .WithMany("Addresses")
                        .HasForeignKey("SubnetId");
                });

            modelBuilder.Entity("Domain.Network.WhiteAddress", b =>
                {
                    b.HasOne("Domain.Network.GreyAddress", "GrayAddress")
                        .WithOne("White")
                        .HasForeignKey("Domain.Network.WhiteAddress", "GrayAddressId");
                });

            modelBuilder.Entity("Domain.Payment.Payment", b =>
                {
                    b.HasOne("Domain.Client.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Tariff.JoinClientsTariffs", b =>
                {
                    b.HasOne("Domain.Client.Client", "Client")
                        .WithMany("JoinTariffs")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Tariff.Tariff", "Tariff")
                        .WithMany("JoinClients")
                        .HasForeignKey("TariffId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
