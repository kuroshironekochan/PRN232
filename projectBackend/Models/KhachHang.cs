using System;
using System.Collections.Generic;

namespace projectBackend.Models;

public partial class KhachHang
{
    public int MaKhachHang { get; set; }

    public string? HoTen { get; set; }

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public virtual ICollection<DoanhThu> DoanhThus { get; set; } = new List<DoanhThu>();

    public virtual ICollection<ThueXeNhieuNgay> ThueXeNhieuNgays { get; set; } = new List<ThueXeNhieuNgay>();

    public virtual ICollection<ThueXeTheoGio> ThueXeTheoGios { get; set; } = new List<ThueXeTheoGio>();
}
