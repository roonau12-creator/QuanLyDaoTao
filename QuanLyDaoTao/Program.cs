using Microsoft.EntityFrameworkCore;
using QuanLyDaoTao.DataAccess.Data;
using QuanLyDaoTao.Business;
using QuanLyDaoTao.Business.Services;
using QuanLyDaoTao.Business.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";
    options.FormFieldName = "__RequestVerificationToken";
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddScoped<IKhoaService,KhoaService>();
builder.Services.AddScoped<ILopService,LopService>();
builder.Services.AddScoped<ISinhVienService,SinhVienService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}",
    defaults:new {area="Customer"})
    .WithStaticAssets();


app.Run();
