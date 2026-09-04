-- =====================================================================
-- QUẢN LÝ ĐÀO TẠO - DỮ LIỆU MẪU (idempotent)
-- Chạy trong database QuanLySinhVien
-- =====================================================================

BEGIN;

DO $$
BEGIN
    -- ---------- 1. KHOAS ----------
    IF NOT EXISTS (SELECT 1 FROM "khoas" WHERE "TenKhoa"='Công nghệ thông tin') THEN
        INSERT INTO "khoas" ("TenKhoa","MoTa") VALUES ('Công nghệ thông tin','Đào tạo kỹ sư công nghệ thông tin');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "khoas" WHERE "TenKhoa"='Kinh tế') THEN
        INSERT INTO "khoas" ("TenKhoa","MoTa") VALUES ('Kinh tế','Đào tạo cử nhân kinh tế, quản trị');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "khoas" WHERE "TenKhoa"='Ngoại ngữ') THEN
        INSERT INTO "khoas" ("TenKhoa","MoTa") VALUES ('Ngoại ngữ','Đào tạo cử nhân ngôn ngữ Anh, Trung');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "khoas" WHERE "TenKhoa"='Điện tử viễn thông') THEN
        INSERT INTO "khoas" ("TenKhoa","MoTa") VALUES ('Điện tử viễn thông','Đào tạo kỹ sư điện tử viễn thông');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "khoas" WHERE "TenKhoa"='Cơ khí') THEN
        INSERT INTO "khoas" ("TenKhoa","MoTa") VALUES ('Cơ khí','Đào tạo kỹ sư cơ khí, tự động hóa');
    END IF;
END $$;

DO $$
DECLARE
    khoa_cntt   INT; khoa_kt INT; khoa_nn INT; khoa_dtvt INT; khoa_ck INT;
BEGIN
    SELECT "MaKhoa" INTO khoa_cntt   FROM "khoas" WHERE "TenKhoa"='Công nghệ thông tin';
    SELECT "MaKhoa" INTO khoa_kt     FROM "khoas" WHERE "TenKhoa"='Kinh tế';
    SELECT "MaKhoa" INTO khoa_nn     FROM "khoas" WHERE "TenKhoa"='Ngoại ngữ';
    SELECT "MaKhoa" INTO khoa_dtvt   FROM "khoas" WHERE "TenKhoa"='Điện tử viễn thông';
    SELECT "MaKhoa" INTO khoa_ck     FROM "khoas" WHERE "TenKhoa"='Cơ khí';

    -- ---------- 2. LOPS ----------
    IF NOT EXISTS (SELECT 1 FROM "lops" WHERE "TenLop"='CNTT-K64') THEN
        INSERT INTO "lops" ("TenLop","MaKhoa") VALUES ('CNTT-K64', khoa_cntt);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "lops" WHERE "TenLop"='CNTT-K65') THEN
        INSERT INTO "lops" ("TenLop","MaKhoa") VALUES ('CNTT-K65', khoa_cntt);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "lops" WHERE "TenLop"='KT-K64') THEN
        INSERT INTO "lops" ("TenLop","MaKhoa") VALUES ('KT-K64', khoa_kt);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "lops" WHERE "TenLop"='NN-K64') THEN
        INSERT INTO "lops" ("TenLop","MaKhoa") VALUES ('NN-K64', khoa_nn);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "lops" WHERE "TenLop"='ĐTVT-K63') THEN
        INSERT INTO "lops" ("TenLop","MaKhoa") VALUES ('ĐTVT-K63', khoa_dtvt);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "lops" WHERE "TenLop"='CK-K64') THEN
        INSERT INTO "lops" ("TenLop","MaKhoa") VALUES ('CK-K64', khoa_ck);
    END IF;
END $$;

DO $$
DECLARE
    khoa_cntt INT; khoa_kt INT; khoa_nn INT; khoa_dtvt INT; khoa_ck INT;
    lop_cntt64 INT; lop_cntt65 INT; lop_kt64 INT; lop_nn64 INT; lop_dtvt63 INT; lop_ck64 INT;
    gv1 INT; gv2 INT; gv3 INT; gv4 INT; gv5 INT; gv6 INT;
