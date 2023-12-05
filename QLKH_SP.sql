USE master
GO
--Chạy tới bảng hóa đơn,chitiethoadon,nhaphang,chititenhaphang thì đừng chạy insert nên chạy trigger trước r hả insert value dô
--drop DATABASE QLKH_SP
go

CREATE DATABASE QLKH_SP
GO
USE QLKH_SP

CREATE TABLE KhachHang
(
	IdKhachHang  VARCHAR(12) NOT NULL,
	TenKhachHang NVARCHAR(50) NOT NULL,
	NgaySinh DATE,
	GioiTinh NVARCHAR(5),
	DiaChi NVARCHAR(255),
	SoDienThoai CHAR(10) NOT NULL,
	Email VARCHAR(100),
	CONSTRAINT PK_KhachHang PRIMARY KEY(IdKhachHang),
	CONSTRAINT UNI_SoDienThoai_KhachHang UNIQUE(SoDienThoai),
	CONSTRAINT UNI_Email_KhachHang UNIQUE(Email),	
);
select*from KhachHang
set dateformat dmy;
INSERT INTO KhachHang VALUES ('KH001',N'Trần Văn Thân','20/9/2003',N'Nam',N'Q1-Tp.HCM','0981725162','than@gmail.com');
INSERT INTO KhachHang VALUES ('KH002',N'Trần Thị Nhàn','10/8/2000',N'Nữ',N'Q5-Tp.HCM','0984675166','nhan@gmail.com');
INSERT INTO KhachHang VALUES ('KH003',N'Lê Thị Mít','8/8/1999',N'Nữ',N'Q.Tân Phú-Tp.HCM','0981725315','mit@gmail.com');
INSERT INTO KhachHang VALUES ('KH004',N'Trần Dần','8/6/1999',N'Nam',N'Q.Tân Phú-Tp.HCM','098176785','dan@gmail.com');
--====================================================================================================---


CREATE TABLE LoaiSanPhamCha
(
	IdLoaiSPCha VARCHAR(2) NOT NULL,
	TenLoaiSPCha NVARCHAR(255),
	CONSTRAINT PK_IdLoaiSPCha PRIMARY KEY(IdLoaiSPCha),
);
INSERT INTO LoaiSanPhamCha VALUES ('01',N'Quần Áo');
INSERT INTO LoaiSanPhamCha VALUES ('02',N'Đồ gia dụng');
INSERT INTO LoaiSanPhamCha VALUES ('03',N'Thực phẩm');
INSERT INTO LoaiSanPhamCha VALUES ('04',N'Sách và Văn phòng phẩm');
INSERT INTO LoaiSanPhamCha VALUES ('05',N'Hóa mỹ phẩm và chăm sóc sức khỏe');
INSERT INTO LoaiSanPhamCha VALUES ('06',N'Giày dép');
INSERT INTO LoaiSanPhamCha VALUES ('07',N'Sản phẩm điện tử');
SELECT * FROM LoaiSanPhamCha

--====================================================================================================---


CREATE TABLE LoaiSanPham
(
	IdLoaiSP VARCHAR(3) NOT NULL,
	TenLoaiSP NVARCHAR(255),
	IdLoaiSPCha VARCHAR(2),
	CONSTRAINT PK_LoaiSanPham PRIMARY KEY(IdLoaiSP),
	CONSTRAINT UNI_TenLoaiSP UNIQUE(TenLoaiSP),
	CONSTRAINT FK_LoaiSanPham_LoaiSanPhamCha FOREIGN KEY (IdLoaiSPCha) REFERENCES LoaiSanPhamCha(IdLoaiSPCha)
);

