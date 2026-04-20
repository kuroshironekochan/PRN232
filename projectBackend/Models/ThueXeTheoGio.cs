using System;
using System.Collections.Generic;

namespace projectBackend.Models;

public partial class ThueXeTheoGio
{
    public int MaThue { get; set; }

    public int? MaKhachHang { get; set; }

    public int? MaXe { get; set; }

    public int? MaNhanVien { get; set; }

    public DateTime? ThoiGianBatDau { get; set; }

    public DateTime? ThoiGianKetThuc { get; set; }

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual NhanVien? MaNhanVienNavigation { get; set; }

    public virtual XeMay? MaXeNavigation { get; set; }
}
