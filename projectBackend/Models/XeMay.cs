using System;
using System.Collections.Generic;

namespace projectBackend.Models;

public partial class XeMay
{
    public int MaXe { get; set; }

    public string? TenXe { get; set; }

    public string? HangXe { get; set; }

    public decimal? GiaThu { get; set; }

    public virtual ICollection<DoanhThu> DoanhThus { get; set; } = new List<DoanhThu>();

    public virtual ICollection<ThueXeNhieuNgay> ThueXeNhieuNgays { get; set; } = new List<ThueXeNhieuNgay>();

    public virtual ICollection<ThueXeTheoGio> ThueXeTheoGios { get; set; } = new List<ThueXeTheoGio>();
}