INSERT INTO LoaiSanPham VALUES ('01',N'Thời trang nam','01');
INSERT INTO LoaiSanPham VALUES ('02',N'Thời trang nữ:','01');
INSERT INTO LoaiSanPham VALUES ('03',N'Thời trang trẻ em','01');
INSERT INTO LoaiSanPham VALUES ('04',N'Nội thất','02');
INSERT INTO LoaiSanPham VALUES ('05',N'Thiết bị gia đình','02');
INSERT INTO LoaiSanPham VALUES ('06',N'Vật dụng nhà bếp','02');
INSERT INTO LoaiSanPham VALUES ('07',N'Trang trí nội thất','02');
INSERT INTO LoaiSanPham VALUES ('08',N'Thực phẩm tươi sống:','03');
INSERT INTO LoaiSanPham VALUES ('09',N'Thực phẩm đông lạnh','03');
INSERT INTO LoaiSanPham VALUES ('10',N'Thực phẩm đóng hộp và đóng gói','03');
INSERT INTO LoaiSanPham VALUES ('11',N'Balo - Bóp viết','04');
INSERT INTO LoaiSanPham VALUES ('12',N'Bút - Viết','04');
INSERT INTO LoaiSanPham VALUES ('13',N'Tập - Vở','04');
INSERT INTO LoaiSanPham VALUES ('14',N'Sách thiếu nhi','04');
INSERT INTO LoaiSanPham VALUES ('15',N'Tiểu thuyết','04');
INSERT INTO LoaiSanPham VALUES ('16',N'Văn học','04');
INSERT INTO LoaiSanPham VALUES ('17',N'Xu hướng kinh tế','04');
INSERT INTO LoaiSanPham VALUES ('18',N'ngôn tình','04');
INSERT INTO LoaiSanPham VALUES ('19',N'Nước hoa','05');
INSERT INTO LoaiSanPham VALUES ('20',N'Son','05');
INSERT INTO LoaiSanPham VALUES ('21',N'Chăm sóc da','05');
INSERT INTO LoaiSanPham VALUES ('22',N'Thực phẩm chức năng','05');
INSERT INTO LoaiSanPham VALUES ('23',N'Tẩy trang','05');
INSERT INTO LoaiSanPham VALUES ('24',N'Sneaker','06');
INSERT INTO LoaiSanPham VALUES ('25',N'Giày cao gót','06');
INSERT INTO LoaiSanPham VALUES ('26',N'Giày sandal','06');
INSERT INTO LoaiSanPham VALUES ('27',N'Giày boot','06');
INSERT INTO LoaiSanPham VALUES ('28',N'Tất - Vớ','06');
INSERT INTO LoaiSanPham VALUES ('29',N'Điện thoại','07');
INSERT INTO LoaiSanPham VALUES ('30',N'Máy tính bảng','07');
INSERT INTO LoaiSanPham VALUES ('31',N'Tivi','07');
INSERT INTO LoaiSanPham VALUES ('32',N'Laptop','07');
INSERT INTO LoaiSanPham VALUES ('33',N'Máy quay - Máy ảnh','07');

SELECT * FROM LoaiSanPham

--====================================================================================================---

CREATE TABLE SanPham (
  IdSanPham VARCHAR(12) NOT NULL,
  TenSanPham NVARCHAR(255) NOT NULL,
  IdLoaiSP VARCHAR(3) NOT NULL,
  AnhSP VARCHAR(255) NOT NULL,
  GiaGoc BIGINT,
  GiamGia INT,
  CONSTRAINT PK_SanPham PRIMARY KEY(IdSanPham),
  CONSTRAINT UNI_TenSanPham UNIQUE(TenSanPham),
  CONSTRAINT FK_SanPham_LoaiSanPham FOREIGN KEY (IdLoaiSP) REFERENCES LoaiSanPham(IdLoaiSP),
);

