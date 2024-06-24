﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ApplicationCore.Entities.Areas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ActivitiesHistory")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPublish")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StaffCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("StoreCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TraceId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsernameEdit")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("ApplicationCore.Entities.AuthenUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ActivitiesHistory")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPublish")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TraceId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("UsernameEdit")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("AuthenUser");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Companies", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ActivitiesHistory")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPublish")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TraceId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsernameEdit")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("ApplicationCore.Entities.OrderDetails", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ActivitiesHistory")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(9, 2)");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPublish")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("OrderCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("StaffCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TraceId")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(9, 2)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsernameEdit")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Orders", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ActivitiesHistory")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<string>("AreaCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPublish")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("StaffCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("StoreCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TableCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TraceId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsernameEdit")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Staffs", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ActivitiesHistory")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPublish")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("StoreCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TraceId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsernameEdit")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Stores", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ActivitiesHistory")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CompanyCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPublish")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TraceId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsernameEdit")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("ApplicationCore.Entities.Tables", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ActivitiesHistory")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("AreaCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPublish")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StaffCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("StoreCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TraceId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TypeBidaCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TypeSaleCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsernameEdit")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("ApplicationCore.Entities.TypeBida", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ActivitiesHistory")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPublish")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("StaffCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("StoreCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TraceId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsernameEdit")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("TypeBida");
                });

            modelBuilder.Entity("ApplicationCore.Entities.TypeSales", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ActivitiesHistory")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPublish")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("StaffCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("StoreCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TraceId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UsernameEdit")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("TypeSales");
                });
#pragma warning restore 612, 618
        }
    }
}
