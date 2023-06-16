using Microsoft.EntityFrameworkCore;
using SignalRDemo3ytEFC.Data;
using SignalRDemo3ytEFC.Hubs;
using SignalRDemo3ytEFC.MiddlewareExtensions;
using SignalRDemo3ytEFC.Repositories;
using SignalRDemo3ytEFC.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// Add services to the container.
builder.Services.AddControllersWithViews();

// DI
builder.Services.AddSingleton<DashboardHub>();
builder.Services.AddSingleton<SubscribeProductTableDependency>();


builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapHub<DashboardHub>("/dashboardHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.UseSqlTableDependency<SubscribeProductTableDependency>(connectionString);
app.Run();