INSERT INTO SanPham VALUES ('SP0723000001',N'Áo Hoodie Unisex Tay Dài In Hình Form Regular','01','10f21hod005-18_1_2.jpg',550000,2);
INSERT INTO SanPham VALUES ('SP0723000002',N'Áo Vest Blazer Nam Tay Dài Kẻ Sọc Form Fitted','01','10f22ves007_black_3_1_1.jpg',1400000,12);
INSERT INTO SanPham VALUES ('SP0723000003',N'Đầm Ôm Ren Hoa Chỉ Đen Dây Trai','02','ff2306074dilabk1.jpg',2100000,5);
INSERT INTO SanPham VALUES ('SP0723000004',N'Đầm Quây Cúp Ngực Đỏ Xếp Ly Chéo','02','fs2303236dmword.jpg',2200000,7);
INSERT INTO SanPham VALUES ('SP0723000005',N'Váy Babydoll Cho Bé Cổ Vuông Chấm Bi','03','vnk5018-tim-04.jpg',299000,45);
INSERT INTO SanPham VALUES ('SP0723000006',N'Áo Khoác Gió Trẻ Em Chống Ngấm Nước Có Mũ','03','ao-khoac-tre-em-akk4001-cam-7.jpg',299000,0);
INSERT INTO SanPham VALUES ('SP0723000007',N'Giường hộc kéo Penny 1m6','04','giuong-penny-768x513.jpg',14630000,10);
INSERT INTO SanPham VALUES ('SP0723000008',N'Bàn ăn 1m8 Elegance màu tự nhiên','04','ban-ghe-an-phong-an-elegance-25-768x511.jpg',27800000,15);
INSERT INTO SanPham VALUES ('SP0723000009',N'Máy lạnh Panasonic Inverter 2.5 HP CU/CS-PU24XKH-8M','05','may-lanh-panaso_main.jpg',27000000,5);
INSERT INTO SanPham VALUES ('SP0723000010',N'Tủ Lạnh LG Inverter 635 Lít GR-X257JS','05','tu-lanh-lg-inve_main_895_1020.jpg',25400000,3);
INSERT INTO SanPham VALUES ('SP0723000011',N'Bộ Tô Chén Hàn Quốc Trắng Volakas Vân Đá Gốm Sứ Dày Viền Vàng Gold Sang Trọng','06','29_497a5845453a45b28.jpg',2490000,2);
INSERT INTO SanPham VALUES ('SP0723000012',N'Chậu Rửa Chén 1 Hộc Lớn Inox Phủ Nano Phím Đàn Hiển Thị Nhiệt Độ','06','060124751711.jpg',8900000,50);
INSERT INTO SanPham VALUES ('SP0723000013',N'Bình con bướm 60464K','07','BINH-BUTTER-FLIGHT-BLUE-50CM-60464K-768x495.jpg',6450000,20);
INSERT INTO SanPham VALUES ('SP0723000014',N'Bình Aila Turquoise','07','BINH-AILA-TURQUOISE-116170E-768x511.jpg',12420000,25);
INSERT INTO SanPham VALUES ('SP0723000015',N'Cá hồi hữu cơ Nauy fillet tươi','08','ca-hoi-organic-fillet-6.jpg',875000,0);
INSERT INTO SanPham VALUES ('SP0723000016',N'Thịt thăn lưng bò Mỹ (loại cao cấp)','08','than-lung-choice-6.jpg',945000,5);
INSERT INTO SanPham VALUES ('SP0723000017',N'[ĐÔNG LẠNH] Ba chỉ bò mỹ cuộn Thảo Tiến 500G','09','ba_chi_bo_my_cuon_thao_tien_500_g_b17eecbd6aef4fc5877ac1a3c0393e70_grande.jpg',149000,13);
INSERT INTO SanPham VALUES ('SP0723000018',N'[ĐÔNG LẠNH] Bò viên Okini 280G','09','kanzi__1__136404f4e81e49e1a2a286c8ec839bcc_grande.jpg',65000,5);
INSERT INTO SanPham VALUES ('SP0723000019',N'Lốc 5 Mì Lẩu Thái Koreno Chua Cay 75G','10','8936028642149-600x600.jpg',40500,0);
INSERT INTO SanPham VALUES ('SP0723000020',N'Cá Saba Sốt Cà Chua Roza 220G','10','8850511121617-600x600.jpg',41000,0);

--ALTER TABLE SanPham

SELECT * FROM SanPham


CREATE TABLE ChiNhanh
(
	IdChiNhanh VARCHAR(5) NOT NULL,
	TenChiNhanh NVARCHAR(100) NOT NULL,
	DiaChi NVARCHAR(255) NOT NULL,
	CONSTRAINT PK_ChiNhanh PRIMARY KEY(IdChiNhanh)
)

INSERT INTO ChiNhanh VALUES('CN1', N'Chi nhánh Tân Phú', N'Tp.HCM');
INSERT INTO ChiNhanh VALUES('CN2', N'Chi nhánh Bình Tân', N'Tp.HCM');
INSERT INTO ChiNhanh VALUES('CN3', N'Chi nhánh Bình Dương', N'Bình Dương');
-- Thêm nhiều chi nhánh khác tương tự ở đây
select*from ChiNhanh
CREATE TABLE QuayThanhToan
(
	IdQuayThanhToan VARCHAR(5) NOT NULL,
	IdChiNhanh VARCHAR(5) NOT NULL,
	TenQuay NVARCHAR(255),
	CONSTRAINT PK_QuayThanhToan PRIMARY KEY(IdQuayThanhToan),
	CONSTRAINT UNI_IdChiNhanh_TenQuay UNIQUE(IdChiNhanh,TenQuay),
	CONSTRAINT FK_QuayThanhToan_ChiNhanh FOREIGN KEY(IdChiNhanh) REFERENCES ChiNhanh(IdChiNhanh)
	
)
INSERT INTO QuayThanhToan (IdQuayThanhToan, IdChiNhanh, TenQuay)
VALUES
    ('QT1', 'CN1', N'Quầy 1'),
    ('QT2', 'CN1', N'Quầy 2'),
    ('QT3', 'CN2', N'Quầy 1'),
    ('QT4', 'CN3', N'Quầy 1');
