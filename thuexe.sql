--create database thuexemay

-- 1. Tạo bảng XeMay
CREATE TABLE XeMay (
    MaXe INT PRIMARY KEY,
    TenXe NVARCHAR(100),
    HangXe NVARCHAR(50),
    GiaThu DECIMAL(18,2)
);

-- 2. Tạo bảng KhachHang
CREATE TABLE KhachHang (
    MaKhachHang INT PRIMARY KEY,
    HoTen NVARCHAR(100),
    DiaChi NVARCHAR(200),
    SoDienThoai NVARCHAR(15)
);

-- 3. Tạo bảng NhanVien
CREATE TABLE NhanVien (
    MaNhanVien INT PRIMARY KEY,
    HoTen NVARCHAR(100),
    DiaChi NVARCHAR(200),
    SoDienThoai NVARCHAR(15)
);

-- 4. Tạo bảng DoanhThu
CREATE TABLE DoanhThu (
    MaHoaDon INT PRIMARY KEY,
    MaKhachHang INT,
    MaXe INT,
    MaNhanVien INT,
    NgayThanhToan DATE,
    SoTien DECIMAL(18,2),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaXe) REFERENCES XeMay(MaXe),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

-- 5. Tạo bảng ThueXeNhieuNgay
CREATE TABLE ThueXeNhieuNgay (
    MaThue INT PRIMARY KEY,
    MaKhachHang INT,
    MaXe INT,
    MaNhanVien INT,
    NgayBatDau DATE,
    NgayKetThuc DATE,
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaXe) REFERENCES XeMay(MaXe),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

-- 6. Tạo bảng ThueXeTheoGio
CREATE TABLE ThueXeTheoGio (
    MaThue INT PRIMARY KEY,
    MaKhachHang INT,
    MaXe INT,
    MaNhanVien INT,
    ThoiGianBatDau DATETIME,
    ThoiGianKetThuc DATETIME,
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaXe) REFERENCES XeMay(MaXe),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

-- ======================
-- CHÈN DỮ LIỆU MẪU
-- ======================

-- XeMay
INSERT INTO XeMay VALUES
(1, N'Exciter 150', N'Yamaha', 200000),
(2, N'Winner X', N'Honda', 220000),
(3, N'SH Mode', N'Honda', 300000),
(4, N'Vespa Primavera', N'Vespa', 450000),
(5, N'Lead', N'Honda', 180000);

-- KhachHang
INSERT INTO KhachHang VALUES
(1, N'Nguyễn Văn An', N'Hà Nội', N'0981234567'),
(2, N'Trần Thị Bình', N'Hải Phòng', N'0972345678'),
(3, N'Lê Văn Cường', N'Đà Nẵng', N'0963456789'),
(4, N'Phạm Thị Dung', N'TP.HCM', N'0954567890'),
(5, N'Hoàng Văn Em', N'Cần Thơ', N'0945678901');

-- NhanVien
INSERT INTO NhanVien VALUES
(1, N'Trần Văn Phúc', N'Hà Nội', N'0903123456'),
(2, N'Lê Thị Hoa', N'Hải Phòng', N'0914234567'),
(3, N'Vũ Văn Kiên', N'Đà Nẵng', N'0925345678');

-- DoanhThu
INSERT INTO DoanhThu VALUES
(1, 1, 1, 1, '2025-04-10', 200000),
(2, 2, 3, 2, '2025-04-11', 300000),
(3, 3, 2, 3, '2025-04-12', 220000),
(4, 4, 4, 1, '2025-04-13', 450000),
(5, 5, 5, 2, '2025-04-14', 180000);

-- ThueXeNhieuNgay
INSERT INTO ThueXeNhieuNgay VALUES
(1, 1, 1, 1, '2025-04-01', '2025-04-05'),
(2, 2, 3, 2, '2025-04-02', '2025-04-06'),
(3, 3, 2, 3, '2025-04-03', '2025-04-07');

-- ThueXeTheoGio
INSERT INTO ThueXeTheoGio VALUES
(1, 4, 4, 1, '2025-04-14 08:00:00', '2025-04-14 12:00:00'),
(2, 5, 5, 2, '2025-04-14 13:00:00', '2025-04-14 17:00:00');