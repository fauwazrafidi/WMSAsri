﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Server.Data;

#nullable disable

namespace Server.Migrations.AppDb
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240905093423_AddModelsCreate")]
    partial class AddModelsCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SHARED.Models.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SHARED.Models.Checkinout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Location_id")
                        .HasColumnType("integer");

                    b.Property<int>("Productid")
                        .HasColumnType("integer");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision");

                    b.Property<string>("Scandate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Transaction_type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Location_id");

                    b.HasIndex("Productid");

                    b.ToTable("Checkinout");
                });

            modelBuilder.Entity("SHARED.Models.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LocationId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("SHARED.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Category_id")
                        .HasColumnType("integer");

                    b.Property<int?>("Location_id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Productid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Supplier_Id")
                        .HasColumnType("integer");

                    b.Property<string>("color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("date")
                        .HasColumnType("date");

                    b.Property<double?>("density")
                        .HasColumnType("double precision");

                    b.Property<string>("invoice_No")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("quantity_in_stock")
                        .HasColumnType("double precision");

                    b.Property<double?>("width")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("Category_id");

                    b.HasIndex("Location_id");

                    b.HasIndex("Supplier_Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("SHARED.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SupplierId"));

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("SHARED.Models.Checkinout", b =>
                {
                    b.HasOne("SHARED.Models.Location", "Location")
                        .WithMany("Checkinout")
                        .HasForeignKey("Location_id");

                    b.HasOne("SHARED.Models.Product", "Product")
                        .WithMany("Checkinout")
                        .HasForeignKey("Productid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SHARED.Models.Product", b =>
                {
                    b.HasOne("SHARED.Models.Categories", "Categories")
                        .WithMany("Product")
                        .HasForeignKey("Category_id");

                    b.HasOne("SHARED.Models.Location", "Location")
                        .WithMany("Product")
                        .HasForeignKey("Location_id");

                    b.HasOne("SHARED.Models.Supplier", "Supplier")
                        .WithMany("Product")
                        .HasForeignKey("Supplier_Id");

                    b.Navigation("Categories");

                    b.Navigation("Location");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("SHARED.Models.Categories", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("SHARED.Models.Location", b =>
                {
                    b.Navigation("Checkinout");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SHARED.Models.Product", b =>
                {
                    b.Navigation("Checkinout");
                });

            modelBuilder.Entity("SHARED.Models.Supplier", b =>
                {
                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