-- Thêm nhiều quầy thanh toán khác tương tự ở đây

CREATE TABLE NguoiDung 
(
    IdNguoiDung VARCHAR(12) NOT NULL,
    TenTaiKhoan VARCHAR(255) NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    CONSTRAINT UNI_TenTaiKhoan UNIQUE(TenTaiKhoan),
    CONSTRAINT PK_NguoiDung PRIMARY KEY (IdNguoiDung),
);


INSERT INTO NguoiDung VALUES ('147000000001', 'admin','admin@123')
INSERT INTO NguoiDung VALUES ('147000000002', 'quanly','123456')
INSERT INTO NguoiDung VALUES ('147000000003', 'quanlykho','123456')
INSERT INTO NguoiDung VALUES ('147000000004', 'thungan','123456')
INSERT INTO NguoiDung VALUES ('147000000005', 'nhanvienbanhang','123456')
INSERT INTO NguoiDung VALUES ('147000000006', 'nhanvienkho','123456')
INSERT INTO NguoiDung VALUES ('147000000007', 'chamsockhachhang','123456')
INSERT INTO NguoiDung VALUES ('147000000008', 'thungan2','123456')
INSERT INTO NguoiDung VALUES ('147000000009', 'thungan3','123456')
INSERT INTO NguoiDung VALUES ('147000000010', 'nhanvienbanhang2','123456')
INSERT INTO NguoiDung VALUES ('147000000011', 'nhanvienbanhang3','123456')
SELECT * FROM NguoiDung

CREATE TABLE ChucVu
(
	IdChucVu CHAR(4) NOT NULL,
	TenChucVu NVARCHAR(100) NOT NULL,
	CONSTRAINT PK_ChucVu PRIMARY KEY(IdChucVu)
)

INSERT INTO ChucVu VALUES ('1000',N'Admin');
INSERT INTO ChucVu VALUES ('1001',N'Quản lý');
INSERT INTO ChucVu VALUES ('1002',N'Quản lý kho');
INSERT INTO ChucVu VALUES ('1003',N'Thu ngân');
INSERT INTO ChucVu VALUES ('1004',N'Nhân viên bán hàng');
INSERT INTO ChucVu VALUES ('1005',N'Nhân viên kho');
INSERT INTO ChucVu VALUES ('1006',N'Chăm sóc khách hàng');
INSERT INTO ChucVu VALUES ('1007',N'Khách hàng');
SELECT * FROM ChucVu


CREATE TABLE NhanVien
(
	IdNhanVien VARCHAR(12) NOT NULL,
	IdNguoiDung VARCHAR(12) NOT NULL,
	IdChucVu CHAR(4) NOT NULL,
	TenNhanVien NVARCHAR(50),
	NgaySinh DATE,
	GioiTinh NCHAR(5),
	DiaChi NVARCHAR(255),
	SoDienThoai CHAR(10),
	Email VARCHAR(100),
	CONSTRAINT PK_NhanVien PRIMARY KEY(IdNhanVien),
	CONSTRAINT UNI_IdNguoiDung_NhanVien UNIQUE(IdNguoiDung),
	CONSTRAINT UNI_SoDienThoai_NhanVien UNIQUE(SoDienThoai),
	CONSTRAINT UNI_Email_NhanVien UNIQUE(Email),
	CONSTRAINT FK_NhanVien_ChucVu FOREIGN KEY(IdChucVu) REFERENCES ChucVu(IdChucVu),
	CONSTRAINT FK_NhanVien_NguoiDung FOREIGN KEY(IdNguoiDung) REFERENCES NguoiDung(IdNguoiDung)
)

