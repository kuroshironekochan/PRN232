using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace projectBackend.Models;

public partial class ThuexemayContext : DbContext
{
    public ThuexemayContext()
    {
    }

    public ThuexemayContext(DbContextOptions<ThuexemayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DoanhThu> DoanhThus { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<ThueXeNhieuNgay> ThueXeNhieuNgays { get; set; }

    public virtual DbSet<ThueXeTheoGio> ThueXeTheoGios { get; set; }

    public virtual DbSet<XeMay> XeMays { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Name=ConnectionStrings:MyCnn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DoanhThu>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__DoanhThu__835ED13BCB10D15C");

            entity.ToTable("DoanhThu");

            entity.Property(e => e.MaHoaDon).ValueGeneratedNever();
            entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.DoanhThus)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__DoanhThu__MaKhac__3D5E1FD2");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany(p => p.DoanhThus)
                .HasForeignKey(d => d.MaNhanVien)
                .HasConstraintName("FK__DoanhThu__MaNhan__3F466844");

            entity.HasOne(d => d.MaXeNavigation).WithMany(p => p.DoanhThus)
                .HasForeignKey(d => d.MaXe)
                .HasConstraintName("FK__DoanhThu__MaXe__3E52440B");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E5B8C3B315");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKhachHang).ValueGeneratedNever();
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai).HasMaxLength(15);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__NhanVien__77B2CA473C573C50");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNhanVien).ValueGeneratedNever();
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai).HasMaxLength(15);
        });

        modelBuilder.Entity<ThueXeNhieuNgay>(entity =>
        {
            entity.HasKey(e => e.MaThue).HasName("PK__ThueXeNh__9CC2FDA37D62FCC9");

            entity.ToTable("ThueXeNhieuNgay");

            entity.Property(e => e.MaThue).ValueGeneratedNever();

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.ThueXeNhieuNgays)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__ThueXeNhi__MaKha__4222D4EF");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany(p => p.ThueXeNhieuNgays)
                .HasForeignKey(d => d.MaNhanVien)
                .HasConstraintName("FK__ThueXeNhi__MaNha__440B1D61");

            entity.HasOne(d => d.MaXeNavigation).WithMany(p => p.ThueXeNhieuNgays)
                .HasForeignKey(d => d.MaXe)
                .HasConstraintName("FK__ThueXeNhie__MaXe__4316F928");
        });

        modelBuilder.Entity<ThueXeTheoGio>(entity =>
        {
            entity.HasKey(e => e.MaThue).HasName("PK__ThueXeTh__9CC2FDA3E7B47C79");

            entity.ToTable("ThueXeTheoGio");

            entity.Property(e => e.MaThue).ValueGeneratedNever();
            entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianKetThuc).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.ThueXeTheoGios)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__ThueXeThe__MaKha__46E78A0C");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany(p => p.ThueXeTheoGios)
                .HasForeignKey(d => d.MaNhanVien)
                .HasConstraintName("FK__ThueXeThe__MaNha__48CFD27E");

            entity.HasOne(d => d.MaXeNavigation).WithMany(p => p.ThueXeTheoGios)
                .HasForeignKey(d => d.MaXe)
                .HasConstraintName("FK__ThueXeTheo__MaXe__47DBAE45");
        });

        modelBuilder.Entity<XeMay>(entity =>
        {
            entity.HasKey(e => e.MaXe).HasName("PK__XeMay__272520CDC0A14443");

            entity.ToTable("XeMay");

            entity.Property(e => e.MaXe).ValueGeneratedNever();
            entity.Property(e => e.GiaThu).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HangXe).HasMaxLength(50);
            entity.Property(e => e.TenXe).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
