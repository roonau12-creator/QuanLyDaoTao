namespace QuanLyDaoTao.DataAccess.Data;

using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.Models;
public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Khoa> khoas { get; set; }
    public DbSet<Lop> lops { get; set; }
    public DbSet<SinhVien> sinhViens { get; set; }
    public DbSet<MonHoc> monHocs { get; set; }
    public DbSet<HocKy>hocKys { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Khoa>().HasData(
            new Khoa { MaKhoa = 1, TenKhoa = "Hạ tầng kỹ thuật", MoTa = "Khoa công nghệ thông tin" }

        );
      modelBuilder.Entity<SinhVien>().HasData(
    new SinhVien
    {
        MaSinhVien = 1,
        MaSoSinhVien = "SV001",
        Hoten = "Nguyễn Văn An",
        NgaySinh = new DateTime(2005, 1, 15, 0, 0, 0, DateTimeKind.Utc),
        GioiTinh = true,
        CanCuocCongDan = "079205001234",
        Email = "nguyenvanan@gmail.com",
        SoDienThoai = "0901234567",
        DiaChi = "Đắk Lắk",
        NgayNhapHoc = new DateTime(2023, 9, 5, 0, 0, 0, DateTimeKind.Utc),
        MaLop = 3,
        AnhDaiDien = "an.jpg"
    },

    new SinhVien
    {
        MaSinhVien = 2,
        MaSoSinhVien = "SV002",
        Hoten = "Trần Thị Bình",
        NgaySinh = new DateTime(2005, 3, 20, 0, 0, 0, DateTimeKind.Utc),
        GioiTinh = false,
        CanCuocCongDan = "079205002345",
        Email = "tranthibinh@gmail.com",
        SoDienThoai = "0912345678",
        DiaChi = "Gia Lai",
        NgayNhapHoc = new DateTime(2023, 9, 5, 0, 0, 0, DateTimeKind.Utc),
        MaLop = 3,
        AnhDaiDien = "binh.jpg"
    },

    new SinhVien
    {
        MaSinhVien = 3,
        MaSoSinhVien = "SV003",
        Hoten = "Lê Văn Cường",
        NgaySinh = new DateTime(2004, 11, 10, 0, 0, 0, DateTimeKind.Utc),
        GioiTinh = true,
        CanCuocCongDan = "079204003456",
        Email = "levancuong@gmail.com",
        SoDienThoai = "0923456789",
        DiaChi = "Kon Tum",
        NgayNhapHoc = new DateTime(2023, 9, 5, 0, 0, 0, DateTimeKind.Utc),
        MaLop = 3,
        AnhDaiDien = "cuong.jpg"
    },

    new SinhVien
    {
        MaSinhVien = 4,
        MaSoSinhVien = "SV004",
        Hoten = "Phạm Thị Dung",
        NgaySinh = new DateTime(2005, 6, 25, 0, 0, 0, DateTimeKind.Utc),
        GioiTinh =false,
        CanCuocCongDan = "079205004567",
        Email = "phamthidung@gmail.com",
        SoDienThoai = "0934567890",
        DiaChi = "Đắk Nông",
        NgayNhapHoc = new DateTime(2023, 9, 5, 0, 0, 0, DateTimeKind.Utc),
        MaLop = 3,
        AnhDaiDien = "dung.jpg"
    },

    new SinhVien
    {
        MaSinhVien = 5,
        MaSoSinhVien = "SV005",
        Hoten = "Hoàng Văn Em",
        NgaySinh = new DateTime(2005, 9, 12, 0, 0, 0, DateTimeKind.Utc),
        GioiTinh = true,
        CanCuocCongDan = "079205005678",
        Email = "hoangvanem@gmail.com",
        SoDienThoai = "0945678901",
        DiaChi = "Phú Yên",
        NgayNhapHoc = new DateTime(2023, 9, 5, 0, 0, 0, DateTimeKind.Utc),
        MaLop = 3,
        AnhDaiDien = "em.jpg"
    }
);

        
    }
}
