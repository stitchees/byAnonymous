using byAnonymous.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL");
}

builder.Services.AddDbContext<MensajeDbContext>(options =>
    options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGet("/Mensaje", (MensajeDbContext context) =>
{
    return context.Mensaje.ToList();
})
.WithName("GetMensajes");
//dan error por eso estan comentadas si funciona se borran 
//.WithOpenApi();

app.MapPost("/Mensaje", (Mensaje mensaje, MensajeDbContext context) =>
{
    context.Add(mensaje);
    context.SaveChanges();
})
.WithName("CreateMensaje");
//.WithOpenApi();

app.Run();