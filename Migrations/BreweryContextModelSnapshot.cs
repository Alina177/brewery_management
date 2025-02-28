﻿// <auto-generated />
using BreweryManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BreweryManagement.Migrations
{
    [DbContext(typeof(BreweryContext))]
    partial class BreweryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("BreweryManagement.Models.Beer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("AlcoholContent")
                        .HasColumnType("REAL");

                    b.Property<int>("BreweryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BreweryId");

                    b.ToTable("Beers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AlcoholContent = 6.5999999999999996,
                            BreweryId = 1,
                            Name = "Leffe Blonde",
                            Price = 2.20m
                        });
                });

            modelBuilder.Entity("BreweryManagement.Models.Brewery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Breweries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Abbaye de Leffe"
                        });
                });

            modelBuilder.Entity("BreweryManagement.Models.Wholesaler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Wholesalers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "GeneDrinks"
                        });
                });

            modelBuilder.Entity("BreweryManagement.Models.WholesalerStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BeerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WholesalerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("WholesalerId");

                    b.ToTable("WholesalerStocks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BeerId = 1,
                            Quantity = 10,
                            WholesalerId = 1
                        });
                });

            modelBuilder.Entity("BreweryManagement.Models.Beer", b =>
                {
                    b.HasOne("BreweryManagement.Models.Brewery", "Brewery")
                        .WithMany("Beers")
                        .HasForeignKey("BreweryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brewery");
                });

            modelBuilder.Entity("BreweryManagement.Models.WholesalerStock", b =>
                {
                    b.HasOne("BreweryManagement.Models.Beer", "Beer")
                        .WithMany("WholesalerStocks")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BreweryManagement.Models.Wholesaler", "Wholesaler")
                        .WithMany("Stocks")
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("BreweryManagement.Models.Beer", b =>
                {
                    b.Navigation("WholesalerStocks");
                });

            modelBuilder.Entity("BreweryManagement.Models.Brewery", b =>
                {
                    b.Navigation("Beers");
                });

            modelBuilder.Entity("BreweryManagement.Models.Wholesaler", b =>
                {
                    b.Navigation("Stocks");
                });
#pragma warning restore 612, 618
        }
    }
}
