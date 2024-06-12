using Microsoft.EntityFrameworkCore;
using CMSysRealization.Data;
using CMSys.Core.Entities.Membership;
using CMSysRealization.Helpers;
using CMSysRealization.Areas.Identity.Data;
using AppContext = CMSys.Infrastructure.AppContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("MainDbConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationUserContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddSingleton<AppContext>();

builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationUserContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Courses}/{action=Index}/{id?}");

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
