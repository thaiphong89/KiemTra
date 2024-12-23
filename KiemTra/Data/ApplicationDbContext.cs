using Microsoft.EntityFrameworkCore;
using KiemTra.Models;

namespace KiemTra.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<HocPhan> HocPhan { get; set; } = null!;
        public DbSet<SinhVien> SinhVien { get; set; } = null!;
        public DbSet<NganhHoc> NganhHoc { get; set; } = null!;
        public DbSet<DangKy> DangKy { get; set; } = null!;
        public DbSet<ChiTietDangKy> ChiTietDangKy { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HocPhan>(entity =>
            {
                entity.ToTable("HocPhan");
                entity.HasKey(e => e.MaHP);
            });

            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.ToTable("SinhVien");
                entity.HasKey(e => e.MaSV);
                entity.Property(e => e.MaSV).IsRequired().HasMaxLength(50);
                entity.Property(e => e.HoTen).IsRequired().HasMaxLength(100);
                entity.Property(e => e.GioiTinh).IsRequired();
                entity.Property(e => e.NgaySinh).IsRequired();
                entity.Property(e => e.Hinh).HasMaxLength(255).IsRequired(false);
                entity.Property(e => e.MaNganh).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<NganhHoc>(entity =>
            {
                entity.ToTable("NganhHoc");
                entity.HasKey(e => e.MaNganh);
            });

            modelBuilder.Entity<DangKy>(entity =>
            {
                entity.ToTable("DangKy");
                entity.HasKey(e => e.MaDK);
            });

            modelBuilder.Entity<ChiTietDangKy>(entity =>
            {
                entity.ToTable("ChiTietDangKy");
                entity.HasKey(e => new { e.MaDK, e.MaHP });
            });
        }
    }
}

