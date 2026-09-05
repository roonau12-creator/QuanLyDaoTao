using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Utility;
using QuanLyDaoTao.Models;

namespace QuanLyDaoTao;
public static class ApplicationDbInitializer
{
    public const string DefaultStudentPassword = "Student@123";
    public const string DefaultAdminPassword = "Admin@123";

    private static readonly string[] Roles = new[]
    {
        SD.Role_Admin,
        SD.Role_Student,
        SD.Role_Teacher
    };

    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await dbContext.Database.MigrateAsync();

        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        await EnsureAdminAsync(userManager);

        var sinhViens = await dbContext.sinhViens.ToListAsync();
        foreach (var sv in sinhViens)
        {
            await EnsureStudentAccountAsync(userManager, sv.MaSoSinhVien, sv.Hoten, sv.MaSinhVien);
        }
    }

    private static async Task EnsureAdminAsync(UserManager<ApplicationUser> userManager)
    {
        const string adminUserName = "admin";
        var adminUser = await userManager.FindByNameAsync(adminUserName);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminUserName,
                Email = "admin@example.com",
                EmailConfirmed = true,
                Hoten = "Quản trị viên"
            };
            var result = await userManager.CreateAsync(adminUser, DefaultAdminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, SD.Role_Admin);
            }
        }
        else if (!await userManager.IsInRoleAsync(adminUser, SD.Role_Admin))
        {
            await userManager.AddToRoleAsync(adminUser, SD.Role_Admin);
        }
    }

    public static async Task EnsureStudentAccountAsync(
        UserManager<ApplicationUser> userManager,
        string maSoSinhVien,
        string hoten,
        int maSinhVien)
    {
        if (string.IsNullOrWhiteSpace(maSoSinhVien))
        {
            return;
        }

        var user = await userManager.FindByNameAsync(maSoSinhVien);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = maSoSinhVien,
                Email = string.Empty,
                EmailConfirmed = true,
                Hoten = hoten,
                MaSinhVien = maSinhVien
            };
            var result = await userManager.CreateAsync(user, DefaultStudentPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, SD.Role_Student);
            }
        }
        else
        {
            user.Hoten = hoten;
            user.MaSinhVien = maSinhVien;
            await userManager.UpdateAsync(user);
            if (!await userManager.IsInRoleAsync(user, SD.Role_Student))
            {
                await userManager.AddToRoleAsync(user, SD.Role_Student);
            }
        }
    }
}