SET DATEFORMAT DMY
INSERT INTO NhanVien VALUES ('NV07230001','147000000001','1000',N'Vũ Văn Anh','13/02/2003',N'Nam',N'Tây Thạnh, Tân Phú, Thành phố Hồ Chí Minh','0393222222','fashionshop@gmail.com')
INSERT INTO NhanVien VALUES ('NV07230002','147000000002','1001',N'Hà Tri Thủy','17/01/2003',N'Nam',N'Tân Sơn Nhì, Tân Phú, Thành phố Hồ Chí Minh','0393222','hatrithuy@gmail.com')
INSERT INTO NhanVien VALUES ('NV07230003','147000000003','1002',N'Huỳnh Tuấn Khang','07/04/2003',N'Nam',N'Phường 7, Quận 5, Thành phố Hồ Chí Minh, Việt Nam','0393555222','khanghuynh@gmail.com')
INSERT INTO NhanVien VALUES ('NV07230004','147000000004','1003',N'Đinh Tuyết Anh','20/11/1990',N'Nữ',N'212-242 Đ. Độc Lập, Tân Thành, Tân Phú, Thành phố Hồ Chí Minh, Việt Nam','0393666222','tuyeanhdinh20@gmail.com')
INSERT INTO NhanVien VALUES ('NV07230005','147000000005','1004',N'Nguyễn Lê Huyền Trang','14/03/1998',N'Nữ',N'Bình Hưng Hòa A, Bình Hưng Hoà A, Bình Tân, Thành phố Hồ Chí Minh, Việt Nam','0393777222','trangnguyen14@gmail.com')
INSERT INTO NhanVien VALUES ('NV07230006','147000000006','1005',N'Huỳnh Thái Cường','21/06/2000',N'Nam',N'131 Đ. 26/3, Bình Hưng Hoà, Bình Tân, Thành phố Hồ Chí Minh, Việt Nam','0393000222','cuonghuynh21@gmail.com')
INSERT INTO NhanVien VALUES ('NV07230007','147000000007','1006',N'Đinh Phương Thảo','23/12/1992',N'Nữ',N'111 Gò Dầu, Tân Quý, Tân Phú, Thành phố Hồ Chí Minh, Việt Nam','0393111222','dinhphuongthao@gmail.com')
INSERT INTO NhanVien VALUES ('NV07230008','147000000008','1003',N'Phạm Thu Hoa','09/02/1996',N'Nữ',N'4 Lê Đình Thám, Tân Quý, Tân Phú, Thành phố Hồ Chí Minh 700000, Việt Nam','0393111444','hoapham@gmail.com')
INSERT INTO NhanVien VALUES ('NV07230009','147000000009','1003',N'Trần Anh Tuấn','26/06/1991',N'Nam',N'Hẻm 10 Đ. Cống Lỡ, Phường 15, Tân Bình, Thành phố Hồ Chí Minh, Việt Nam','0393111555','anhtuan@gmail.com')
INSERT INTO NhanVien VALUES ('NV07230010','147000000010','1004',N'Lê Minh hải','25/5/1997',N'Nam',N'34/20d Đ. Cống Lỡ, Phường 15, Tân Bình, Thành phố Hồ Chí Minh, Việt Nam','0393111777','leminhhai@gmail.com')
INSERT INTO NhanVien VALUES ('NV07230011','147000000011','1004',N'Trần Lệ Thu','05/04/1993',N'Nữ',N'6, Tân Thới Nhất, Hồ Chí Minh, Thành phố Hồ Chí Minh 700000, Việt Nam','0393111999','thutranle@gmail.com')
SELECT* FROM NhanVien

SELECT *
FROM NguoiDung ND INNER JOIN NhanVien NV ON ND.IdNguoiDung = ND.IdNguoiDung

