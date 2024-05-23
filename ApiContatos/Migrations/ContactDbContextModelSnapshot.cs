﻿// <auto-generated />
using ApiContatos.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiContatos.Migrations
{
    [DbContext(typeof(ContactDbContext))]
    partial class ContactDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("ApiContatos.Domain.Contact", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ddds")
                        .HasColumnType("Varchar(150)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("INT");

                    b.HasKey("Id");

                    b.ToTable("Contacts", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
