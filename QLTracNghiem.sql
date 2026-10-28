use master
go
if exists(select * from sys.databases where name='QuanLyTracNghiem')
drop database QuanLyTracNghiem
go
create database QuanLyTracNghiem
go
use QuanLyTracNghiem

create table tblThongTinHocSinh(
	MaHocSinh varchar(15) primary key,
	TenHocSinh nvarchar(max),
	GioiTinh nvarchar(5),
	NgaySinh date,
	LopHoc nvarchar(15)
);
go

create table tblThongTinKhoDeThi(
	MaDeThi varchar(15) primary key,
	MonDeThi nvarchar(50),
	NoiDungThi nvarchar(max),
	MucDo nvarchar(20)
);
go

create table tblThongTinBaiThiCuaHocSinh(
	MaBaiThi varchar(15) primary key,
	MaHocSinh varchar(15),
	MaDeThi varchar(15),
	ThoiGianThi datetime,
	foreign key (MaHocSinh) references tblThongTinHocSinh(MaHocSinh),
	foreign key (MaDeThi) references tblThongTinKhoDeThi(MaDeThi)
);
go
create trigger auto_IDBaiThi  on tblThongTinBaiThiCuaHocSinh instead of insert as
begin
	declare @ID varchar(15)
	declare @number int
	if not exists(select * from tblThongTinBaiThiCuaHocSinh) set @number=0
	else 
	select @number=max(right(MaBaiThi,3)) from tblThongTinBaiThiCuaHocSinh
	set @number=@number+1
	set @ID='BT'+right('000'+cast(@number as varchar(5)),3)
	insert into tblThongTinBaiThiCuaHocSinh
	select @ID, MaHocSinh, MaDeThi, Getdate()
	from inserted
end
go
create table tblThongTinKetQuaBaiThi(
	MaKetQua varchar(15) primary key,
	MaBaiThi varchar(15),
	DiemSo int,
	TrangThai nvarchar(20),
	foreign key (MaBaiThi) references tblThongTinBaiThiCuaHocSinh(MaBaiThi)
);
go
create trigger auto_IDKQBaiThi  on tblThongTinKetQuaBaiThi instead of insert as
begin
	declare @ID varchar(15)
	declare @number int
	if not exists(select * from tblThongTinKetQuaBaiThi) set @number=0
	else 
	select @number=max(right(MaKetQua,3)) from tblThongTinKetQuaBaiThi
	set @number=@number+1
	set @ID='KQ'+right('000'+cast(@number as varchar(5)),3)
	insert into tblThongTinKetQuaBaiThi
	select @ID, MaBaiThi, DiemSo,TrangThai
	from inserted
end
go
create table tblDangNhap
(
	TaiKhoan varchar(220) primary key,
	MatKhau varchar(220)
)
insert into tblDangNhap
-- Mat khau goc: admin123 (da hash bang SHA256 + Base64, giong ham HashPassword trong account.cs)
values('admin','JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=')
go
INSERT INTO tblThongTinHocSinh (MaHocSinh, TenHocSinh, GioiTinh, NgaySinh, LopHoc)
VALUES ('HS001', N'Nguyễn Văn A', N'Nam', '2005-01-10', '10A1'),
       ('HS002', N'Trần Thị B', N'Nữ', '2005-02-15', '10A2'),
       ('HS003', N'Lê Văn C', N'Nam', '2005-03-20', '10A3'),
       ('HS004', N'Phạm Thị D', N'Nữ', '2005-04-25', '10A4'),
       ('HS005', N'Hoàng Vănn E', N'Nam', '2005-05-30', '10A5'),
       ('HS006', N'Nguyễn Thị Lan',  N'Nữ',  '2005-06-12', '10A1'),
       ('HS007', N'Trần Văn Minh',   N'Nam', '2005-07-08', '10A2'),
       ('HS008', N'Lê Thị Hoa',      N'Nữ',  '2005-08-22', '10A3'),
       ('HS009', N'Phạm Văn Hùng',   N'Nam', '2005-09-15', '10A4'),
       ('HS010', N'Hoàng Thị Mai',   N'Nữ',  '2005-10-03', '10A5'),
       ('HS011', N'Võ Văn Tú',       N'Nam', '2005-11-17', '10A1'),
       ('HS012', N'Đặng Thị Thu',    N'Nữ',  '2005-12-25', '10A2'),
       ('HS013', N'Bùi Văn Khoa',    N'Nam', '2006-01-09', '10A3'),
       ('HS014', N'Ngô Thị Linh',    N'Nữ',  '2006-02-14', '10A4'),
       ('HS015', N'Đinh Văn Sơn',    N'Nam', '2006-03-28', '10A5');
go
INSERT INTO tblThongTinKhoDeThi (MaDeThi, MonDeThi, NoiDungThi, MucDo)
VALUES ('DT001', N'Toán', N'Bài tập vi tích phân', N'Khó'),
       ('DT002', N'Lý', N'Bài điện tích', N'Trung bình'),
       ('DT003', N'Hóa', N'Bài tập oxi khóa khử', N'Khó'),
       ('DT004', N'Van', N'Phân tích thơ 7 chữ', N'Trung bình'),
       ('DT005', N'Anh', N'Ngữ pháp chủ đề du lịch', N'D?'),
       ('DT006', N'Sử', N'Lịch sử việt nam', N'Trung bình'),
       ('DT007', N'Điạ', N'Vùng kình tế đồng bằng sông cửu long', N'Khó'),
       ('DT008', N'GDCD', N'Pháp luật và xã hội', N'Trung bình'),
       ('DT009', N'Sinh', N'Bài tập vi sinh', N'Khó'),
       ('DT010', N'Toán', N'Bài tập đạo hàm', N'Trung bình');
