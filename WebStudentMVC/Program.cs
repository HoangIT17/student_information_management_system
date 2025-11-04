using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStudentMVC.Data;
using WebStudentMVC.Models;
using WebStudentMVC.Services;
using WebStudentMVC.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddDbContext<AppDbContext>(opptions => opptions.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<Users, IdentityRole>(opptions =>
{
    opptions.Password.RequireNonAlphanumeric = false;
    opptions.Password.RequiredLength = 8;
    opptions.Password.RequireUppercase = false;
    opptions.Password.RequireLowercase = false;
    opptions.User.RequireUniqueEmail = true;
    opptions.SignIn.RequireConfirmedAccount = false;
    opptions.SignIn.RequireConfirmedEmail = false;
    opptions.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();
await SeedService.SeedRolesAndAdminAsync(app.Services);

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
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


app.Run();
