using Hien_Dat_Nha_De2.Data;
using Hien_Dat_Nha_De2.Models;
using Hien_Dat_Nha_De2.Repositories; // Namespace chứa GenericRepository
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. KẾT NỐI CƠ SỞ DỮ LIỆU (Yêu cầu 1: 0,5 điểm)
// Lấy chuỗi kết nối có tên "DefaultConnection" từ file appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. CẤU HÌNH IDENTITY (Yêu cầu 2: 0,5 điểm)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Tùy chỉnh policy mật khẩu cho dễ dàng test nội bộ
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 3. ĐĂNG KÝ DEPENDENCY INJECTION (DI)
// Đăng ký Generic Repository để sử dụng ở các Controller (Câu 3, 4, 6, 7)
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Cho phép sử dụng các file tĩnh như CSS (Bootstrap), JS trong thư mục wwwroot

app.UseRouting();

// Bắt buộc phải gọi UseAuthentication (Xác thực) trước UseAuthorization (Phân quyền)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();