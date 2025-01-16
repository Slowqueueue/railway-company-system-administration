﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RailwayCompanyIS.Data;

#nullable disable

namespace RailwayCompanyIS.Data.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241206153542_RailwayCompanyCreateTables")]
    partial class RailwayCompanyCreateTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StreetNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.Carrier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Carriers");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("City");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarrierId")
                        .HasColumnType("int");

                    b.Property<string>("ContactContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.Departure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarrierId")
                        .HasColumnType("int");

                    b.Property<int>("CityFromId")
                        .HasColumnType("int");

                    b.Property<int>("CityToId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfArrival")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfDeparture")
                        .HasColumnType("datetime2");

                    b.Property<int>("DistanceId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<int>("PaymentCategoryId")
                        .HasColumnType("int");

                    b.Property<double>("PriceOfCard")
                        .HasColumnType("float");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.HasIndex("CityFromId");

                    b.HasIndex("CityToId");

                    b.HasIndex("DistanceId");

                    b.HasIndex("PaymentCategoryId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Departures");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.Distance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CityFromId")
                        .HasColumnType("int");

                    b.Property<int>("CityToId")
                        .HasColumnType("int");

                    b.Property<int>("DistanceBetween")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityFromId");

                    b.HasIndex("CityToId");

                    b.ToTable("Distances");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.PaymentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("PaymentCategories");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int?>("CarrierId")
                        .HasColumnType("int");

                    b.Property<int>("RegistrationNumber")
                        .HasColumnType("int");

                    b.Property<bool>("Tourist")
                        .HasColumnType("bit");

                    b.Property<int>("VehicleType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.Address", b =>
                {
                    b.HasOne("RailwayCompanyIS.Data.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.Carrier", b =>
                {
                    b.HasOne("RailwayCompanyIS.Data.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.Contact", b =>
                {
                    b.HasOne("RailwayCompanyIS.Data.Models.Carrier", null)
                        .WithMany("Contacts")
                        .HasForeignKey("CarrierId");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.Departure", b =>
                {
                    b.HasOne("RailwayCompanyIS.Data.Models.Carrier", "Carrier")
                        .WithMany()
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RailwayCompanyIS.Data.Models.City", "CityFrom")
                        .WithMany()
                        .HasForeignKey("CityFromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RailwayCompanyIS.Data.Models.City", "CityTo")
                        .WithMany()
                        .HasForeignKey("CityToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RailwayCompanyIS.Data.Models.Distance", "Distance")
                        .WithMany()
                        .HasForeignKey("DistanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RailwayCompanyIS.Data.Models.PaymentCategory", "PaymentCategory")
                        .WithMany()
                        .HasForeignKey("PaymentCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RailwayCompanyIS.Data.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrier");

                    b.Navigation("CityFrom");

                    b.Navigation("CityTo");

                    b.Navigation("Distance");

                    b.Navigation("PaymentCategory");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.Distance", b =>
                {
                    b.HasOne("RailwayCompanyIS.Data.Models.City", "CityFrom")
                        .WithMany()
                        .HasForeignKey("CityFromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RailwayCompanyIS.Data.Models.City", "CityTo")
                        .WithMany()
                        .HasForeignKey("CityToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CityFrom");

                    b.Navigation("CityTo");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.Vehicle", b =>
                {
                    b.HasOne("RailwayCompanyIS.Data.Models.Carrier", null)
                        .WithMany("Vehicles")
                        .HasForeignKey("CarrierId");
                });

            modelBuilder.Entity("RailwayCompanyIS.Data.Models.Carrier", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
