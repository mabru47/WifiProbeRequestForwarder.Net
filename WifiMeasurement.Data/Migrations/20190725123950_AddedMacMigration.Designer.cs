﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WifiMeasurement.Data;

namespace WifiMeasurement.Data.Migrations
{
    [DbContext(typeof(MeasurementContext))]
    [Migration("20190725123950_AddedMacMigration")]
    partial class AddedMacMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WifiMeasurement.Data.Models.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("DeviceId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("WifiMeasurement.Data.Models.Measurement", b =>
                {
                    b.Property<int>("MeasurementId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MAC");

                    b.Property<int>("RSSI");

                    b.Property<int>("TestSeriesId");

                    b.HasKey("MeasurementId");

                    b.HasIndex("TestSeriesId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("WifiMeasurement.Data.Models.TestSeries", b =>
                {
                    b.Property<int>("TestSeriesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DeviceId");

                    b.Property<int>("Distance");

                    b.HasKey("TestSeriesId");

                    b.HasIndex("DeviceId");

                    b.ToTable("TestSeries");
                });

            modelBuilder.Entity("WifiMeasurement.Data.Models.Measurement", b =>
                {
                    b.HasOne("WifiMeasurement.Data.Models.TestSeries", "TestSeries")
                        .WithMany("Measurements")
                        .HasForeignKey("TestSeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WifiMeasurement.Data.Models.TestSeries", b =>
                {
                    b.HasOne("WifiMeasurement.Data.Models.Device", "Device")
                        .WithMany("TestSeries")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
