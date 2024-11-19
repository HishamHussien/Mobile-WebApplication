using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mobile_WebApplication.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Mobile_WebApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Mobile_WebApplicationContext") ?? throw new InvalidOperationException("Connection string 'Mobile_WebApplicationContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(1); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=usersaccounts}/{action=login}/{id?}");

app.Run();