CREATE TABLE KhoHang
(
	IdKhoHang VARCHAR(5) NOT NULL,
	IdChiNhanh VARCHAR(5) NOT NULL,
	TenKhoHang NVARCHAR(255) NOT NULL,
	IdSanPham VARCHAR(12) NOT NULL,
	DiaChiKhoHang NVARCHAR(255) NOT NULL,
	CONSTRAINT PK_KhoHang PRIMARY KEY(IdKhoHang),
	CONSTRAINT UNI_IdChiNhanh_TenKhoHang_IdSanPham UNIQUE(IdChiNhanh,TenKhoHang,IdSanPham),
	CONSTRAINT FK_KhoHang_ChiNhanh FOREIGN KEY(IdChiNhanh) REFERENCES ChiNhanh(IdChiNhanh),
	CONSTRAINT FK_KhoHang_IdSanPham FOREIGN KEY(IdSanPham) REFERENCES SanPham(IdSanPham)
)


	CREATE TABLE HoaDon (
	  IdHoaDon VARCHAR(14) NOT NULL,
	  IdKhachHang  VARCHAR(12) NOT NULL,
	  PhuongThucThanhToan NVARCHAR(30) NOT NULL,
	  
	  IdNhanVien VARCHAR(12) NOT NULL,
	  IdQuayThanhToan VARCHAR(5) NOT NULL,
	  ThoiGianTao DATETIME NOT NULL,
	  
	  CONSTRAINT PK_DonHang PRIMARY KEY(IdHoaDon),
	  CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY(IdKhachHang) REFERENCES KhachHang(IdKhachHang),
	  CONSTRAINT FK_HoaDon_NhanVien FOREIGN KEY(IdNhanVien) REFERENCES NhanVien(IdNhanVien),
	  CONSTRAINT FK_HoaDon_QuayThanhToan FOREIGN KEY(IdQuayThanhToan) REFERENCES QuayThanhToan(IdQuayThanhToan),
	  CONSTRAINT FK_HoaDon_ChiTietHD FOREIGN KEY(IdQuayThanhToan) REFERENCES QuayThanhToan(IdQuayThanhToan)
	);
	select *from HoaDon
	
	SET DATEFORMAT DMY
	INSERT INTO HoaDon VALUES ('HDBH001','KH001','Thu','NV07230001','QT1','19/9/2023')
	INSERT INTO HoaDon VALUES ('HDBH002','KH002','Thu','NV07230002','QT1','29/8/2023')
	INSERT INTO HoaDon VALUES ('HDBH003','KH003','Thu','NV07230001','QT2','18/9/2023')
	INSERT INTO HoaDon VALUES ('HDBH004','KH004','Thu','NV07230003','QT1','20/6/2023')

	CREATE TABLE ChiTietHoaDon (
	  IdHoaDon VARCHAR(14) NOT NULL,
	  IdSanPham VARCHAR(12) NOT NULL,
	  
	  SoLuong INT,
	  GiaBan BIGINT,
	  
	  CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY(IdHoaDon, IdSanPham),
	  CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY(IdHoaDon) REFERENCES HoaDon(IdHoaDon),
	  CONSTRAINT FK_ChiTietHoaDon_SanPham FOREIGN KEY(IdSanPham) REFERENCES SanPham(IdSanPham),
	);
	SET DATEFORMAT DMY
	INSERT INTO ChiTietHoaDon VALUES ('HDBH001','SP0723000001',2,NULL)
	INSERT INTO ChiTietHoaDon VALUES ('HDBH001','SP0723000005',3,NULL)
	INSERT INTO ChiTietHoaDon VALUES ('HDBH001','SP0723000013',1,NULL)
	INSERT INTO ChiTietHoaDon VALUES ('HDBH002','SP0723000013',2,NULL)
	INSERT INTO ChiTietHoaDon VALUES ('HDBH002','SP0723000010',1,NULL)
	INSERT INTO ChiTietHoaDon VALUES ('HDBH004','SP0723000010',1,NULL)
	INSERT INTO ChiTietHoaDon VALUES ('HDBH004','SP0723000020',1,NULL)
	INSERT INTO ChiTietHoaDon VALUES ('HDBH003','SP0723000019',2,NULL)

	select*from ChiTietHoaDon

