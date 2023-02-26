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
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connStr));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
