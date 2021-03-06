﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherStore.Models;

namespace WeatherStore.Migrations
{
    [DbContext(typeof(ReadingContext))]
    [Migration("20190127175140_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WeatherStore.Models.Reading", b =>
                {
                    b.Property<int>("ReadingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<int>("Device");

                    b.Property<decimal>("Humidity")
                        .HasColumnType("decimal(6,1)");

                    b.Property<decimal>("Pressure")
                        .HasColumnType("decimal(6,1)");

                    b.Property<decimal>("Temperature")
                        .HasColumnType("decimal(6,1)");

                    b.HasKey("ReadingId");

                    b.ToTable("Readings");
                });
#pragma warning restore 612, 618
        }
    }
}
