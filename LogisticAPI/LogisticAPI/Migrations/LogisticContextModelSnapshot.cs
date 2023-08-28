﻿// <auto-generated />
using System;
using LogisticAPI.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogisticAPI.Migrations
{
    [DbContext(typeof(LogisticContext))]
    partial class LogisticContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Modern_Spanish_CI_AS")
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LogisticAPI.Entities.Place", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlaceType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("LogisticAPI.Entities.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("ConveyanceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeliveryDay")
                        .HasColumnType("datetime2");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<double>("FinalPrice")
                        .HasColumnType("float");

                    b.Property<string>("PlaceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConveyanceId");

                    b.HasIndex("PlaceId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("LogisticAPI.Test.Repositories.Conveyance", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TransportType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Conveyances");
                });

            modelBuilder.Entity("LogisticAPI.Entities.Product", b =>
                {
                    b.HasOne("LogisticAPI.Test.Repositories.Conveyance", "Conveyance")
                        .WithMany("Products")
                        .HasForeignKey("ConveyanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LogisticAPI.Entities.Place", "Place")
                        .WithMany("Products")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conveyance");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("LogisticAPI.Entities.Place", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("LogisticAPI.Test.Repositories.Conveyance", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
