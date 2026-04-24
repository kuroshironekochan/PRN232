namespace projectBackend.DTO
{
    public class ThueXeTheoGioDto
    {
        public int MaThue { get; set; }
        public int MaKhachHang { get; set; }
        public int MaXe { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }

    public class readThueXeDTO
    {
        public int MaThue { get; set; }
        public string TenKhachHang { get; set; }
        public string TenXe { get; set; }
        public string TenNhanVien { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }
}