go
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES ( 'HS001', 'DT001', '2023-01-05')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES('HS002', 'DT002', '2023-01-06')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES( 'HS003', 'DT003', '2023-01-07')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES('HS004', 'DT004', '2023-01-08')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES('HS005', 'DT005', '2023-01-09')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES( 'HS001', 'DT006', '2023-01-10')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES( 'HS002', 'DT007', '2023-01-11')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES( 'HS003', 'DT008', '2023-01-12')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES( 'HS004', 'DT009', '2023-01-13')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES( 'HS005', 'DT010', '2023-01-14')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES( 'HS001', 'DT001', '2023-01-15')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES( 'HS002', 'DT002', '2023-01-16')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES( 'HS003', 'DT003', '2023-01-17')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES( 'HS004', 'DT004', '2023-01-18')
INSERT INTO tblThongTinBaiThiCuaHocSinh ( MaHocSinh, MaDeThi, ThoiGianThi)
VALUES( 'HS005', 'DT005', '2023-01-19');
INSERT INTO tblThongTinBaiThiCuaHocSinh (MaHocSinh, MaDeThi) VALUES ('HS006', 'DT009');
INSERT INTO tblThongTinBaiThiCuaHocSinh (MaHocSinh, MaDeThi) VALUES ('HS007', 'DT001');
INSERT INTO tblThongTinBaiThiCuaHocSinh (MaHocSinh, MaDeThi) VALUES ('HS008', 'DT002');
INSERT INTO tblThongTinBaiThiCuaHocSinh (MaHocSinh, MaDeThi) VALUES ('HS009', 'DT003');
INSERT INTO tblThongTinBaiThiCuaHocSinh (MaHocSinh, MaDeThi) VALUES ('HS010', 'DT005');
INSERT INTO tblThongTinBaiThiCuaHocSinh (MaHocSinh, MaDeThi) VALUES ('HS011', 'DT006');
INSERT INTO tblThongTinBaiThiCuaHocSinh (MaHocSinh, MaDeThi) VALUES ('HS012', 'DT007');
INSERT INTO tblThongTinBaiThiCuaHocSinh (MaHocSinh, MaDeThi) VALUES ('HS013', 'DT008');
INSERT INTO tblThongTinBaiThiCuaHocSinh (MaHocSinh, MaDeThi) VALUES ('HS014', 'DT010');
INSERT INTO tblThongTinBaiThiCuaHocSinh (MaHocSinh, MaDeThi) VALUES ('HS015', 'DT009');
go
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ001', 'BT001', 85, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES('KQ002', 'BT002', 78, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ003', 'BT003', 90, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ004', 'BT004', 65, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ005', 'BT005', 95, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ006', 'BT006', 80, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ007', 'BT007', 75, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ008', 'BT008', 70, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ009', 'BT009', 88, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ010', 'BT010', 92, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ011', 'BT011', 83, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ012', 'BT012', 79, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ013', 'BT013', 91, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ014', 'BT014', 72, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaKetQua, MaBaiThi, DiemSo, TrangThai)
VALUES ('KQ015', 'BT015', 86, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaBaiThi, DiemSo, TrangThai) VALUES ('BT016', 80,  N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaBaiThi, DiemSo, TrangThai) VALUES ('BT017', 40,  N'Rớt')
INSERT INTO tblThongTinKetQuaBaiThi (MaBaiThi, DiemSo, TrangThai) VALUES ('BT018', 60,  N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaBaiThi, DiemSo, TrangThai) VALUES ('BT019', 100, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaBaiThi, DiemSo, TrangThai) VALUES ('BT020', 20,  N'Rớt')
INSERT INTO tblThongTinKetQuaBaiThi (MaBaiThi, DiemSo, TrangThai) VALUES ('BT021', 80,  N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaBaiThi, DiemSo, TrangThai) VALUES ('BT022', 60,  N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaBaiThi, DiemSo, TrangThai) VALUES ('BT023', 40,  N'Rớt')
INSERT INTO tblThongTinKetQuaBaiThi (MaBaiThi, DiemSo, TrangThai) VALUES ('BT024', 100, N'Đậu')
INSERT INTO tblThongTinKetQuaBaiThi (MaBaiThi, DiemSo, TrangThai) VALUES ('BT025', 60,  N'Đậu')
go

create proc Inser_Table_ThongTinHocSinh
(
		@MaHocSinh varchar(15),
		@TenHocSinh nvarchar(max),
		@GioiTinh nvarchar(5),
		@NgaySinh date,
		@LopHoc nvarchar(15)
)
as
begin
	insert into tblThongTinHocSinh
	values(@MaHocSinh,@TenHocSinh,@GioiTinh,@NgaySinh,@LopHoc);
end
go
create proc Delete_Table_ThongTinHocSinh
(
	@MaHocSinh varchar(15)
) as
begin
	delete from tblThongTinHocSinh where MaHocSinh=@MaHocSinh
end
go
create proc Update_Table_ThongTinHocSinh
(
	@MaHocSinh varchar(15),
	@TenHocSinh nvarchar(max),
	@GioiTinh nvarchar(5),
	@NgaySinh date,
	@LopHoc nvarchar(15)
) as
begin
	update tblThongTinHocSinh set 
	MaHocSinh=@MaHocSinh,
	TenHocSinh=@TenHocSinh,
	GioiTinh=@GioiTinh,
	NgaySinh=@NgaySinh,
	LopHoc=@LopHoc
	where MaHocSinh=@MaHocSinh
