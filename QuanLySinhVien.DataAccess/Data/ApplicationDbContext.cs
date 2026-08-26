namespace QuanLySinhVien.DataAccess.Data;

using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Models;
public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Khoa> khoas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Khoa>().HasData(
            new Khoa{MaKhoa=1,TenKhoa="Hạ tầng kỹ thuật",MoTa="Khoa công nghệ thông tin"}

        );
        
    }
}
