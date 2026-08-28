namespace QuanLySinhVien.DataAccess.Data;

using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Models;
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
            new Khoa{MaKhoa=1,TenKhoa="Hạ tầng kỹ thuật",MoTa="Khoa công nghệ thông tin"}

        );
        
    }
}
