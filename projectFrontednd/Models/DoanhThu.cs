using System;
using System.Collections.Generic;

namespace projectFrontednd.Models;

public partial class DoanhThu
{
    public int MaHoaDon { get; set; }

    public int? MaKhachHang { get; set; }

    public int? MaXe { get; set; }

    public int? MaNhanVien { get; set; }

    public DateOnly? NgayThanhToan { get; set; }

    public decimal? SoTien { get; set; }

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual NhanVien? MaNhanVienNavigation { get; set; }

    public virtual XeMay? MaXeNavigation { get; set; }
}
