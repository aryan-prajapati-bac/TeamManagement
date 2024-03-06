﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamManagement_Models.Database;

#nullable disable

namespace TeamManagement_Model.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TeamManagement.Models.Login", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Login");

                    b.HasData(
                        new
                        {
                            Email = "aryanprajapati2112001@gmail.com",
                            Password = "orM+mYfowlQ2G8r87ViiRep7qRnq8JMRlpSmjgG9Wf4="
                        });
                });

            modelBuilder.Entity("TeamManagement.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.HasKey("Email");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Email = "aryanprajapati2112001@gmail.com",
                            ContactNumber = "8989898989",
                            Count = 0,
                            DOB = new DateTime(2001, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Aryan",
                            LastName = "Prajapati",
                            Password = "orM+mYfowlQ2G8r87ViiRep7qRnq8JMRlpSmjgG9Wf4=",
                            RoleId = 3,
                            UserId = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