BEGIN
    SELECT "MaKhoa" INTO khoa_cntt   FROM "khoas" WHERE "TenKhoa"='Công nghệ thông tin';
    SELECT "MaKhoa" INTO khoa_kt     FROM "khoas" WHERE "TenKhoa"='Kinh tế';
    SELECT "MaKhoa" INTO khoa_nn     FROM "khoas" WHERE "TenKhoa"='Ngoại ngữ';
    SELECT "MaKhoa" INTO khoa_dtvt   FROM "khoas" WHERE "TenKhoa"='Điện tử viễn thông';
    SELECT "MaKhoa" INTO khoa_ck     FROM "khoas" WHERE "TenKhoa"='Cơ khí';
    SELECT "MaLop" INTO lop_cntt64 FROM "lops" WHERE "TenLop"='CNTT-K64';
    SELECT "MaLop" INTO lop_cntt65 FROM "lops" WHERE "TenLop"='CNTT-K65';
    SELECT "MaLop" INTO lop_kt64   FROM "lops" WHERE "TenLop"='KT-K64';
    SELECT "MaLop" INTO lop_nn64   FROM "lops" WHERE "TenLop"='NN-K64';
    SELECT "MaLop" INTO lop_dtvt63 FROM "lops" WHERE "TenLop"='ĐTVT-K63';
    SELECT "MaLop" INTO lop_ck64   FROM "lops" WHERE "TenLop"='CK-K64';

    -- ---------- 3. GIANGVIEN ----------
    IF NOT EXISTS (SELECT 1 FROM "giangViens" WHERE "MaSoGiangVien"='GV001') THEN
        INSERT INTO "giangViens" ("MaSoGiangVien","HoTen","NgaySinh","GioiTinh","Email","SoDienThoai","DiaChi","HocVi","ChucVu","MaKhoa","TrangThai")
        VALUES ('GV001','TS. Trần Văn Đạt','1978-05-12','Nam','dat.tran@school.edu.vn','0912345001','TP. Hồ Chí Minh','Tiến sĩ','Trưởng bộ môn',khoa_cntt,true);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "giangViens" WHERE "MaSoGiangVien"='GV002') THEN
        INSERT INTO "giangViens" ("MaSoGiangVien","HoTen","NgaySinh","GioiTinh","Email","SoDienThoai","DiaChi","HocVi","ChucVu","MaKhoa","TrangThai")
        VALUES ('GV002','ThS. Nguyễn Thị Hà','1985-08-23','Nữ','ha.nguyen@school.edu.vn','0912345002','TP. Hồ Chí Minh','Thạc sĩ','Giảng viên',khoa_cntt,true);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "giangViens" WHERE "MaSoGiangVien"='GV003') THEN
        INSERT INTO "giangViens" ("MaSoGiangVien","HoTen","NgaySinh","GioiTinh","Email","SoDienThoai","DiaChi","HocVi","ChucVu","MaKhoa","TrangThai")
        VALUES ('GV003','TS. Phạm Văn Minh','1980-01-30','Nam','minh.pham@school.edu.vn','0912345003','Hà Nội','Tiến sĩ','Trưởng bộ môn',khoa_kt,true);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "giangViens" WHERE "MaSoGiangVien"='GV004') THEN
        INSERT INTO "giangViens" ("MaSoGiangVien","HoTen","NgaySinh","GioiTinh","Email","SoDienThoai","DiaChi","HocVi","ChucVu","MaKhoa","TrangThai")
        VALUES ('GV004','ThS. Lê Thị Thu','1987-11-05','Nữ','thu.le@school.edu.vn','0912345004','Đà Nẵng','Thạc sĩ','Giảng viên',khoa_nn,true);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "giangViens" WHERE "MaSoGiangVien"='GV005') THEN
        INSERT INTO "giangViens" ("MaSoGiangVien","HoTen","NgaySinh","GioiTinh","Email","SoDienThoai","DiaChi","HocVi","ChucVu","MaKhoa","TrangThai")
        VALUES ('GV005','PGS.TS. Hoàng Đức Long','1972-03-18','Nam','long.hoang@school.edu.vn','0912345005','TP. Hồ Chí Minh','Phó giáo sư','Trưởng khoa',khoa_dtvt,true);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "giangViens" WHERE "MaSoGiangVien"='GV006') THEN
        INSERT INTO "giangViens" ("MaSoGiangVien","HoTen","NgaySinh","GioiTinh","Email","SoDienThoai","DiaChi","HocVi","ChucVu","MaKhoa","TrangThai")
        VALUES ('GV006','ThS. Vũ Thị Lan','1990-07-09','Nữ','lan.vu@school.edu.vn','0912345006','Cần Thơ','Thạc sĩ','Giảng viên',khoa_ck,true);
    END IF;

    -- ---------- 4. PHONGHOC ----------
    IF NOT EXISTS (SELECT 1 FROM "phongHocs" WHERE "TenPhong"='A101') THEN
        INSERT INTO "phongHocs" ("TenPhong","ToaNha","SucChua","TrangThai") VALUES ('A101','Nhà A',45,true);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "phongHocs" WHERE "TenPhong"='A102') THEN
        INSERT INTO "phongHocs" ("TenPhong","ToaNha","SucChua","TrangThai") VALUES ('A102','Nhà A',45,true);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "phongHocs" WHERE "TenPhong"='B201') THEN
        INSERT INTO "phongHocs" ("TenPhong","ToaNha","SucChua","TrangThai") VALUES ('B201','Nhà B',60,true);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "phongHocs" WHERE "TenPhong"='B202') THEN
        INSERT INTO "phongHocs" ("TenPhong","ToaNha","SucChua","TrangThai") VALUES ('B202','Nhà B',30,true);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "phongHocs" WHERE "TenPhong"='C301') THEN
        INSERT INTO "phongHocs" ("TenPhong","ToaNha","SucChua","TrangThai") VALUES ('C301','Nhà C',80,true);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "phongHocs" WHERE "TenPhong"='P.Thực Hành 1') THEN
        INSERT INTO "phongHocs" ("TenPhong","ToaNha","SucChua","TrangThai") VALUES ('P.Thực Hành 1','Xưởng TH',25,true);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "phongHocs" WHERE "TenPhong"='P.Thực Hành 2') THEN
        INSERT INTO "phongHocs" ("TenPhong","ToaNha","SucChua","TrangThai") VALUES ('P.Thực Hành 2','Xưởng TH',25,true);
    END IF;

    -- ---------- 5. MONHOC ----------
    IF NOT EXISTS (SELECT 1 FROM "monHocs" WHERE "TenMonHoc"='Cấu trúc dữ liệu và giải thuật') THEN
        INSERT INTO "monHocs" ("TenMonHoc","SoTinChi","MaKhoa","MoTa") VALUES ('Cấu trúc dữ liệu và giải thuật',3,khoa_cntt,'Các cấu trúc dữ liệu và thuật toán căn bản');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "monHocs" WHERE "TenMonHoc"='Hệ điều hành') THEN
        INSERT INTO "monHocs" ("TenMonHoc","SoTinChi","MaKhoa","MoTa") VALUES ('Hệ điều hành',3,khoa_cntt,'Nguyên lý hệ điều hành máy tính');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "monHocs" WHERE "TenMonHoc"='Cơ sở dữ liệu') THEN
        INSERT INTO "monHocs" ("TenMonHoc","SoTinChi","MaKhoa","MoTa") VALUES ('Cơ sở dữ liệu',3,khoa_cntt,'Thiết kế và quản trị cơ sở dữ liệu');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "monHocs" WHERE "TenMonHoc"='Nguyên lý kế toán') THEN
        INSERT INTO "monHocs" ("TenMonHoc","SoTinChi","MaKhoa","MoTa") VALUES ('Nguyên lý kế toán',3,khoa_kt,'Kế toán căn bản');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "monHocs" WHERE "TenMonHoc"='Quản trị học') THEN
        INSERT INTO "monHocs" ("TenMonHoc","SoTinChi","MaKhoa","MoTa") VALUES ('Quản trị học',2,khoa_kt,'Quản trị doanh nghiệp');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "monHocs" WHERE "TenMonHoc"='Tiếng Anh giao tiếp') THEN
        INSERT INTO "monHocs" ("TenMonHoc","SoTinChi","MaKhoa","MoTa") VALUES ('Tiếng Anh giao tiếp',2,khoa_nn,'Kỹ năng giao tiếp tiếng Anh');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "monHocs" WHERE "TenMonHoc"='Xử lý tín hiệu số') THEN
        INSERT INTO "monHocs" ("TenMonHoc","SoTinChi","MaKhoa","MoTa") VALUES ('Xử lý tín hiệu số',3,khoa_dtvt,'Xử lý tín hiệu số');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "monHocs" WHERE "TenMonHoc"='Vẽ kỹ thuật cơ khí') THEN
        INSERT INTO "monHocs" ("TenMonHoc","SoTinChi","MaKhoa","MoTa") VALUES ('Vẽ kỹ thuật cơ khí',2,khoa_ck,'Bản vẽ kỹ thuật ngành cơ khí');
    END IF;

    -- ---------- 6. HOCKY ----------
    IF NOT EXISTS (SELECT 1 FROM "hocKys" WHERE "TenHocKy"='Học kỳ 2' AND "NamHoc"=2023) THEN
        INSERT INTO "hocKys" ("TenHocKy","NamHoc","NgayBatDau","NgayKetThuc") VALUES ('Học kỳ 2',2023,'2024-02-01','2024-06-15');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "hocKys" WHERE "TenHocKy"='Học kỳ 1' AND "NamHoc"=2024) THEN
        INSERT INTO "hocKys" ("TenHocKy","NamHoc","NgayBatDau","NgayKetThuc") VALUES ('Học kỳ 1',2024,'2024-09-01','2025-01-15');
    END IF;
    IF NOT EXISTS (SELECT 1 FROM "hocKys" WHERE "TenHocKy"='Học kỳ 2' AND "NamHoc"=2024) THEN
        INSERT INTO "hocKys" ("TenHocKy","NamHoc","NgayBatDau","NgayKetThuc") VALUES ('Học kỳ 2',2024,'2025-02-01','2025-06-15');
    END IF;

    -- Lấy các khoá tham chiếu
    -- (đã có các biến lớp, khóa, giảng viên định sẵn)
    SELECT "MaGiangVien" INTO gv1 FROM "giangViens" WHERE "MaSoGiangVien"='GV001';

    -- ---------- 7. HOCPHAN ----------
    IF NOT EXISTS (SELECT 1 FROM "hocPhans" WHERE "MaHocPhanCode"='HP001') THEN
        INSERT INTO "hocPhans" ("MaHocPhanCode","MaMonHoc","MaHocKy","MaGiangVien","SiSoToiDa","SiSoHienTai","NgayMoDangKy","NgayDongDangKy","TrangThai","GhiChu")
        SELECT 'HP001', m."MaMonHoc", h."MaHocKy", gv1, 60, 5,
               '2023-09-01 07:00:00+07','2023-09-30 23:59:59+07','Đã đóng','Học kỳ 1 - 2023'
        FROM "monHocs" m, "hocKys" h
        WHERE m."TenMonHoc"='Lập trình cơ bản' AND h."TenHocKy"='Học kỳ 1' AND h."NamHoc"=2023;
    END IF;

    -- ---------- 8. LICHHOC ----------
    IF NOT EXISTS (SELECT 1 FROM "lichHocs" WHERE "MaLop"=lop_cntt64 AND "MaMonHoc"=(SELECT "MaMonHoc" FROM "monHocs" WHERE "TenMonHoc"='Lập trình cơ bản')) THEN
        INSERT INTO "lichHocs" ("MaMonHoc","MaLop","Thu","TietBatDau","SoTiet","NgayBatDau","NgayKetThuc","GhiChu")
        SELECT m."MaMonHoc", lop_cntt64, 2, 1, 3, '2023-09-04 07:00:00+07','2023-12-25 07:00:00+07','Phòng A101'
        FROM "monHocs" m WHERE m."TenMonHoc"='Lập trình cơ bản';
    END IF;

    -- ---------- 9. DANGKYHOCPHAN ----------
    IF NOT EXISTS (SELECT 1 FROM "dangKyHocPhans" d JOIN "hocPhans" hp ON d."MaHocPhan"=hp."MaHocPhan" WHERE hp."MaHocPhanCode"='HP001') THEN
        INSERT INTO "dangKyHocPhans" ("MaSinhVien","MaHocPhan","MaHocKy","NgayDangKy","TrangThai","GhiChu")
        SELECT s."MaSinhVien", hp."MaHocPhan", hp."MaHocKy", '2023-09-05 08:00:00+07', 'Đã đăng ký', 'Đăng ký đầu kỳ'
        FROM "sinhViens" s, "hocPhans" hp
        WHERE hp."MaHocPhanCode"='HP001' AND s."MaSinhVien" <= 5;
    END IF;
END $$;

COMMIT;

-- ---------- KIỂM TRA KẾT QUẢ ----------
SELECT 'Khoas' AS Bang, COUNT(*) AS SoLuong FROM "khoas"
UNION ALL SELECT 'Lops', COUNT(*) FROM "lops"
UNION ALL SELECT 'SinhViens', COUNT(*) FROM "sinhViens"
UNION ALL SELECT 'MonHocs', COUNT(*) FROM "monHocs"
UNION ALL SELECT 'HocKys', COUNT(*) FROM "hocKys"
UNION ALL SELECT 'GiangViens', COUNT(*) FROM "giangViens"
UNION ALL SELECT 'PhongHocs', COUNT(*) FROM "phongHocs"
UNION ALL SELECT 'HocPhans', COUNT(*) FROM "hocPhans"
UNION ALL SELECT 'LichHocs', COUNT(*) FROM "lichHocs"
UNION ALL SELECT 'DangKyHocPhans', COUNT(*) FROM "dangKyHocPhans";
