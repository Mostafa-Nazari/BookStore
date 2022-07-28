using BookStore.DataAccess;
using BookStoreWeb.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BookStore.DataAccess.Repository.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDatabaseContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("BookStore.DataAccess")));
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<IdentityDatabaseContext>();
builder.Services.AddDbContext<IdentityDatabaseContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("IdentityDatabaseContextConnection"),
    b => b.MigrationsAssembly("BookStoreWeb")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
