using TOLEAGRI.Model.Persistence;
using TOLEAGRI.Model.Services;
using TOLEAGRI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<EstoqueService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TOLEDbContext>(options => options.UseSqlServer("name=ConnectionStrings:TOLEconnectionString"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Estoque}/{action=Index}/{id?}");

app.Run();