end
go
create proc Inser_Table_ThongTinKhoDeThi
(
		@MaDeThi nvarchar(15),
		@MonDeThi nvarchar(50),
		@NoiDungThi nvarchar(max),
		@MucDo nvarchar(20)
)
as
begin
	insert into tblThongTinKhoDeThi
	values(@MaDeThi,@MonDeThi,@NoiDungThi,@MucDo);
end
go
create proc Delete_Table_ThongTinKhoDeThi
(
	@MaDeThi varchar(15)
) as
begin
	delete from tblThongTinKhoDeThi where MaDeThi=@MaDeThi
end
go
create proc Update_Table_ThongTinKhoDeThi
(
		@MaDeThi nvarchar(15),
		@MonDeThi nvarchar(50),
		@NoiDungThi nvarchar(max),
		@MucDo nvarchar(20)
) as
begin
	update tblThongTinKhoDeThi set 
	MaDeThi=@MaDeThi,
	MonDeThi=@MonDeThi,
	NoiDungThi=@NoiDungThi,
	MucDo=@MucDo
	where MaDeThi=@MaDeThi
end
go

create proc Inser_Table_ThongTinBaiThiCuaHocSinh
(
		@MaBaiThi nvarchar(15) ,
		@MaHocSinh varchar(15),
		@MaDeThi nvarchar(15),
		@ThoiGianThi datetime
)
as
begin
	insert into tblThongTinBaiThiCuaHocSinh
	values(@MaBaiThi,@MaHocSinh,@MaDeThi,@ThoiGianThi);
end
go
create proc Delete_Table_ThongTinBaiThiCuaHocSinh
(
	@MaBaiThi nvarchar(15)
) as
begin
	delete from tblThongTinBaiThiCuaHocSinh where MaBaiThi=@MaBaiThi
end
go
create proc Update_Table_ThongTinBaiThiCuaHocSinh
(
		@MaBaiThi nvarchar(15) ,
		@MaHocSinh varchar(15),
		@MaDeThi nvarchar(15),
		@ThoiGianThi datetime
) as
begin
	update tblThongTinBaiThiCuaHocSinh set 
	MaBaiThi=@MaBaiThi,
	MaHocSinh=@MaHocSinh,
	MaDeThi=@MaDeThi,
	ThoiGianThi=@ThoiGianThi
	where MaBaiThi=@MaBaiThi
end
go
create proc Inser_Table_ThongTinKetQuaBaiThi
(
		@MaKetQua nvarchar(15),
		@MaBaiThi nvarchar(15),
		@DiemSo int,
		@TrangThai nvarchar(20)
)
as
begin
	insert into tblThongTinKetQuaBaiThi
	values(@MaKetQua,@MaBaiThi,@DiemSo,@TrangThai);
end
go
create proc Delete_Table_ThongTinKetQuaBaiThi
(
	@MaKetQua nvarchar(15)
) as
begin
	delete from tblThongTinKetQuaBaiThi where MaKetQua=@MaKetQua
end
go
create proc Update_Table_ThongTinKetQuaBaiThi
(
		@MaKetQua nvarchar(15),
		@MaBaiThi nvarchar(15),
		@DiemSo int,
		@TrangThai nvarchar(20)
) as
begin
	update tblThongTinKetQuaBaiThi set 
	MaKetQua=@MaKetQua,
	MaBaiThi=@MaBaiThi,
	DiemSo=@DiemSo,
	TrangThai=@TrangThai
	where MaKetQua=@MaKetQua
end
go
create view vw_ThongTinDiemHocSinh as
select TenHocSinh ,hs.MaHocSinh,LopHoc ,MonDeThi,btchs.MaDeThi,ThoiGianThi ,
DiemSo ,TrangThai
from tblThongTinHocSinh hs, tblThongTinKhoDeThi kdt, tblThongTinBaiThiCuaHocSinh btchs,tblThongTinKetQuaBaiThi kqbt
where hs.MaHocSinh=btchs.MaHocSinh and btchs.MaDeThi=kdt.MaDeThi and btchs.MaBaiThi=kqbt.MaBaiThi
go
create proc ThongTinDiemHocSinh
(@TimKiem nvarchar(max))
as
begin
select TenHocSinh as N'Tên học sinh',hs.MaHocSinh as N'Mã học sinh',LopHoc as N'Lớp học',MonDeThi as N'Môn đề thi',btchs.MaDeThi as N'Mã đề thi',ThoiGianThi as N'Thời gian thi',
DiemSo as N'Điểm số'
from tblThongTinHocSinh hs, tblThongTinKhoDeThi kdt, tblThongTinBaiThiCuaHocSinh btchs,tblThongTinKetQuaBaiThi kqbt
where hs.MaHocSinh=btchs.MaHocSinh and btchs.MaDeThi=kdt.MaDeThi and btchs.MaBaiThi=kqbt.MaBaiThi and 
(TenHocSinh like N'%'+@TimKiem +'%' or hs.MaHocSinh like  N'%'+@TimKiem +'%' or LopHoc like N'%'+@TimKiem+'%')
end
go