CREATE TABLE NhaCungCap
(
    IdNhaCungCap VARCHAR(12) NOT NULL PRIMARY KEY, 
    TenNhaCungCap NVARCHAR(255) NOT NULL,          
    DiaChi NVARCHAR(255),                          
    SoDienThoai CHAR(10),                        
    Email VARCHAR(100)                            
);
select* from NhaCungCap
INSERT INTO NhaCungCap VALUES('NCC001',N'Công Ty thời trang Start',N'Tp.HCM','0985372617','start@gmail.com');
INSERT INTO NhaCungCap VALUES('NCC003',N'Công Ty đồ gia dụng QPZ',N'Hải Phòng','0985372543','qpz@gmail.com');
INSERT INTO NhaCungCap VALUES('NCC004',N'Hội liên hiệp lương thực thực phẩm',N'Bình Dương','0985372876','luongthuc@gmail.com');
INSERT INTO NhaCungCap VALUES('NCC005',N'Tập đoàn công nghệ Vi',N'Tp.HCM','0985376409','vi@gmail.com');
CREATE TABLE NhapHang
(
    IdNhapHang VARCHAR(12) NOT NULL PRIMARY KEY,
    IdNhanVien VARCHAR(12) NOT NULL,  
	IdNhaCungCap VARCHAR(12), 
    TongTien BIGINT ,
	
	PhuongThucThanhToan NVARCHAR(30) NOT NULL,
	ThoiGianTao DATETIME NOT NULL,
    CONSTRAINT FK_NhapHang_NhanVien FOREIGN KEY (IdNhanVien) REFERENCES NhanVien(IdNhanVien),
	CONSTRAINT FK_ChiTietHoaDonNhapHang_NhaCungCap FOREIGN KEY (IdNhaCungCap) REFERENCES NhaCungCap(IdNhaCungCap)

);
SET DATEFORMAT DMY
INSERT INTO NhapHang VALUES ('HDNH001','NV07230006','NCC001',NULL,'Chi','17/8/2023')
INSERT INTO NhapHang VALUES ('HDNH002','NV07230005','NCC005',NULL,'Chi','1/9/2023')
INSERT INTO NhapHang VALUES ('HDNH003','NV07230005','NCC003',NULL,'Chi','15/8/2023')
INSERT INTO NhapHang VALUES ('HDNH004','NV07230006','NCC004',NULL,'Chi','10/9/2023')
CREATE TABLE ChiTietHoaDonNhapHang (
  IdNhapHang VARCHAR(12) NOT NULL,
  IdSanPham VARCHAR(12) NOT NULL,

  SoLuong INT,
  GiaNhap BIGINT,
  
  CONSTRAINT PK_ChiTietHoaDonNhapHang PRIMARY KEY(IdNhapHang, IdSanPham),
  CONSTRAINT FK_ChiTietHoaDonNhapHang_NhapHang FOREIGN KEY(IdNhapHang) REFERENCES NhapHang(IdNhapHang),
  CONSTRAINT FK_ChiTietHoaDonNhapHang_SanPham FOREIGN KEY(IdSanPham) REFERENCES SanPham(IdSanPham)
);
select*from ChiTietHoaDonNhapHang
SET DATEFORMAT DMY
INSERT INTO ChiTietHoaDonNhapHang VALUES ('HDNH001','SP0723000001',30,390000);
INSERT INTO ChiTietHoaDonNhapHang VALUES ('HDNH001','SP0723000003',20,1800000);
INSERT INTO ChiTietHoaDonNhapHang VALUES ('HDNH002','SP0723000013',15,5800000);
INSERT INTO ChiTietHoaDonNhapHang VALUES ('HDNH001','SP0723000006',32,220000);
INSERT INTO ChiTietHoaDonNhapHang VALUES ('HDNH003','SP0723000004',30,1900000);
INSERT INTO ChiTietHoaDonNhapHang VALUES ('HDNH002','SP0723000003',30,1800000);
INSERT INTO ChiTietHoaDonNhapHang VALUES ('HDNH004','SP0723000001',30,390000);
INSERT INTO ChiTietHoaDonNhapHang VALUES ('HDNH004','SP0723000010',23,20000000);

--CREATE TABLE QuangCao (
--	IdQuangCao VARCHAR(12) NOT NULL,
--	TenQuangCao NVARCHAR(255) NOT NULL,
--	MoTaQuangCao NVARCHAR(255),
--	NgayBatDau DATE,
--	NgayKetThuc DATE,
--	IdSanPham VARCHAR(12) NOT NULL,
--	CONSTRAINT PK_QuangCao PRIMARY KEY(IdQuangCao),
--	CONSTRAINT FK_QuangCao_SanPham FOREIGN KEY(IdSanPham) REFERENCES SanPham(IdSanPham)
--);

--CREATE TABLE ChienDichQuangCao (
--	IdChienDich VARCHAR(12) NOT NULL,
--	TenChienDich NVARCHAR(255) NOT NULL,
--	IdQuangCao VARCHAR(12) NOT NULL,
--	CONSTRAINT PK_ChienDichQuangCao PRIMARY KEY(IdChienDich),
--	CONSTRAINT FK_ChienDichQuangCao_QuangCao FOREIGN KEY(IdQuangCao) REFERENCES QuangCao(IdQuangCao)
--);

