﻿// <auto-generated />
using System;
using KiemTra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KiemTra.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241221132858_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KiemTra.Models.ChiTietDangKy", b =>
                {
                    b.Property<int>("MaDK")
                        .HasColumnType("int");

                    b.Property<string>("MaHP")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MaDK", "MaHP");

                    b.HasIndex("MaHP");

                    b.ToTable("ChiTietDangKys");
                });

            modelBuilder.Entity("KiemTra.Models.DangKy", b =>
                {
                    b.Property<int>("MaDK")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDK"));

                    b.Property<string>("MaSV")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("NgayDK")
                        .HasColumnType("datetime2");

                    b.HasKey("MaDK");

                    b.HasIndex("MaSV");

                    b.ToTable("DangKys");
                });

            modelBuilder.Entity("KiemTra.Models.HocPhan", b =>
                {
                    b.Property<string>("MaHP")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SoLuongDuKien")
                        .HasColumnType("int");

                    b.Property<int>("SoTinChi")
                        .HasColumnType("int");

                    b.Property<string>("TenHP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaHP");

                    b.ToTable("HocPhans");
                });

            modelBuilder.Entity("KiemTra.Models.NganhHoc", b =>
                {
                    b.Property<string>("MaNganh")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TenNganh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaNganh");

                    b.ToTable("NganhHocs");
                });

            modelBuilder.Entity("KiemTra.Models.SinhVien", b =>
                {
                    b.Property<string>("MaSV")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GioiTinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaNganh")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.HasKey("MaSV");

                    b.HasIndex("MaNganh");

                    b.ToTable("SinhViens");
                });

            modelBuilder.Entity("KiemTra.Models.ChiTietDangKy", b =>
                {
                    b.HasOne("KiemTra.Models.DangKy", "DangKy")
                        .WithMany("ChiTietDangKys")
                        .HasForeignKey("MaDK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KiemTra.Models.HocPhan", "HocPhan")
                        .WithMany("ChiTietDangKys")
                        .HasForeignKey("MaHP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DangKy");

                    b.Navigation("HocPhan");
                });

            modelBuilder.Entity("KiemTra.Models.DangKy", b =>
                {
                    b.HasOne("KiemTra.Models.SinhVien", "SinhVien")
                        .WithMany("DangKys")
                        .HasForeignKey("MaSV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SinhVien");
                });

            modelBuilder.Entity("KiemTra.Models.SinhVien", b =>
                {
                    b.HasOne("KiemTra.Models.NganhHoc", "NganhHoc")
                        .WithMany("SinhViens")
                        .HasForeignKey("MaNganh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NganhHoc");
                });

            modelBuilder.Entity("KiemTra.Models.DangKy", b =>
                {
                    b.Navigation("ChiTietDangKys");
                });

            modelBuilder.Entity("KiemTra.Models.HocPhan", b =>
                {
                    b.Navigation("ChiTietDangKys");
                });

            modelBuilder.Entity("KiemTra.Models.NganhHoc", b =>
                {
                    b.Navigation("SinhViens");
                });

            modelBuilder.Entity("KiemTra.Models.SinhVien", b =>
                {
                    b.Navigation("DangKys");
                });
#pragma warning restore 612, 618
        }
    }
}