create table tblCauHoi
(
	MaCauHoi varchar(15) primary key,
	MaDeThi varchar(15) foreign key references tblThongTinKhoDeThi(MaDeThi),
	cauHoi nvarchar(max),
	CauA nvarchar(max),
	CauB nvarchar(max),
	CauC nvarchar(max),
	CauD nvarchar(max),
	DapAnA bit,
	DapAnB bit,
	DapAnC bit,
	DapAnD bit,
	KetQua nvarchar(15)
)
INSERT INTO tblCauHoi
VALUES 
('CH001','DT009',N'ADN có tác dụng gì trong tế bào?', N'Chứa thông tin di truyền', N'Điều chỉnh hoạt động tế bào', N'Tạo năng lượng', N'Chứa các enzyme', 1, 0, 0, 0, N'A'),
('CH002','DT009',N'Cơ quan nào chịu trách nhiệm sản xuất insuline?', N'Thận', N'Gan', N'Tuỵ', N'Tuyến tụy', 0, 0, 0, 1, N'D'),
('CH003','DT009',N'Quá trình nào sau đây không liên quan đến quang hợp?', N'Nhận diện ánh sáng', N'Tách nước', N'Sản xuất ATP', N'Sản xuất glucose', 0, 0, 1, 0, N'C'),
('CH004','DT009',N'Trong quá trình trao đổi khí ở động vật, khí nào là chất thải?', N'Oxy', N'CO2', N'H2O', N'N2', 0, 1, 0, 0, N'B'),
('CH005','DT009',N'Cơ quan nào có nhiệm vụ lọc máu?', N'Thận', N'Gan', N'Phổi', N'Tim', 1, 0, 0, 0, N'A'),
('CH006','DT009',N'Loại nào sau đây là loại tế bào không có nhân?', N'Tế bào thần kinh', N'Tế bào cơ', N'Tế bào máu', N'Tế bào bao quy đầu', 0, 1, 0, 0, N'B'),
('CH007','DT009',N'Nơi nào trong tế bào thực hiện quá trình trao đổi chất?', N'Mitochondria', N'ER', N'Ribosome', N'Nucleus', 1, 0, 0, 0, N'A'),
('CH008', 'DT009',N'Cơ quan nào chịu trách nhiệm sản xuất hormon nữ?', N'Tuỵ', N'Tuyến vú', N'Thận', N'Tuyến yên', 0, 1, 0, 0, N'B'),
('CH009','DT009',N'Chất gì có mặt nhiều nhất trong tế bào?', N'Nước', N'Protein', N'Carbohydrate', N'Lipid', 1, 0, 0, 0, N'A'),
('CH010','DT009',N'Quá trình nào dưới đây không liên quan đến tế bào?', N'Phân cấp', N'Nhân đôi DNA', N'Tiêu hóa', N'Phát triển', 0, 0, 1, 0, N'C'),
-- DT001 - Toán: Vi tích phân
('CH011','DT001',N'Đạo hàm của hàm số sin(x) là gì?', N'cos(x)', N'-cos(x)', N'-sin(x)', N'tan(x)', 1, 0, 0, 0, N'A'),
('CH012','DT001',N'Tích phân của hàm số 1/x là gì?', N'ln|x| + C', N'x² + C', N'1/x² + C', N'e^x + C', 1, 0, 0, 0, N'A'),
('CH013','DT001',N'Giới hạn lim(x→0) sin(x)/x bằng bao nhiêu?', N'0', N'Vô cùng', N'1', N'-1', 0, 0, 1, 0, N'C'),
('CH014','DT001',N'Đạo hàm của e^x là gì?', N'xe^(x-1)', N'e^x', N'ln(x)', N'1/x', 0, 1, 0, 0, N'B'),
('CH015','DT001',N'Quy tắc đạo hàm tích (uv)'' bằng gì?', N'u''v''', N'u''v + uv''', N'u''v - uv''', N'uv''', 0, 1, 0, 0, N'B'),
-- DT002 - Lý: Điện tích
('CH016','DT002',N'Đơn vị của điện tích trong hệ SI là gì?', N'Volt', N'Ampere', N'Coulomb', N'Ohm', 0, 0, 1, 0, N'C'),
('CH017','DT002',N'Theo định luật Coulomb, lực điện tỉ lệ nghịch với...', N'Tích hai điện tích', N'Bình phương khoảng cách', N'Khoảng cách', N'Hằng số điện môi', 0, 1, 0, 0, N'B'),
('CH018','DT002',N'Điện dung của tụ điện có đơn vị là gì?', N'Henry', N'Farad', N'Ohm', N'Tesla', 0, 1, 0, 0, N'B'),
('CH019','DT002',N'Chiều của đường sức điện trường đi từ đâu đến đâu?', N'Từ âm sang dương', N'Từ dương sang âm', N'Từ trái sang phải', N'Từ trên xuống dưới', 0, 1, 0, 0, N'B'),
('CH020','DT002',N'Điện trường được ký hiệu bằng chữ gì?', N'B', N'F', N'E', N'V', 0, 0, 1, 0, N'C'),
-- DT003 - Hóa: Oxi hóa - Khử
('CH021','DT003',N'Chất nào bị oxi hóa trong phản ứng oxi hóa khử?', N'Chất nhận electron', N'Chất nhường electron', N'Chất trung tính', N'Dung môi', 0, 1, 0, 0, N'B'),
('CH022','DT003',N'Chất oxi hóa là chất nào?', N'Nhường electron', N'Nhận electron', N'Không thay đổi số oxi hóa', N'Tạo kết tủa', 0, 1, 0, 0, N'B'),
('CH023','DT003',N'Số oxi hóa của O trong H₂O là bao nhiêu?', N'+2', N'+1', N'-2', N'0', 0, 0, 1, 0, N'C'),
('CH024','DT003',N'Trong phản ứng Fe + CuSO₄ → FeSO₄ + Cu, chất nào bị khử?', N'Fe', N'SO₄²⁻', N'Cu²⁺', N'FeSO₄', 0, 0, 1, 0, N'C'),
('CH025','DT003',N'Số oxi hóa của H trong HCl là bao nhiêu?', N'-1', N'0', N'+1', N'+2', 0, 0, 1, 0, N'C'),
-- DT004 - Văn: Thơ 7 chữ
('CH026','DT004',N'Thơ 7 chữ còn được gọi là thể thơ gì?', N'Thất ngôn', N'Ngũ ngôn', N'Lục bát', N'Song thất lục bát', 1, 0, 0, 0, N'A'),
('CH027','DT004',N'Mỗi câu trong thơ 7 chữ có bao nhiêu tiếng?', N'5', N'6', N'7', N'8', 0, 0, 1, 0, N'C'),
('CH028','DT004',N'Thơ thất ngôn tứ tuyệt có bao nhiêu câu?', N'2', N'4', N'6', N'8', 0, 1, 0, 0, N'B'),
('CH029','DT004',N'Bài thơ "Nam quốc sơn hà" được viết theo thể thơ nào?', N'Thất ngôn tứ tuyệt', N'Lục bát', N'Ngũ ngôn', N'Tự do', 1, 0, 0, 0, N'A'),
('CH030','DT004',N'Vần trong thơ 7 chữ thường gieo ở vị trí nào?', N'Tiếng thứ 1', N'Tiếng thứ 7', N'Tiếng thứ 3', N'Tiếng thứ 5', 0, 1, 0, 0, N'B'),
-- DT005 - Anh: Ngữ pháp du lịch
('CH031','DT005',N'Which sentence is correct?', N'I am go to the airport', N'I am going to the airport', N'I going to the airport', N'I go to the airport now', 0, 1, 0, 0, N'B'),
('CH032','DT005',N'What does "departure" mean in travel?', N'Arrival', N'Boarding', N'Leaving a place', N'Check-in', 0, 0, 1, 0, N'C'),
('CH033','DT005',N'Choose the correct preposition: "The flight arrives ___ 3 PM."', N'in', N'on', N'at', N'by', 0, 0, 1, 0, N'C'),
('CH034','DT005',N'What is the past tense of "fly"?', N'flied', N'flown', N'flew', N'flying', 0, 0, 1, 0, N'C'),
('CH035','DT005',N'Which word means a place where planes take off and land?', N'Harbor', N'Station', N'Airport', N'Terminal', 0, 0, 1, 0, N'C'),
-- DT006 - Sử: Lịch sử Việt Nam
('CH036','DT006',N'Chiến thắng Điện Biên Phủ diễn ra vào năm nào?', N'1945', N'1950', N'1954', N'1968', 0, 0, 1, 0, N'C'),
('CH037','DT006',N'Ai là chủ tịch nước đầu tiên của nước Việt Nam Dân chủ Cộng hòa?', N'Trần Phú', N'Võ Nguyên Giáp', N'Hồ Chí Minh', N'Lê Duẩn', 0, 0, 1, 0, N'C'),
('CH038','DT006',N'Ngày 30/4/1975 đánh dấu sự kiện gì?', N'Quốc khánh', N'Ngày thành lập Đảng', N'Giải phóng miền Nam', N'Cách mạng tháng Tám', 0, 0, 1, 0, N'C'),
('CH039','DT006',N'Triều đại nào kéo dài lâu nhất trong lịch sử phong kiến Việt Nam?', N'Nhà Lý', N'Nhà Trần', N'Nhà Lê', N'Nhà Nguyễn', 0, 0, 1, 0, N'C'),
('CH040','DT006',N'Khởi nghĩa Hai Bà Trưng nổ ra vào năm nào?', N'39 SCN', N'40 SCN', N'100 SCN', N'179 TCN', 0, 1, 0, 0, N'B'),
-- DT007 - Địa: ĐBSCL
('CH041','DT007',N'Đồng bằng sông Cửu Long thuộc hạ lưu con sông nào?', N'Sông Hồng', N'Sông Đà', N'Sông Mê Kông', N'Sông Đồng Nai', 0, 0, 1, 0, N'C'),
('CH042','DT007',N'Tỉnh nào không thuộc vùng Đồng bằng sông Cửu Long?', N'Cần Thơ', N'An Giang', N'Bình Dương', N'Cà Mau', 0, 0, 1, 0, N'C'),
('CH043','DT007',N'Sản phẩm nông nghiệp chủ lực của ĐBSCL là gì?', N'Cà phê', N'Cao su', N'Lúa gạo', N'Hồ tiêu', 0, 0, 1, 0, N'C'),
('CH044','DT007',N'ĐBSCL có bao nhiêu tỉnh/thành phố trực thuộc trung ương?', N'11', N'12', N'13', N'14', 0, 0, 1, 0, N'C'),
('CH045','DT007',N'Tỉnh nào có diện tích lớn nhất ở ĐBSCL?', N'Kiên Giang', N'Long An', N'An Giang', N'Cà Mau', 1, 0, 0, 0, N'A'),
-- DT008 - GDCD: Pháp luật
('CH046','DT008',N'Hiến pháp Việt Nam hiện hành được ban hành năm nào?', N'1992', N'2001', N'2013', N'2015', 0, 0, 1, 0, N'C'),
('CH047','DT008',N'Độ tuổi đủ để có năng lực hành vi dân sự đầy đủ là bao nhiêu?', N'16 tuổi', N'17 tuổi', N'18 tuổi', N'21 tuổi', 0, 0, 1, 0, N'C'),
('CH048','DT008',N'Cơ quan quyền lực nhà nước cao nhất của Việt Nam là gì?', N'Chính phủ', N'Quốc hội', N'Tòa án nhân dân tối cao', N'Chủ tịch nước', 0, 1, 0, 0, N'B'),
('CH049','DT008',N'Nguyên tắc nào thể hiện sự công bằng trong pháp luật?', N'Pháp luật chỉ áp dụng cho người nghèo', N'Mọi người đều bình đẳng trước pháp luật', N'Người giàu được ưu tiên', N'Pháp luật do dân tự đặt ra', 0, 1, 0, 0, N'B'),
('CH050','DT008',N'Vi phạm hành chính là vi phạm pháp luật có mức độ nguy hiểm như thế nào?', N'Rất cao', N'Cao', N'Thấp hơn tội phạm', N'Bằng tội phạm', 0, 0, 1, 0, N'C'),
-- DT010 - Toán: Đạo hàm
('CH051','DT010',N'Đạo hàm của hàm số f(x) = x² là gì?', N'x', N'2x', N'x²/2', N'2', 0, 1, 0, 0, N'B'),
('CH052','DT010',N'Đạo hàm của hàm hằng f(x) = 5 là gì?', N'5', N'1', N'0', N'x', 0, 0, 1, 0, N'C'),
('CH053','DT010',N'Đạo hàm của f(x) = x³ là gì?', N'x²', N'3x²', N'3x', N'x³/3', 0, 1, 0, 0, N'B'),
('CH054','DT010',N'Nếu f(x) = ln(x) thì f''(x) bằng gì?', N'e^x', N'x', N'1/x', N'ln(x)/x', 0, 0, 1, 0, N'C'),
('CH055','DT010',N'Đạo hàm của f(x) = cos(x) là gì?', N'sin(x)', N'-sin(x)', N'cos(x)', N'-cos(x)', 0, 1, 0, 0, N'B'),
-- DT001 - Toán Vi tích phân (bổ sung)
('CH056','DT001',N'Quy tắc đạo hàm thương (u/v)'' bằng gì?', N'u''v/v²', N'(u''v - uv'')/v²', N'(u''v + uv'')/v²', N'uv''/v²', 0, 1, 0, 0, N'B'),
('CH057','DT001',N'Tích phân ∫x dx bằng gì?', N'x + C', N'x²/2 + C', N'x² + C', N'2x + C', 0, 1, 0, 0, N'B'),
('CH058','DT001',N'Đạo hàm của ln(x) là gì?', N'1/x', N'x', N'e^x', N'1/x²', 1, 0, 0, 0, N'A'),
('CH059','DT001',N'Hàm số nào sau đây có đạo hàm bằng chính nó?', N'sin(x)', N'cos(x)', N'e^x', N'x²', 0, 0, 1, 0, N'C'),
('CH060','DT001',N'Đạo hàm của hàm số f(x) = 5x³ là gì?', N'5x²', N'15x', N'15x²', N'x³', 0, 0, 1, 0, N'C'),
-- DT002 - Lý Điện tích (bổ sung)
('CH061','DT002',N'Điện trường đều là điện trường có tính chất gì?', N'Hướng thay đổi theo vị trí', N'Độ lớn và hướng không đổi', N'Độ lớn thay đổi', N'Chỉ tồn tại trong chân không', 0, 1, 0, 0, N'B'),
('CH062','DT002',N'Điện thế tại một điểm có đơn vị là gì?', N'Coulomb', N'Ampere', N'Volt', N'Ohm', 0, 0, 1, 0, N'C'),
('CH063','DT002',N'Trong tụ điện, năng lượng được tích trữ dưới dạng?', N'Năng lượng cơ học', N'Năng lượng điện trường', N'Năng lượng nhiệt', N'Năng lượng hóa học', 0, 1, 0, 0, N'B'),
('CH064','DT002',N'Hiệu điện thế giữa hai bản tụ điện được ký hiệu là?', N'I', N'Q', N'U', N'R', 0, 0, 1, 0, N'C'),
('CH065','DT002',N'Khi tăng khoảng cách giữa hai điện tích lên 2 lần, lực Coulomb thay đổi thế nào?', N'Tăng 2 lần', N'Giảm 2 lần', N'Tăng 4 lần', N'Giảm 4 lần', 0, 0, 0, 1, N'D'),
-- DT003 - Hóa Oxi hóa khử (bổ sung)
('CH066','DT003',N'Quá trình oxi hóa là quá trình?', N'Nhường electron và số oxi hóa tăng', N'Nhận electron', N'Số oxi hóa giảm', N'Tạo liên kết ion', 1, 0, 0, 0, N'A'),
('CH067','DT003',N'Trong phản ứng Zn + H₂SO₄ → ZnSO₄ + H₂, chất khử là?', N'H₂SO₄', N'ZnSO₄', N'Zn', N'H₂', 0, 0, 1, 0, N'C'),
('CH068','DT003',N'Số oxi hóa của Fe trong Fe₂O₃ là bao nhiêu?', N'+2', N'+3', N'+4', N'+6', 0, 1, 0, 0, N'B'),
('CH069','DT003',N'Chất nào sau đây vừa có tính oxi hóa vừa có tính khử?', N'O₂', N'F₂', N'SO₂', N'Cl₂', 0, 0, 1, 0, N'C'),
('CH070','DT003',N'Nguyên tắc cân bằng phương trình oxi hóa khử theo thăng bằng electron là?', N'Bảo toàn khối lượng', N'Bảo toàn điện tích', N'Số electron nhường = số electron nhận', N'Bảo toàn nguyên tố', 0, 0, 1, 0, N'C'),
-- DT004 - Văn Thơ 7 chữ (bổ sung)
('CH071','DT004',N'Thơ thất ngôn bát cú có bao nhiêu câu?', N'4', N'6', N'8', N'10', 0, 0, 1, 0, N'C'),
('CH072','DT004',N'Bài thơ "Đây thôn Vĩ Dạ" của tác giả nào?', N'Xuân Diệu', N'Hàn Mặc Tử', N'Nguyễn Bính', N'Chế Lan Viên', 0, 1, 0, 0, N'B'),
('CH073','DT004',N'Luật bằng trắc trong thơ 7 chữ có quy định gì?', N'Tất cả tiếng phải bằng nhau', N'Tiếng bằng và trắc xen kẽ có quy luật', N'Chỉ dùng tiếng bằng', N'Không có quy định', 0, 1, 0, 0, N'B'),
('CH074','DT004',N'Thể thơ thất ngôn tứ tuyệt có nguồn gốc từ đâu?', N'Pháp', N'Nhật Bản', N'Trung Quốc', N'Ấn Độ', 0, 0, 1, 0, N'C'),
('CH075','DT004',N'Bài thơ "Qua Đèo Ngang" được viết theo thể thơ gì?', N'Lục bát', N'Thất ngôn bát cú Đường luật', N'Song thất lục bát', N'Tự do', 0, 1, 0, 0, N'B'),
-- DT005 - Anh Du lịch (bổ sung)
('CH076','DT005',N'Which phrase means to leave a hotel room at the end of a stay?', N'Check in', N'Check out', N'Book in', N'Register out', 0, 1, 0, 0, N'B'),
('CH077','DT005',N'What does "itinerary" mean?', N'A type of passport', N'A travel plan or schedule', N'A piece of luggage', N'A travel insurance', 0, 1, 0, 0, N'B'),
('CH078','DT005',N'Choose the correct sentence for making a reservation:', N'I want to reserving a table', N'I would like to make a reservation', N'I am reserve a seat', N'I will to book a ticket', 0, 1, 0, 0, N'B'),
('CH079','DT005',N'What does "boarding pass" mean?', N'A document to enter a country', N'A ticket for a bus', N'A card needed to board a plane', N'A hotel receipt', 0, 0, 1, 0, N'C'),
('CH080','DT005',N'Which word means someone who visits a place for pleasure?', N'Immigrant', N'Resident', N'Tourist', N'Commuter', 0, 0, 1, 0, N'C'),
-- DT006 - Sử Lịch sử VN (bổ sung)
('CH081','DT006',N'Chiến thắng Bạch Đằng năm 938 gắn liền với tên tuổi ai?', N'Lý Thường Kiệt', N'Ngô Quyền', N'Trần Hưng Đạo', N'Đinh Bộ Lĩnh', 0, 1, 0, 0, N'B'),
('CH082','DT006',N'Đảng Cộng sản Việt Nam được thành lập vào năm nào?', N'1925', N'1930', N'1945', N'1954', 0, 1, 0, 0, N'B'),
('CH083','DT006',N'Ngày 2/9/1945, Bác Hồ đọc Tuyên ngôn độc lập tại đâu?', N'Huế', N'Sài Gòn', N'Hà Nội - Quảng trường Ba Đình', N'Đà Nẵng', 0, 0, 1, 0, N'C'),
('CH084','DT006',N'Hiệp định Genève (1954) chia cắt Việt Nam ở vĩ tuyến mấy?', N'13', N'16', N'17', N'20', 0, 0, 1, 0, N'C'),
('CH085','DT006',N'Ai lãnh đạo cuộc kháng chiến chống Mông-Nguyên lần thứ nhất năm 1258?', N'Trần Hưng Đạo', N'Trần Thái Tông', N'Lý Thường Kiệt', N'Ngô Quyền', 0, 1, 0, 0, N'B'),
-- DT007 - Địa ĐBSCL (bổ sung)
('CH086','DT007',N'Sông Tiền và Sông Hậu là 2 nhánh của sông nào?', N'Sông Hồng', N'Sông Đà', N'Sông Mê Kông', N'Sông Đồng Nai', 0, 0, 1, 0, N'C'),
('CH087','DT007',N'Vùng nào ở ĐBSCL nổi tiếng với rừng tràm và sinh thái ngập nước?', N'Đồng Tháp Mười', N'Bán đảo Cà Mau', N'Đồng bằng phù sa', N'Vùng núi Thất Sơn', 1, 0, 0, 0, N'A'),
('CH088','DT007',N'Thành phố Cần Thơ là trung tâm kinh tế của vùng nào?', N'Tây Nguyên', N'Đông Nam Bộ', N'Đồng bằng sông Cửu Long', N'Bắc Trung Bộ', 0, 0, 1, 0, N'C'),
('CH089','DT007',N'Sản phẩm thủy sản chủ yếu của ĐBSCL là gì?', N'Tôm và cá tra', N'Cá hồi và tôm hùm', N'Mực và bạch tuộc', N'Cua và ghẹ', 1, 0, 0, 0, N'A'),
('CH090','DT007',N'ĐBSCL đóng góp bao nhiêu % sản lượng lúa gạo của Việt Nam?', N'Khoảng 30%', N'Khoảng 40%', N'Khoảng 50%', N'Hơn 50%', 0, 0, 0, 1, N'D'),
-- DT008 - GDCD Pháp luật (bổ sung)
('CH091','DT008',N'Pháp luật Việt Nam được ban hành bởi cơ quan nào?', N'Chính phủ', N'Quốc hội', N'Chủ tịch nước', N'Bộ Tư pháp', 0, 1, 0, 0, N'B'),
('CH092','DT008',N'Có mấy loại trách nhiệm pháp lý (hình sự, dân sự, hành chính, kỷ luật)?', N'2', N'3', N'4', N'5', 0, 0, 1, 0, N'C'),
('CH093','DT008',N'Độ tuổi tối thiểu để kết hôn theo pháp luật VN: Nam bao nhiêu tuổi?', N'18 tuổi', N'19 tuổi', N'20 tuổi', N'21 tuổi', 0, 0, 1, 0, N'C'),
('CH094','DT008',N'Tội phạm là hành vi vi phạm pháp luật có mức độ nguy hiểm như thế nào?', N'Thấp', N'Trung bình', N'Cao', N'Rất cao', 0, 0, 0, 1, N'D'),
('CH095','DT008',N'Quyền bầu cử và ứng cử của công dân là quyền gì?', N'Quyền dân sự', N'Quyền chính trị', N'Quyền kinh tế', N'Quyền văn hóa', 0, 1, 0, 0, N'B'),
-- DT010 - Toán Đạo hàm (bổ sung)
('CH096','DT010',N'Đạo hàm của f(x) = tan(x) là gì?', N'-1/cos²(x)', N'1/sin²(x)', N'1/cos²(x)', N'-1/sin²(x)', 0, 0, 1, 0, N'C'),
('CH097','DT010',N'Cho f(x) = x² - 4x + 5, đạo hàm f''(2) bằng bao nhiêu?', N'0', N'1', N'2', N'-2', 1, 0, 0, 0, N'A'),
('CH098','DT010',N'Nếu f''(x₀) = 0 và f''''(x₀) > 0, thì x₀ là điểm gì?', N'Cực đại', N'Cực tiểu', N'Uốn', N'Gãy', 0, 1, 0, 0, N'B'),
('CH099','DT010',N'Đạo hàm của f(x) = √x là gì?', N'1/√x', N'2√x', N'1/(2√x)', N'√x/2', 0, 0, 1, 0, N'C'),
('CH100','DT010',N'Quy tắc đạo hàm hợp: nếu h(x) = f(g(x)), thì h''(x) = ?', N'f''(x) × g''(x)', N'f''(g(x)) × g''(x)', N'f''(g''(x))', N'g''(f(x)) × f''(x)', 0, 1, 0, 0, N'B');
go
CREATE PROCEDURE Insert_CauHoi
    @MaCauHoi varchar(15),
	@MaDeThi varchar(15),
    @CauHoi nvarchar(max),
    @CauA nvarchar(max),
    @CauB nvarchar(max),
    @CauC nvarchar(max),
    @CauD nvarchar(max),
    @DapAnA bit,
    @DapAnB bit,
    @DapAnC bit,
    @DapAnD bit,
    @KetQua nvarchar(15)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO tblCauHoi 
    VALUES (@MaCauHoi,@MaDeThi,@CauHoi, @CauA, @CauB, @CauC, @CauD, @DapAnA, @DapAnB, @DapAnC, @DapAnD, @KetQua);
    
END
go
CREATE PROCEDURE Delete_CauHoi
    @MaCauHoi varchar(15)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM tblCauHoi WHERE MaCauHoi = @MaCauHoi;
    
END
go
CREATE PROCEDURE Update_CauHoi
    @MaCauHoi varchar(15),
    @cauHoi nvarchar(max),
	@MaDeThi varchar(15),
    @CauA nvarchar(max),
    @CauB nvarchar(max),
    @CauC nvarchar(max),
    @CauD nvarchar(max),
    @DapAnA bit,
    @DapAnB bit,
    @DapAnC bit,
    @DapAnD bit,
    @KetQua nvarchar(15)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE tblCauHoi
    SET 
        cauHoi = @cauHoi,
		MaDeThi = @MaDeThi,
        CauA = @CauA,
        CauB = @CauB,
        CauC = @CauC,
        CauD = @CauD,
        DapAnA = @DapAnA,
        DapAnB = @DapAnB,
        DapAnC = @DapAnC,
        DapAnD = @DapAnD,
        KetQua = @KetQua
    WHERE 
        MaCauHoi = @MaCauHoi;
    
END
go
CREATE PROCEDURE GetRandomQuestions
(
	@MaDeThi varchar(15)
)
AS
BEGIN
    SELECT TOP 5 * FROM tblCauHoi where MaDeThi=@MaDeThi
    ORDER BY NEWID();
END
go
create view vw_ThongKeDiem as
select TenHocSinh as N'Tên học sinh', hs.MaHocSinh as N'Mã học sinh',GioiTinh as N'Giới tính', avg(DiemSo) as N'Điểm TB'
from tblThongTinBaiThiCuaHocSinh bt, tblThongTinHocSinh hs,tblThongTinKetQuaBaiThi kq
where bt.MaBaiThi=kq.MaBaiThi and bt.MaHocSinh=hs.MaHocSinh
group by  TenHocSinh, hs.MaHocSinh, GioiTinh
go

create table tblCauHoi_new
(
	MaCauHoi varchar(15) ,
	MaDeThi varchar(15),
	cauHoi nvarchar(max),
	CauA nvarchar(max),
	CauB nvarchar(max),
	CauC nvarchar(max),
	CauD nvarchar(max),
	DapAnA bit,
	DapAnB bit,
	DapAnC bit,
	DapAnD bit,
	KetQua nvarchar(15)
)
go
insert into tblCauHoi_new
exec GetRandomQuestions 'DT009'

select *  from tblCauHoi_new

delete from tblCauHoi_new