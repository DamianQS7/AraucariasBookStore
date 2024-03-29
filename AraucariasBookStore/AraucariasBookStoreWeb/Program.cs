using Microsoft.EntityFrameworkCore;
using AraucariasBookStore.DataAccess;
using AraucariasBookStore.DataAccess.Repository.IRepository;
using AraucariasBookStore.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

//Setting up the database before the app builds.
string? connStr = builder.Configuration.GetConnectionString("ConnString");
string? assemblyName = "AraucariasBookStore.DataAccess";
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connStr, b => b.MigrationsAssembly(assemblyName)));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
