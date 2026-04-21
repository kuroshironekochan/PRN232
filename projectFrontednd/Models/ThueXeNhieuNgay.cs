using System;
using System.Collections.Generic;

namespace projectFrontednd.Models;

public partial class ThueXeNhieuNgay
{
    public int MaThue { get; set; }

    public int? MaKhachHang { get; set; }

    public int? MaXe { get; set; }

    public int? MaNhanVien { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual NhanVien? MaNhanVienNavigation { get; set; }

    public virtual XeMay? MaXeNavigation { get; set; }
}
