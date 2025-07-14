using Microsoft.EntityFrameworkCore;
using PeliculasAPI;
using Microsoft.Extensions.Configuration; // Asegúrate de tener este using

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 1. Leer el origen permitido desde appsettings.json
//    El builder.Configuration ya tiene acceso a los valores de appsettings.json
var origenPermitido = builder.Configuration["origenesPermitidos"];

// 2. Configurar el servicio CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MiPoliticaCors", // Dale un nombre a tu política
                      policy =>
                      {
                          // Si origenPermitido no es null o vacío, úsalo.
                          // De lo contrario, puedes poner un fallback o lanzar un error,
                          // aunque para desarrollo el valor suele estar garantizado.
                          if (!string.IsNullOrEmpty(origenPermitido))
                          {
                              policy.WithOrigins(origenPermitido)
                                    .AllowAnyHeader()    // Permite cualquier cabecera (Content-Type, Authorization, etc.)
                                    .AllowAnyMethod();   // Permite cualquier método HTTP (GET, POST, PUT, DELETE)
                          }
                          else
                          {
                              // Opcional: Manejar el caso donde el origen no está configurado
                              // Por ejemplo, no permitir nada o registrar una advertencia.
                              Console.WriteLine("Advertencia: 'origenesPermitidos' no está configurado en appsettings.json para CORS.");
                          }
                      });
});

// Agrega servicios a tu contenedor
builder.Services.AddControllers();
// Otros servicios como DbContext, Swagger, etc.
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

builder.Services.AddOutputCache(opciones =>
{
    opciones.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(60);
});

var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!.Split(",");

builder.Services.AddCors(opciones => 
{
    opciones.AddDefaultPolicy(opcionesCORS =>
    {
        opcionesCORS.WithOrigins(origenesPermitidos).AllowAnyMethod().AllowAnyHeader().
        WithExposedHeaders("cantidad-total-registros");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configurar el pipeline de solicitudes HTTP.

// 3. Habilitar el middleware CORS y aplicar tu política
app.UseCors("MiPoliticaCors"); // Usa el nombre de tu política aquí

app.UseHttpsRedirection();

app.UseOutputCache();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
