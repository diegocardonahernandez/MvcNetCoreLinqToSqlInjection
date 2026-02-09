using MvcNetCoreLinqToSqlInjection.Models;
using MvcNetCoreLinqToSqlInjection.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

Coche car = new Coche();
car.Marca = "PONTIAC";
car.Modelo = "FIREBIRD";
car.Imagen = "lmlyml.webp";
car.Velocidad = 0;
car.VelocidadMaxima = 280;
builder.Services.AddSingleton<ICoche, Coche>(x=>car);

//Nota: Los repos suelen ir como Transient
builder.Services.AddTransient<IRepositoryDoctores,RepositoryDoctoresOracle>();

//Resolvemos el servicio coche
//builder.Services.AddTransient<Coche>();
//builder.Services.AddSingleton<Coche>();
//builder.Services.AddSingleton<Deportivo>();
//builder.Services.AddSingleton<ICoche,Deportivo>();

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