--CREATE TABLE ChiTietChienDichQuangCao (
--	IdChiTietChienDich VARCHAR(12) NOT NULL,
--	IdChienDich VARCHAR(12) NOT NULL,
--	KenhQuangCao VARCHAR(12) NOT NULL,
--	SoLuotXem INT,
--	SoLuotClick INT,
--	CONSTRAINT PK_ChiTietChienDichQuangCao PRIMARY KEY(IdChiTietChienDich),
--	CONSTRAINT FK_ChiTietChienDichQuangCao_ChienDichQuangCao FOREIGN KEY(IdChienDich) REFERENCES ChienDichQuangCao(IdChienDich)
--);

--CREATE TABLE YkienKH
--(
--	IdFeedback VARCHAR(10) NOT NULL,
--	IdKhachHang VARCHAR(12) NOT NULL,
--	NgayYkien DATE,
--	NDYkien NVARCHAR(150),
--	CONSTRAINT PK_YkienKH PRIMARY KEY(IdFeedback),
--	CONSTRAINT FK_YKienKH_KhachHang FOREIGN KEY(IdKhachHang) REFERENCES KhachHang(IdKhachHang)
--)

CREATE VIEW BangChi AS
SELECT 
    NhapHang.IdNhapHang,
    NhanVien.TenNhanVien,
	NhaCungCap.TenNhaCungCap,
    SUM(ChiTietHoaDonNhapHang.GiaNhap * ChiTietHoaDonNhapHang.SoLuong) AS TongTien,
    NhapHang.ThoiGianTao,
    SUM(ChiTietHoaDonNhapHang.SoLuong) AS SoLuong
FROM 
    NhapHang,NhanVien,NhaCungCap,ChiTietHoaDonNhapHang
where
	NhapHang.IdNhapHang=ChiTietHoaDonNhapHang.IdNhapHang and NhapHang.IdNhanVien=NhanVien.IdNhanVien and NhapHang.IdNhaCungCap=NhaCungCap.IdNhaCungCap
GROUP BY 
    NhapHang.IdNhapHang,
    NhanVien.TenNhanVien,
	NhaCungCap.TenNhaCungCap,
    NhapHang.ThoiGianTao;
select * from BangChi
select * from ChiTietHoaDonNhapHang
drop view BangThu

CREATE VIEW BangThu AS
SELECT 
    HoaDon.IdHoaDon,
    SUM(ChiTietHoaDon.GiaBan * ChiTietHoaDon.SoLuong) AS TongTien,
    HoaDon.ThoiGianTao,
    NhanVien.TenNhanVien,
	KhachHang.TenKhachHang,
    HoaDon.IdQuayThanhToan,
    SUM(ChiTietHoaDon.SoLuong) AS SoLuong
FROM 
    HoaDon,ChiTietHoaDon,KhachHang,NhanVien
where HoaDon.IdNhanVien=NhanVien.IdNhanVien and HoaDon.IdHoaDon=ChiTietHoaDon.IdHoaDon and HoaDon.IdKhachHang=KhachHang.IdKhachHang
GROUP BY 
    HoaDon.IdHoaDon,
    HoaDon.ThoiGianTao,
    NhanVien.TenNhanVien,
	KhachHang.TenKhachHang,
    HoaDon.IdQuayThanhToan;
select * from BangThu

CREATE TRIGGER CapNhatGiaBan
ON ChiTietHoaDon
AFTER INSERT, UPDATE
AS
BEGIN
    -- Cập nhật cột "GiaBan" cho các bản ghi được thêm mới hoặc cập nhật
    UPDATE ChiTietHoaDon
    SET ChiTietHoaDon.GiaBan = SanPham.GiaGoc - (SanPham.GiaGoc * (SanPham.GiamGia / 100))
    FROM ChiTietHoaDon
    INNER JOIN SanPham ON ChiTietHoaDon.IdSanPham = SanPham.IdSanPham
    INNER JOIN inserted ON ChiTietHoaDon.IdHoaDon = inserted.IdHoaDon AND ChiTietHoaDon.IdSanPham = inserted.IdSanPham; 
END;

select * from ChiTietHoaDon

