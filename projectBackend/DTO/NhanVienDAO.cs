namespace projectBackend.DTO
{
    public class NhanVienDAO
    {
        public class ReadNhanVien()
        {
            public int MaNhanVien { get; set; }
            public string? HoTen { get; set; }
            public string? DiaChi { get; set; }
            public string? SoDienThoai { get; set; }
        }

    }
}
