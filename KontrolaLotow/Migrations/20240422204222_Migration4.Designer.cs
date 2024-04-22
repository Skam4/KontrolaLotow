﻿// <auto-generated />
using System;
using KontrolaLotow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KontrolaLotow.Migrations
{
    [DbContext(typeof(BazaLotow))]
    [Migration("20240422204222_Migration4")]
    partial class Migration4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KontrolaLotow.Models.Flight", b =>
                {
                    b.Property<int>("IdLotu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLotu"));

                    b.Property<DateTime>("DataWylotu")
                        .HasColumnType("datetime2");

                    b.Property<string>("MiejscePrzylotu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiejsceWylotu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumerLotu")
                        .HasColumnType("int");

                    b.Property<string>("TypSamolotu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdLotu");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("KontrolaLotow.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("KontrolaLotow.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRoleRoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("UserRoleRoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KontrolaLotow.Models.User", b =>
                {
                    b.HasOne("KontrolaLotow.Models.Role", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("KontrolaLotow.Models.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
