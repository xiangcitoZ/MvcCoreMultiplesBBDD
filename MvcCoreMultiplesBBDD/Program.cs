using Microsoft.EntityFrameworkCore;
using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Repository;

var builder = WebApplication.CreateBuilder(args);

string connectionString =
    builder.Configuration.GetConnectionString("SqlHospital");

builder.Services.AddTransient<IRepositoryEmpleados, RepositoryEmpleadosSql>();
builder.Services.AddDbContext<EmpleadosContext>
     (options => options.UseSqlServer(connectionString));



//string connectionString =
//    builder.Configuration.GetConnectionString("OracleHospital");

//builder.Services.AddTransient<IRepositoryEmpleados, RepositoryEmpleadosOracle>();

//builder.Services.AddDbContext<EmpleadosContext>
//     (options => options.UseOracle(connectionString));


builder.Services.AddControllersWithViews();




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
    pattern: "{controller=Empleados}/{action=Index}/{id?}");

app.Run();
